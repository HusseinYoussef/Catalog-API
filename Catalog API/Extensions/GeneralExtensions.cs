using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Catalog_API.Extensions
{
    public static class GeneralExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Single(c => c.Type == "Id").Value;
        }
    }
}