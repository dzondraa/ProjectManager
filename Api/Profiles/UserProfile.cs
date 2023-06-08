using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using System;
using System.Linq;

namespace Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                x => x.UserRole, 
                opt => opt.MapFrom(src => src.UserRole.Select(x => new RoleDto
                {
                    Name = x.Role.Name,
                    Id = x.Role.Id
                }).ToList()));
        }
    }
}
