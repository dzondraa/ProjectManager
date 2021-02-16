using Application.Commands;
using Application.DataTransfer;
using Application.Requests;
using AutoMapper;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using FluentValidation;
using Implementation.Core;
using Implementation.Validatiors;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Commands
{
    public class TableCliUpdateTaskAsync : IUpdateTaskAsync
    {
        private readonly TableCli _tableCli;
        private readonly IMapper _mapper;
        private readonly TaskRequestValidatior _validator;


        public TableCliUpdateTaskAsync(IMapper mapper, TaskRequestValidatior validator)
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks"); ;
            _mapper = mapper;
            _validator = validator;
        }

        public int Id => 26;

        public string Name => "Updating project record using Table Storage API";

        public async Task Execute(TaskRequest request)
        {
            //// mapping to DataAccess object
            //var projectEntity = _mapper.Map<Tasks>(request);
            //await _tableCli.MergeEntityAsync(projectEntity);
            _validator.ValidateAndThrow(request);
            var guid = Guid.NewGuid().ToString();
            var hasAdditionalFields = request.AdditionalFields != null;
            if (hasAdditionalFields)
            {
                var dynTableEntity = new DynamicTableEntity(request.ProjectId, guid);
                dynTableEntity.ETag = "*";

                dynTableEntity.Properties.Add("Description", new EntityProperty(request.Description));
                dynTableEntity.Properties.Add("Name", new EntityProperty(request.Name));
                dynTableEntity.Properties.Add("Deleted", new EntityProperty(false));
                dynTableEntity.Properties.Add("ETag", new EntityProperty("*"));

                foreach (var prop in request.AdditionalFields)
                {
                    // Better approach:
                    // - Using reflection
                    // - Create class/method which is able to create DynamicTableEntity from TableEntity and just add addidional fields
                    dynTableEntity.Properties.Add(prop.Name, new EntityProperty(Helper.toEntityValue(prop.Value)));
                }

                await _tableCli.InsertOrMergeDynamicEntity(dynTableEntity);

            }
            else
            {
                await _tableCli.InsertOrMergeEntityAsync(new Tasks
                {
                    ETag = "*",
                    PartitionKey = request.ProjectId,
                    RowKey = guid,
                    Description = request.Description,
                    Name = request.Name,
                    Deleted = false
                });
            }
        }
    }
}
