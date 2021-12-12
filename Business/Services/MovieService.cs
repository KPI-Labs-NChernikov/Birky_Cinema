﻿using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public MovieService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public Task AddAsync(MovieModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MovieModel> GetAll()
        {
            return _mapper.Map<IEnumerable<MovieModel>>(_context.Movies);
        }

        public async Task<MovieModel> GetByIdAsync(int id)
        {
            var model = await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Directors)
                .Include(m => m.Actors)
                .Include(m => m.ScenarioWriters)
                .Include(m => m.Reviews)
                .Include(m => m.Sessions)
                .SingleOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<MovieModel>(model);
        }

        public async Task<IEnumerable<MovieModel>> GetShortsAsync()
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<MovieModel>>(_context.Movies.Where(m => m.Length < 52)));
        }

        public Task UpdateAsync(MovieModel model)
        {
            throw new NotImplementedException();
        }
    }
}
