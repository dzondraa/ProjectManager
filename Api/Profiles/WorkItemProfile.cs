using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;

namespace Api.Profiles
{
    public class WorkItemProfile : Profile
    {
        public WorkItemProfile()
        {
            CreateMap<WorkItem, WorkItemDto>()
                .ForMember(w => w.Project, x => x.MapFrom(x => x.Project))
                .ForMember(w => w.Type, x => x.MapFrom(x => x.WorkItemType))
                .ReverseMap();
        }
    }

    public class WorkItemTypeProfile : Profile
    {
        public WorkItemTypeProfile()
        {
            CreateMap<WorkItemType, WorkItemTypeDto>().ReverseMap();
        }
    }

    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectProfile, ProjectDto>().ReverseMap();
        }
    }
}
