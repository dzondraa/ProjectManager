using Application.Commands;
using Application.DataTransfer;
using AzureTableDataAccess;
using AzureTableDataAccess.Entities;
using FluentValidation;
using Implementation.Validatiors;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents.SystemFunctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Implementation.Core;

namespace Implementation.Commands
{
    public class TableCliCreateTaskAsync : ICreateTaskCommandAsync
    {
        private readonly TableCli _tableCli;
        private readonly TaskRequestValidatior _validatior;

        public TableCliCreateTaskAsync(TaskRequestValidatior validator)
        {
            _tableCli = new TableCli(AzureStorageConnection.Instance(), "Tasks");
            _validatior = validator;
        }

        public int Id => 221;

        public string Name => "Create task asyncronously";


        public async Task Execute(TaskDto request)
        {
            _validatior.ValidateAndThrow(request);
            var guid = Guid.NewGuid().ToString();
            var hasAdditionalFields = request.AdditionalFields != null;
            if (hasAdditionalFields)
            {
                var dynTableEntity = new DynamicTableEntity(request.ProjectId, guid);

                dynTableEntity.Properties.Add("Description", new EntityProperty(request.Description));
                dynTableEntity.Properties.Add("Name", new EntityProperty(request.Name));

                foreach (var prop in request.AdditionalFields)
                {
                    // Better approach:
                    // - Using reflection
                    // - Create class/method which is able to create DynamicTableEntity from TableEntity and just add addidional fields
                    dynTableEntity.Properties.Add(prop.Name, new EntityProperty(Helper.toEntityValue(prop.Value)));
                }

                await _tableCli.InsertDynamicEntity(dynTableEntity);

            } else
            {
                await _tableCli.InsertOrMergeEntityAsync(new Tasks
                {
                    PartitionKey = request.ProjectId,
                    RowKey = guid,
                    Description = request.Description,
                    Name = request.Name
                });
            }
            
        }
    }
}
