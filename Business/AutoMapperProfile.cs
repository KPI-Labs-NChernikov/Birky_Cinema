using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    using AutoMapper;
    using DAL.Entities;
    using BLL.Models;
    using System.Linq;
    using Data.Entities;
    using Business.Models;

    namespace BLL
    {
        public class AutoMapperProfile : Profile
        {
            public AutoMapperProfile()
            {
                CreateMap<Genre, GenreModel>()
                    .ForMember(p => p.MovieIds, c => c.MapFrom(v => v.Movies.Select(m => m.Id)))
                    .ForMember(p => p.UserIds, c => c.MapFrom(v => v.Users.Select(m => m.Id)))
                    .ReverseMap();
            }
        }
    }
}
