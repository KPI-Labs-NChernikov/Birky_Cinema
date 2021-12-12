using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IActorService : ICRUD<ActorModel>
    {
        Task AddMovieToActorAsync(ActorModel model, string userId);

        Task DeleteMovieFromActorAsync(int genreId, string userId);
    }
}
