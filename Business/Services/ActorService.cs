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
    public class ActorService : IActorService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public ActorService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public Task AddAsync(ActorModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieToActorAsync(ActorModel model, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieFromActorAsync(int genreId, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ActorModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ActorModel>>(_context.Actors.Include(m => m.Movies));
        }

        public Task<ActorModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ActorModel>> GetMovieActorsAsync(int movieId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<ActorModel>>(_context.Actors
                .Where(m => m.Movies.Select(i => i.Id).Contains(movieId)).Include(m => m.Movies)));
        }

        public Task UpdateAsync(ActorModel model)
        {
            throw new NotImplementedException();
        }
    }
}
