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
        Task AddUserToGenreAsync(GenreModel model, string userId);

        Task AddMovieToGenreAsync(GenreModel model, int movieId);

        Task DeleteUserFromGenreAsync(int genreId, string userId);

        Task DeleteMovieFromGenreAsync(int genreId, int movieId);
    }
}
