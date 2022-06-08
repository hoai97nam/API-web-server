using MoviesAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helper
{
    public static class QueryabeExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable <T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordPerPage)
                .Take(pagination.RecordPerPage);
        }
    }
}
