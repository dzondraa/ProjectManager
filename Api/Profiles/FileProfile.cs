using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;

namespace Api.Profiles
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<File, AttachmentDto>().ReverseMap();
        }
    }
}
