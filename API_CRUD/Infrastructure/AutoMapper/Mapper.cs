using API_CRUD.Model.Concrete;
using API_CRUD.Models.DTOs;
using API_CRUD.Models.VMs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.AutoMapper
{
    public class Mapper:Profile
    {
        public Mapper()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCatgoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryVM>().ReverseMap();
            CreateMap<GetCategoryVM, Category>().ReverseMap();

            CreateMap<AppUser, AuthenticationDTO>().ReverseMap();
        }

    }
}
