using DL;
using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Lost_Found
{
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;
        Lost_FindContext lost_FindContext;
        public RatingMiddleware(RequestDelegate next)
        {
            _next = next; 
        }

        public async Task Invoke(HttpContext httpContext, Lost_FindContext lost_FindContext)
        {
            this.lost_FindContext = lost_FindContext;
            Rating r = new Rating
            {
                Host = httpContext.Request.Host.ToString(),
                Method = httpContext.Request.Method,
                Path = httpContext.Request.Path,
                Referer = httpContext.Request.Headers["Referer"],
                UserAgent= httpContext.Request.Headers["User_Agent"].ToString(),
                RecordDate= DateTime.Now
            };

            await lost_FindContext.Ratings.AddAsync(r);
            await lost_FindContext.SaveChangesAsync();
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
