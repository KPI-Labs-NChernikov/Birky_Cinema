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
    public class ReviewService : IReviewService
    {
        private readonly ApplicationContext _context;

        private readonly IMapper _mapper;

        private const short _maxReviewSize = 400;

        public ReviewService(ApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null");
        }

        public async Task AddAsync(ReviewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ArgumentNullException(nameof(model), "Model's text cannot be null or empty");
            if (string.IsNullOrEmpty(model.UserId))
                throw new ArgumentNullException(nameof(model), "Model's author id cannot be null or empty");
            if (model.Text.Length > _maxReviewSize)
                throw new ArgumentException($"Review cannot be bigger than {_maxReviewSize} characters", nameof(model));
            await _context.Reviews.AddAsync(_mapper.Map<Review>(model));
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var model = await _context.Reviews.FindAsync(id);
            if (model is null)
                throw new InvalidOperationException("Model with such an id was not found");
            _context.Reviews.Remove(model);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<ReviewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<ReviewModel>>(_context.Reviews);
        }

        public async Task<ReviewModel> GetByIdAsync(int id)
        {
            return _mapper.Map<ReviewModel>(await _context.Reviews.FindAsync(id));
        }

        public async Task<IEnumerable<ReviewModel>> GetMovieReviewsAsync(int movieId)
        {
            return await Task.Run(() => _mapper.Map<IEnumerable<ReviewModel>>(_context.Reviews)
            .Where(r => r.MovieId == movieId));
        }

        public async Task UpdateAsync(ReviewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model), "Model cannot be null");
            if (string.IsNullOrEmpty(model.Text))
                throw new ArgumentNullException(nameof(model), "Model's text cannot be null or empty");
            if (string.IsNullOrEmpty(model.UserId))
                throw new ArgumentNullException(nameof(model), "Model's author id cannot be null or empty");
            if (model.Text.Length > _maxReviewSize)
                throw new ArgumentException($"Review cannot be bigger than {_maxReviewSize} characters", nameof(model));
            var existingModel = await _context.Reviews.SingleOrDefaultAsync(v => v.Id == model.Id);
            if (existingModel is null)
                throw new ArgumentException("Model was not found", nameof(model));
            existingModel = _mapper.Map(model, existingModel);
            _context.Reviews.Update(existingModel);
            await _context.SaveChangesAsync();
        }
    }
}
