using Application.DataTransfer;
using Application.Requests;
using AutoMapper;
using AzureTableDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Profiles
{
   
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDto, Project>()
                .ForMember(dest =>
                    dest.PartitionKey,
                    opt => opt.MapFrom(src => src.Id.Split('$', StringSplitOptions.None)[0]
                    ))
                .ForMember(dest =>
                    dest.RowKey,
                    opt => opt.MapFrom(src => src.Id.Split('$', StringSplitOptions.None)[1]
                    ));
            CreateMap<Project, ProjectDto>()
               .ForMember(dest =>
                   dest.Id,
                   opt => opt.MapFrom(src => src.PartitionKey + '$' + src.RowKey)
                   );
               


        }
    }
    
}
