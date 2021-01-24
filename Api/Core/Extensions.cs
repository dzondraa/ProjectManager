using Api.Searches;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class Extensions
    {
        // T -> DTO object
        // L -> Paged response object - formated for representation
        public static PagedResponse<T> ToPagedResponse<T, L>(this IQueryable<L> query, PagedSearch search, [FromServices] IMapper mapper)
        {
            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<L, T>();
            //});
            //var mapper = new Mapper(config

            var skipCount = search.PerPage * (search.Page - 1);
            var items = query.Skip(skipCount).Take(search.PerPage).ToList();

            var response = new PagedResponse<T>
            {
                CurrentPage = search.Page,
                ItemsPerPage = search.PerPage,
                TotalCount = query.Count(),
                Items = mapper.Map<IEnumerable<L>, IEnumerable<T>>(items)

            };

            return response;
        }


    }
}
