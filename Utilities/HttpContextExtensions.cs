using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace back_end.Utilities
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParamsHeaderPagination<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
        {
            if (httpContext is null)
            {
                throw new ArgumentException(nameof(HttpContext));
            }

            var amountRecords = await queryable.CountAsync();
            httpContext.Response.Headers.Add("totalAmountRecords", amountRecords.ToString());
        }
    }
}