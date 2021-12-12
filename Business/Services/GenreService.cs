using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public GenreService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public async Task AddAsync(GenreModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null");
            if (string.IsNullOrEmpty(model.Name))
                throw new ArgumentNullException(nameof(model), "Model's name cannot be null or empty");
            await _context.Genres.AddAsync(_mapper.Map<Genre>(model));
            await _context.SaveChangesAsync();
        }

        public async Task AddMovieToGenreAsync(GenreModel genre, int movieId)
        {
            throw new NotImplementedException();
        }

        public async Task AddUserToGenreAsync(GenreModel genre, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenreModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<GenreModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(GenreModel model)
        {
            throw new NotImplementedException();
        }
    }
}
