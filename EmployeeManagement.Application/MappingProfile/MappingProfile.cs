using AutoMapper;
using EmployeeManagement.Application.Dtos;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.MappingProfile
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Department Mapping
            CreateMap<Department, DepartmentDto>();
            CreateMap<CreateDepartmentDto, Department>();
            CreateMap<UpdateDepartmentDto, Department>();

            //Employee Mapping
            CreateMap<ApplicationUser, EmployeeDto>().ForMember(d => d.Phone, opt => opt.MapFrom(src => src.PhoneNumber));
            CreateMap<CreateEmployeeDto, ApplicationUser>();
            CreateMap<UpdateEmployeeDto, ApplicationUser>();
            CreateMap<LoginDto, ApplicationUser>()
                .ForMember(d=>d.PasswordHash,opt=>opt.MapFrom(src=>src.Password));

          //  CreateMap<RegisterDto, ApplicationUser>()
          //.ForMember(d => d.PasswordHash, opt => opt.MapFrom(src => src.Password))
          //.ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
          //.ForMember(d => d.UserName, opt => opt.MapFrom(src => src.Name));
          

        }
    }
}
