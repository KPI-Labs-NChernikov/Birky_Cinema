using AutoMapper;
using Business.Models;
using Data.Entities;
using System.Linq;

namespace Business
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
