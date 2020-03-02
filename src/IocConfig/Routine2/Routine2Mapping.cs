using AutoMapper;
using Tochal.Core.DomainModels.DTO.Routine2;
using Tochal.Core.DomainModels.Entity.Routine2;
using Tochal.Core.DomainModels.ViewModel.Routine2;
using System;
using System.Collections.Generic;
using System.Text;

namespace IocConfig.Routine2
{
    public class Routine2Mapping : Profile
    {
        public Routine2Mapping()
        {

            // Routine2
            CreateMap<CreateRoutine2LogViewModel, Routine2Log>();
            CreateMap<Tochal.Core.DomainModels.Entity.Routine2.Routine2, Routine2FullDTO>();
            CreateMap<Routine2Role, Routine2RoleDTO>();
            CreateMap<Routine2Step, Routine2StepDTO>();
            CreateMap<Routine2Action, Routine2ActionDTO>();


        }
    }
}
