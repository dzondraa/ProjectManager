using Application.DataTransfer;
using AzureTableDataAccess.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetTask : IQuery<TaskDto, TaskDto>
    {
    }
}
