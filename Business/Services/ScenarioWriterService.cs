using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ScenarioWriterService : IScenarioWriterService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public ScenarioWriterService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public Task AddAsync(ScenarioWriterModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieToScenarioWriterAsync(ScenarioWriterModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieFromScenarioWriterAsync(int writerId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ScenarioWriterModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ScenarioWriterModel>>(_context.ScenarioWriters.Include(m => m.Movies));
        }

        public Task<ScenarioWriterModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ScenarioWriterModel>> GetMovieScenarioWritersAsync(int movieId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<ScenarioWriterModel>>(_context.ScenarioWriters
                .Where(m => m.Movies.Select(i => i.Id).Contains(movieId)).Include(m => m.Movies)));
        }

        public Task UpdateAsync(ScenarioWriterModel model)
        {
            throw new NotImplementedException();
        }
    }
}
