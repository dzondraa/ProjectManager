using Application.Commands;
using Application.DataTransfer;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    class TableCliDeleteProjectAsync : IDeleteProjectAsync
    {
        private readonly TableCli _tableCli;
        public int Id => 2;

        public string Name => "Async deleting project using TableCli";

        public TableCliDeleteProjectAsync()
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
        }

        public async Task Execute(ProjectDto request)
        {
            await _tableCli.MergeEntityAsync(new Project("partition1", request.Id, request.Name));
        }
    }
}
