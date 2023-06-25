﻿using noteCodeAPI.Models;
using noteCodeAPI.Repositories;
using noteCodeAPI.Services;

namespace noteCodeAPI.Middlewares
{
    public class IsTokenBannedMiddleware
    {
        private readonly RequestDelegate _next;
        private UnusedActiveTokenRepository _unusedTokenRepos;
        private UserAppService _userService;

        public IsTokenBannedMiddleware(RequestDelegate next, UnusedActiveTokenRepository unusedTokenRepos, UserAppService userService)
        {
            _next = next;
            _unusedTokenRepos = unusedTokenRepos;
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Token tokenResponse = (Token)_userService.GetCurrentTokenInfos();
            string tokenResponse = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (tokenResponse != null && tokenResponse != "")
            {
                List<string> unusedTokens = new();
                var allUnusedTokens = await _unusedTokenRepos.GetAllAsync();
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
