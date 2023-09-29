namespace Videoclub.API.Middleware;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex) //if any exception is catched, a bad request is thrown with the proper message
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "text/plain"; //the content type is set to plain text to return only the message
            var errorMessage = ex.Message; 
            await context.Response.WriteAsync(errorMessage);
        }
    }
}
