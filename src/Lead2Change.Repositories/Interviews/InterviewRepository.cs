﻿using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.Interviews
{
    public class InterviewRepository : _BaseRepository, IInterviewRepository
    {
        private AppDbContext AppDbContext;

        public InterviewRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<List<Interview>> GetInterviews()
        {
            return await this.AppDbContext.Interviews.ToListAsync();
        }
        public async Task<Interview> GetInterview(Guid id)
        {
            return await this.AppDbContext.Interviews.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Interview> Update(Interview model)
        {
            var result = AppDbContext.Interviews.Update(model);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Interview> Create(Interview interview)
        {
            var result = await this.AppDbContext.AddAsync(interview);
            await this.AppDbContext.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<Interview> Delete(Interview model)
        {
            AppDbContext.Interviews.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
    }
}
