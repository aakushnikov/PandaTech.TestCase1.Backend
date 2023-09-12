using System.Text;
using Microsoft.AspNetCore.Http;

namespace PandaTech.TestCase1.Extensions;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (ConfirmCredentials(context))
            await _next.Invoke(context); 
        else
            context.Response.StatusCode = 401;
    }
    
    
    private static bool ConfirmCredentials(HttpContext context)
    {
        if (context.Request.Path.HasValue)
            if (!context.Request.Path.Value.Contains("api", StringComparison.OrdinalIgnoreCase))
                return true;
        
        var authHeader = context.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authHeader))
            return false;

        if (!authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
            return false;

        var encodedCredentials = authHeader["Basic ".Length..].Trim();
        var credentials = Encoding
            .GetEncoding("iso-8859-1")
            .GetString(Convert.FromBase64String(encodedCredentials))
            .Split(':');

        if (credentials.Length != 2)
            return false;

        var username = credentials.First();
        var password = credentials.Last();

        if (username != "test" || password != "test")
            return false;

        return true;
    }
}