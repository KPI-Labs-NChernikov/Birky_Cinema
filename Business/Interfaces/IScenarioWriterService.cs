using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IScenarioWriterService : ICRUD<ScenarioWriterModel>
    {
        Task AddMovieToScenarioWriterAsync(ScenarioWriterModel model, string userId);

        Task DeleteMovieFromScenarioWriterAsync(int writerId, string userId);

        Task<IEnumerable<ScenarioWriterModel>> GetMovieScenarioWritersAsync(int movieId);
    }
}
