using Application.DataTransfer;
using AutoMapper;
using Domain.Entities;
using System;

namespace Api.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleDto, Role>().ReverseMap();
        }
    }
}
