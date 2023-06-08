using Api.Searches;
using Application.Queries;
using AutoMapper;
using Microsoft.Azure.KeyVault.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Implementation.Core
{
    public static class Extensions
    {

        // T -> DTO object
        // L -> Paged response object - formated for representation
        public static PagedResponse<T> ToPagedResponse<T>(this List<T> items)
        {

            var response = new PagedResponse<T>
            {
                TotalCount = items.Count(),
                Items = items

            };

            return response;
        }

        public static PagedResponse<TResponse> ToPagedResponse<TResponse, TQueryEntity>(
            this IQueryable<TQueryEntity> query, 
            PagedSearch search, 
            IMapper mapper,
            Action<List<TQueryEntity>> computeData = null)
        {
            var queryString = query.ToString();
            var data = query.Skip((search.PageNumber - 1) * search.PageSize).Take(search.PageSize).ToList();
            
            if(!(computeData is null))
                computeData(data);
            
            return new PagedResponse<TResponse>
            {
                Items = mapper.Map<List<TResponse>>(data),
                TotalCount = query.Count(),
            };
        }

    }
}
