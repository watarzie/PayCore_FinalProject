﻿using AutoMapper;
using Data.Model;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, User>().ReverseMap();
        }
    }
}
