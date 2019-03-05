using AngularWithCoreDemo.Models;
using AngularWithCoreDemo.Models.WebApi;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularWithCoreDemo.MapperProfiles
{
    public class ReverseMappingProfile : Profile
    {
        public ReverseMappingProfile()
        {
            CreateMap<RegisterUserBindingModel, ApplicationUser>().ReverseMap();
        }
    }
}
