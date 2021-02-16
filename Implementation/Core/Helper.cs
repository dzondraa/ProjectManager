using Application.DataTransfer;
using AzureTableDataAccess.Entities;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Management.Storage.Fluent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Implementation.Core
{
    public static class Helper
    {
        public static dynamic toEntityValue(JsonElement value)
        {
            var type = value.ValueKind;
            switch (type)
            {
                case JsonValueKind.Number:
                    return value.GetDouble();
                case JsonValueKind.String:
                    return value.GetString();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
            }
            return null;
        }

        public static TaskDto toTaskDto(DynamicTableEntity result)
        {
            var dto = new TaskDto
            {
                Id = result.PartitionKey + "$" + result.RowKey,
                ProjectId = result.PartitionKey,
                Timestamp = result.Timestamp.ToString(),
                Description = result.Properties["Description"].ToString(),
                Name = result.Properties["Name"].ToString()
            };
            result.Properties.Remove("Description");
            result.Properties.Remove("Name");
            result.Properties.Remove("Deleted");
            var additionalFields = new List<DataModel>();
            if (result.Properties.Count() > 0)
            {
                foreach (var x in result.Properties)
                {
                    dynamic value = null;
                    switch (x.Value.PropertyType)
                    {
                        case EdmType.Boolean:
                            value = x.Value.BooleanValue;
                            break;
                        case EdmType.Double:
                            value = x.Value.DoubleValue;
                            break;
                        case EdmType.DateTime:
                            value = x.Value.DateTimeOffsetValue.ToString();
                            break;
                        case EdmType.String:
                            value = x.Value.StringValue;
                            break;
                    }
                    if (x.Value.PropertyType == EdmType.Double) value = x.Value.DoubleValue;
                    additionalFields.Add(new DataModel
                    {
                        Name = x.Key,
                        Value = value
                    });
                }
            }
            dto.AdditionalFields = additionalFields;
            return dto;
        }

        // Mapping dynamic entity using reflection
        //public static TaskDto TaskToDto(Tasks taskEntity)
        //{ 
        //    TaskDto target = new TaskDto();
        //    var entityProperties = taskEntity.GetType().GetProperties();
        //    var dtoProperties = target.GetType().GetProperties();
        //    var entityPropertiesNames = new List<string>();
        //    var dtoPropertiesNames = new List<string>();


        //    foreach (var x in entityProperties)
        //    {
        //        entityPropertiesNames.Add(x.Name);
        //    }

        //    foreach (var x in dtoProperties)
        //    {
        //        dtoPropertiesNames.Add(x.Name);
        //    }

        //    var intersect = dtoPropertiesNames.Intersect(entityPropertiesNames);
        //    var addFields = new List<DataModel>();

        //    foreach(var x in intersect)
        //    {
        //        addFields.Add(new DataModel { Name = x, Value = (JsonElement)taskEntity.GetType().GetProperty(x).GetValue(taskEntity)} );
        //    }

        //    target.AdditionalFields = addFields;


        //    return target;
        //}
    }
}
