using Application.DataTransfer;
using AzureTableDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetTask : IQuery<TaskDto, PagedResponse<Tasks>>
    {
    }
}
