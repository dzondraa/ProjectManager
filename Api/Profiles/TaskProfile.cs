using Application.DataTransfer;
using AutoMapper;
using AzureTableDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDto, Tasks>()
                .ForMember(dest =>
                    dest.PartitionKey,
                    opt => opt.MapFrom(src => src.ProjectId
                    ))
                .ForMember(dest =>
                    dest.RowKey,
                    opt => opt.MapFrom(src => src.Id
                    ))
                .ReverseMap();

        }
    }
}
