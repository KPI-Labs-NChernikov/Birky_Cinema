using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if (genre is null)
                throw new ArgumentNullException(nameof(genre), "Model cannot be null");
            if (string.IsNullOrEmpty(genre.Name))
                throw new ArgumentNullException(nameof(genre), "Model's name cannot be null or empty");
            if (genre.MovieIds is null)
                throw new ArgumentNullException(nameof(genre), "Model's Movie Ids cannot be null or empty");
            var existingModel = await _context.Genres
                .Include(g => g.Movies)
                .Include(g => g.Users)
                .SingleOrDefaultAsync(v => v.Id == genre.Id);
            if (existingModel is null)
                throw new ArgumentException("Model was not found", nameof(genre));
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie is null)
                throw new ArgumentException("Movie with such an Id was not found", nameof(movieId));
            existingModel.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task AddUserToGenreAsync(GenreModel genre, string userId)
        {
            if (genre is null)
                throw new ArgumentNullException(nameof(genre), "Model cannot be null");
            if (string.IsNullOrEmpty(genre.Name))
                throw new ArgumentNullException(nameof(genre), "Model's name cannot be null or empty");
            if (genre.MovieIds is null)
                throw new ArgumentNullException(nameof(genre), "Model's Movie Ids cannot be null or empty");
            var existingModel = await _context.Genres
                .Include(g => g.Movies)
                .Include(g => g.Users)
                .SingleOrDefaultAsync(v => v.Id == genre.Id);
            if (existingModel is null)
                throw new ArgumentException("Model was not found", nameof(genre));
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                throw new ArgumentException("User with such an Id was not found", nameof(userId));
            existingModel.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var model = await _context.Genres.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            _context.Genres.Remove(model);
            await _context.SaveChangesAsync();
        }

        public Task DeleteMovieFromGenreAsync(int genreId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserFromGenreAsync(int genreId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GenreModel> GetAll()
        {
            return _mapper.Map<IEnumerable<GenreModel>>(_context.Genres.Include(g => g.Movies).Include(g => g.Users));
        }

        public async Task<GenreModel> GetByIdAsync(int id)
        {
            return _mapper.Map<GenreModel>(await _context.Genres
                .Include(g => g.Movies)
                .Include(g => g.Users)
                .SingleOrDefaultAsync(v => v.Id == id));
        }

        public async Task UpdateAsync(GenreModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null");
            if (string.IsNullOrEmpty(model.Name))
                throw new ArgumentNullException(nameof(model), "Model's name cannot be null or empty");
            var existingModel = await _context.Genres
                .Include(g => g.Movies)
                .Include(g => g.Users)
                .SingleOrDefaultAsync(v => v.Id == model.Id);
            if (existingModel is null)
                throw new ArgumentException("Model was not found", nameof(model));
            existingModel = _mapper.Map(model, existingModel);
            _context.Genres.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
