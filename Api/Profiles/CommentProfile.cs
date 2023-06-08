using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;

namespace Api.Profiles
{
    public class CommentProfile : Profile
    {
    
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
        
    }
}
