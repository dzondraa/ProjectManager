using Application.Commands;
using Application.DataTransfer;
using AzureTableDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class TableCliCreateProject : ICreateProjectCommand
    {
        private readonly TableCli _tableCli;
        
        public int Id => 1;

        public string Name => "Insert or merge operation using TableCli";

        public TableCliCreateProject()
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Projects");
        }

        public void Execute(ProjectDto request)
        {
           
        }
    }
}
