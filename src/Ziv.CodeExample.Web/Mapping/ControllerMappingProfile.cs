using AutoMapper;
using Ziv.CodeExample.Dto;
using Ziv.CodeExample.Entities;
using Ziv.CodeExample.Web.Contracts;

namespace Ziv.CodeExample.Web.Mapping
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, CommentModel>();
            CreateMap<CommentModel, CommentDto>();
            CreateMap<CommentDto, Comment>();
            
            CreateMap<User, UserDto>();
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}