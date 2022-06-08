using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helper
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParameterInResponse <T>(this HttpContext httpContext,
            IQueryable<T> queryable, int recordPerPage)
        {
            if(httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }
            double count = await queryable.CountAsync();
            double totalAmountPage = Math.Ceiling(count / recordPerPage);
            httpContext.Response.Headers.Add("totalAmountPage", totalAmountPage.ToString());
        }
    }
}
