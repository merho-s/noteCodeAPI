using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using noteCodeAPI.Middlewares;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services;
using noteCodeAPI.Services.Interfaces;
using noteCodeAPI.Tools;

using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
string currentDirectory = Directory.GetCurrentDirectory();
builder.Services.AddDbContext<DataDbContext>(options =>

    options.UseSqlServer(@$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={currentDirectory}\noteCodeDB.mdf;Integrated Security=True;Connect Timeout=30"),
    ServiceLifetime.Singleton
);
builder.Services.AddSingleton<NoteRepository>();
builder.Services.AddSingleton<UserAppRepository>();
builder.Services.AddSingleton<CodetagRepository>();
builder.Services.AddSingleton<UnusedActiveTokenRepository>();
builder.Services.AddSingleton<TagAliasRepository>();
builder.Services.AddSingleton<ILogin, LoginJwtService>();
builder.Services.AddSingleton<NoteService>();
builder.Services.AddSingleton<UserAppService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(a =>
{
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidIssuer = "sogeti",
    ValidateLifetime = true,
    ValidateAudience = true,
    ValidAudience = "sogeti",
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("J'suis la clé, j'suis la clé, j'suis la clé, j'suis la clééééé ! (ref à Dora l'Exploratrice, t'as compris ?)")),

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("all");
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<IsTokenBannedMiddleware>();   

app.MapControllers();

app.Run();
