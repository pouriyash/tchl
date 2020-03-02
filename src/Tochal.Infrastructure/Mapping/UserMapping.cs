using AutoMapper; 
using Tochal.Core.DomainModels.DTO.User; 
using Tochal.Core.DomainModels.Entity.Identity; 
using Tochal.Core.DomainModels.ViewModel.Identity; 
using System;
using System.Collections.Generic;
using System.Text;
using Tochal.Core.DomainModels.Entity;

namespace Tochal.Infrastructure.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {

            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<UserEditViewModel, User>().ReverseMap();
            CreateMap<UserSummary, User>().ReverseMap();
            CreateMap<EditUserInfoViewModel, User>().ReverseMap();
            CreateMap<CommentViewModel, Comment>().ReverseMap();
       
        }
    }
}
