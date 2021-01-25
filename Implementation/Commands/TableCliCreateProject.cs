using Application.Commands;
using Application.DataTransfer;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class TableCliCreateProject : ICreateProjectCommandAsync
    {
        private readonly TableCli _tableCli;
        
        public int Id => 1;

        public string Name => "Insert or merge operation using TableCli";

        public TableCliCreateProject()
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
        }

        public async Task Execute(ProjectDto request)
        {
            var date = DateTime.Now;
            var partitionId = date.Year.ToString() + "-" + date.Month.ToString();
            await _tableCli.InsertOrMergeEntityAsync(new Project(partitionId, Guid.NewGuid().ToString(), request.Name));
        }
    }
}
