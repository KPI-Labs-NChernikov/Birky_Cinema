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

            CreateMap<Actor, ActorModel>()
                .ForMember(p => p.MovieIds, c => c.MapFrom(v => v.Movies.Select(m => m.Id)))
                .ReverseMap();

            CreateMap<Director, DirectorModel>()
                .ForMember(p => p.MovieIds, c => c.MapFrom(v => v.Movies.Select(m => m.Id)))
                .ReverseMap();

            CreateMap<ScenarioWriter, ScenarioWriterModel>()
                .ForMember(p => p.MovieIds, c => c.MapFrom(v => v.Movies.Select(m => m.Id)))
                .ReverseMap();

            CreateMap<ReviewModel, ReviewModel>()
                .ReverseMap();

            CreateMap<Country, CountryModel>()
                .ForMember(p => p.MovieIds, c => c.MapFrom(v => v.Movies.Select(m => m.Id)))
                .ReverseMap();

            CreateMap<Row, RowModel>()
                .ForMember(p => p.SeatIds, c => c.MapFrom(v => v.Seats.Select(m => m.Id)))
                .ReverseMap();

            CreateMap<Seat, SeatModel>()
                .ForMember(p => p.SessionSeatModels, c => c.MapFrom(v => v.SessionSeats.Select(m => new SessionSeatModel()
                { 
                    SeatId = m.SeatId,
                    SessionId = m.SessionId,
                    UserId = m.UserId
                })))
                .ReverseMap();

            CreateMap<SessionSeat, SessionSeatModel>()
                .ReverseMap();

            CreateMap<Session, SessionModel>()
                .ForMember(p => p.SessionSeatModels, c => c.MapFrom(v => v.SessionSeats.Select(m => new SessionSeatModel()
                {
                    SeatId = m.SeatId,
                    SessionId = m.SessionId,
                    UserId = m.UserId
                })))
                .ReverseMap();

            CreateMap<Movie, MovieModel>()
                .ForMember(p => p.UserIds, c => c.MapFrom(v => v.Users.Select(m => m.Id)))
                .ForMember(p => p.ScenarioWriterIds, c => c.MapFrom(v => v.ScenarioWriters.Select(m => m.Id)))
                .ForMember(p => p.ActorIds, c => c.MapFrom(v => v.Actors.Select(m => m.Id)))
                .ForMember(p => p.DirectorIds, c => c.MapFrom(v => v.Directors.Select(m => m.Id)))
                .ForMember(p => p.ReviewIds, c => c.MapFrom(v => v.Reviews.Select(m => m.Id)))
                .ForMember(p => p.SessionIds, c => c.MapFrom(v => v.Sessions.Select(m => m.Id)))
                .ForMember(p => p.GenreIds, c => c.MapFrom(v => v.Genres.Select(m => m.Id)))
                .ReverseMap();
        }
    }
}
