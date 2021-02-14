using Application.DataTransfer;
using Application.Requests;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Profiles
{
    public class ProjectRequestProfile : Profile
    {
        public ProjectRequestProfile()
        {
            CreateMap<ProjectRequest, ProjectDto>()
                .ReverseMap();

        }
    }
}
