using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
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
            if (genre.UserIds is null)
                throw new ArgumentNullException(nameof(genre), "Model's User Ids cannot be null or empty");
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

        public async Task DeleteMovieFromGenreAsync(int genreId, int movieId)
        {
            var model = await _context.Genres
                .Include(g => g.Movies)
                .SingleOrDefaultAsync(v => v.Id == genreId);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie is null)
                throw new InvalidOperationException("Movie with such an id was not found");
            model.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserFromGenreAsync(int genreId, string userId)
        {
            var model = await _context.Genres
                .Include(g => g.Movies)
                .SingleOrDefaultAsync(v => v.Id == genreId);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            var user = await _context.Users.FindAsync(userId);
            if (user is null)
                throw new InvalidOperationException("User with such an id was not found");
            model.Users.Remove(user);
            await _context.SaveChangesAsync();
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

        public async Task<IEnumerable<GenreModel>> GetMovieGenresAsync(int movieId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<GenreModel>>(
                _context.Genres
                .Include(g => g.Movies)
                .Include(g => g.Users)
                .Where(r => r.Movies.Select(m => m.Id).Contains(movieId))));
        }

        public string GetNameForLang(GenreModel model, string lang)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null");
            var name = lang switch
            {
                "eng" => model.NameENG,
                "ru" => model.NameRU,
                _ => model.Name,
            };
            return name;
        }

        public async Task<IEnumerable<GenreModel>> GetUserGenresAsync(string userId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<GenreModel>>(
                _context.Genres
                .Include(g => g.Movies)
                .Include(g => g.Users)
                .Where(r => r.Users.Select(m => m.Id).Contains(userId))));
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
