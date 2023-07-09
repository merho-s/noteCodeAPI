using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services;

namespace noteCodeAPI.Middlewares
{
    public class IsTokenBannedMiddleware
    {
        private readonly RequestDelegate _next;

        public IsTokenBannedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UnusedActiveTokenRepository unusedTokenRepos)
        {
            //Token tokenResponse = (Token)_userService.GetCurrentTokenInfos();
            string tokenResponse = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (tokenResponse != null && tokenResponse != "")
            {
                List<string> unusedTokens = new();
                var allUnusedTokens = await unusedTokenRepos.GetAllAsync();
                allUnusedTokens.ForEach(t => unusedTokens.Add(t.JwtToken));
                if (unusedTokens.Contains(tokenResponse))
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            await _next(context);
        }
    }
}
