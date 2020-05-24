﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheCodeCamp.Models;

namespace TheCodeCamp.Data.Profiles
{
    public class CampMappingProfile : Profile
    {
        public CampMappingProfile()
        {
            CreateMap<Camp, CampModel>();
        }
    }
}