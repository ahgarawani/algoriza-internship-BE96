using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Domain.Entities;
using Vezeeta.Infrastructure.Identity;

namespace Vezeeta.Infrastructure.Mappings
{
    public class UserProfile: Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => $"{src.Email}"
                ));
        }
    }
}
