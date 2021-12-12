using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IDirectorService : ICRUD<DirectorModel>
    {
        Task AddMovieToDirectorAsync(DirectorModel model, string userId);

        Task DeleteMovieFromDirectorAsync(int directorId, string userId);

        Task<IEnumerable<DirectorModel>> GetMovieDirectorsAsync(int movieId);
    }
}
