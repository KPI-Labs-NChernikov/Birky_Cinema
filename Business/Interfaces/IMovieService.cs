using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMovieService : ICRUD<MovieModel>
    {
        Task<IEnumerable<MovieModel>> GetShortsAsync();

        Task<IEnumerable<MovieModel>> GetRecommendedAsync(string userId);

        Task<IEnumerable<MovieModel>> GetSimilarAsync(int movieId);
    }
}
