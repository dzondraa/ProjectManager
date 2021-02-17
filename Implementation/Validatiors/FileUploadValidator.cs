using Application.Queries;
using Application.Requests;
using AzureTableDataAccess;
using FluentValidation;
using Implementation.Queries;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Implementation.Validatiors
{
    public class FileUploadValidator : AbstractValidator<FileRequest>
    {
        private readonly IQueryProject _queryProject;

        public FileUploadValidator(IQueryProject queryProject)
        {
            _queryProject = queryProject;
            RuleFor(x => x.ProjectName)
                .Must(p => _queryProject.Execute(new Api.Searches.ProjectSearch { Name = p }).TotalCount > 0)
                .WithMessage($"Could not reference the project with that name");
              
        }

    }
}
