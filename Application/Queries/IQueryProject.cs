using Api.Searches;
using Application.DataTransfer;
using AzureTableDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IQueryProject : IQuery<ProjectSearch, Task<PagedResponse<Project>>>
    {
    }
}
