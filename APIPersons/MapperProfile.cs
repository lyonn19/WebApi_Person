using APIPersons.Entity;
using APIPersons.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPersons
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PersonEntity, PersonModel>()
                .ReverseMap();
        }
    }
}
