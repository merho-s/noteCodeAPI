using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using noteCodeAPI.Middlewares;
using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services;
using noteCodeAPI.Services.Interfaces;
using noteCodeAPI.Tools;
using System.Net;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("all", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
    options.AddPolicy("specificOrigin", policyBuilder =>
    {
        policyBuilder.WithOrigins("https://notecode.fr")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddDbContext<DataDbContext>();
builder.Services.AddScoped<NoteRepository>();
builder.Services.AddScoped<UserAppRepository>();
builder.Services.AddScoped<CodetagRepository>();
builder.Services.AddScoped<UnusedActiveTokenRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();
builder.Services.AddScoped<IAuthentication, AuthenticationService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<UserAppService>();
builder.Services.AddScoped<CodetagService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidIssuer = "noteCode",
    ValidateLifetime = true,
    ValidateAudience = true,
    ValidAudience = "noteCode",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["EnvironmentVariables:JWTSecretKey"])),
});
builder.Services.AddAuthorization((builder) =>
{
    builder.AddPolicy("admin", options =>
    {
        options.RequireRole("admin");
    });

    builder.AddPolicy("user", options =>
    {
        options.RequireRole("admin", "user");
    });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DataDbContext>();
    DbInitializer.Initialize(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("all");
} else
{
    app.UseCors("specificOrigin");
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<IsTokenBannedMiddleware>();   

app.MapControllers();

app.Run();
