using Application;
using Application.Commands;
using Application.DataTransfer;
using Application.Requests;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using FluentValidation;
using Implementation.Validatiors;
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
        private readonly ProjectRequestValidator _validator;
        
        public int Id => 1;

        public string Name => "Insert or merge operation using TableCli";

        public TableCliCreateProject(ProjectRequestValidator validator)
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
            _validator = validator;
        }

        public async System.Threading.Tasks.Task Execute(ProjectRequest request)
        {
            _validator.ValidateAndThrow(request);
            var date = DateTime.Now;
            var partitionId = date.Year.ToString() + "-" + date.Month.ToString();
            await _tableCli.InsertOrMergeEntityAsync(new Project(partitionId, Guid.NewGuid().ToString(), request.Name) { Deleted = false});
        }

    }
}
