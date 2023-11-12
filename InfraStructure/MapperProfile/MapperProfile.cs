using AutoMapper;
using DomainModels;
using InfraStructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.MapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Student, StudentCreateVm>().ReverseMap()
           .ForMember(dest => dest.Id, opt => opt.Ignore()).ForMember(dest => dest.Marks, opt => opt.Ignore());
            
        }
    }
}
