using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public interface IQueryTask : IQuery<TaskSearch, PagedResponse<AzureTableDataAccess.Entities.Tasks>>
    {
    }
}
