using Application.DataTransfer;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using FluentValidation;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Validatiors
{
    public class TaskRequestValidatior : AbstractValidator<TaskDto>
    {
        private readonly TableCli tableCli;

        public TaskRequestValidatior()
        {
           tableCli =  new TableCli(AzureStorageConnection.Instance(), "Tasks");
            RuleFor(t => t.Name)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(25);

            RuleFor(t => t.Description)
                .NotEmpty()
                .MinimumLength(10)
                .MaximumLength(200);
            RuleFor(t => t.ProjectId)
                .Must(id => id.Contains("$"))
                .WithMessage("Not a valid ID")
                .DependentRules(() =>
                {
                    RuleFor(p => p).Must(p => tableCli.EntityExists(p.ProjectId.Split('$')[0], p.ProjectId.Split('$')[1]))
                   .WithMessage(p => $"Project with id '{p.ProjectId}' not found.");

                });
        }
    }
}
