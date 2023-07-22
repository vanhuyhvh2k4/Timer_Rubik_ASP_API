﻿using AutoMapper;
using Timer_Rubik.WebApp.Authorize.General.DTO;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Authorize.General.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //account
            CreateMap<Account, RegisterRequest>();
            CreateMap<RegisterRequest, Account>();

            CreateMap<Account, GetAccountDTO>();
            CreateMap<GetAccountDTO, Account>();

            //category 
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<GetCategoryDTO, Category>();
        }
    }
}
