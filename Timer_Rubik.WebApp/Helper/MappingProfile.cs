﻿using AutoMapper;
using Timer_Rubik.WebApp.Dto;
using Timer_Rubik.WebApp.Models;

namespace Timer_Rubik.WebApp.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
        }
    }
}