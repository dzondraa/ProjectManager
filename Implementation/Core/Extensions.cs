using Api.Searches;
using Application.DataTransfer;
using Application.Queries;
using AutoMapper;
using AzureTableDataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
