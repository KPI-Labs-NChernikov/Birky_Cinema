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
    public class SessionService : ISessionService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public SessionService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public Task AddAsync(SessionModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SessionModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SessionModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SessionModel>> GetMovieSessionsAsync(int movieId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<SessionModel>>(_context.Sessions
                .Where(s => s.MovieId == movieId).Include(s => s.SessionSeats)));
        }

        public Task UpdateAsync(SessionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
