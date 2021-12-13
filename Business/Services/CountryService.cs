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
    public class CountryService : ICountryService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        public CountryService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public Task AddAsync(CountryModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CountryModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<CountryModel> GetByIdAsync(int id)
        {
            var model = await _context.Countries.Include(c => c.Movies).SingleOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CountryModel>(model);
        }

        public Task UpdateAsync(CountryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
