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
    public class DirectorService : IDirectorService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public DirectorService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public Task AddAsync(DirectorModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieToDirectorAsync(DirectorModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieFromDirectorAsync(int directorId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DirectorModel> GetAll()
        {
            return _mapper.Map<IEnumerable<DirectorModel>>(_context.Directors.Include(m => m.Movies));
        }

        public Task<DirectorModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DirectorModel>> GetMovieDirectorsAsync(int movieId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<DirectorModel>>(_context.Directors
                .Where(m => m.Movies.Select(i => i.Id).Contains(movieId)).Include(m => m.Movies)));
        }

        public Task UpdateAsync(DirectorModel model)
        {
            throw new NotImplementedException();
        }
    }
}
