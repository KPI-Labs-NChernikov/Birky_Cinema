using Business.Models;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGenreService : ICRUD<GenreModel>
    {
        Task AddUserToGenreAsync(GenreModel genre, string userId);

        Task AddMovieToGenreAsync(GenreModel genre, int movieId);
    }
}
