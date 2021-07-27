using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Constants;
using Lead2Change.Domain.Models;
using Lead2Change.Domain.ViewModels;
using Lead2Change.Repositories.AppEvents;
using Lead2Change.Repositories.Coaches;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.Coaches
{
    public class CoachService : _BaseService, ICoachService
    {
        ICoachesRepository _coachRepo;

        public CoachService(AppDbContext dbContext) : base(dbContext)
        {
            _coachRepo = new CoachesRepository(dbContext);
        }

        public async Task<Coach> GetCoach(Guid id)
        {
            return await this._coachRepo.GetCoach(id);
        }

        public async Task<Coach> Create(Coach coach)
        {
            return await this._coachRepo.Create(coach);
        }
        public async Task<List<Coach>> GetCoaches()
        {
            return await this._coachRepo.GetCoaches();
        }
        public async Task<Coach> Delete(Coach coach)
        {
            return await _coachRepo.Delete(coach);
        }
        public async Task<Coach> Update(Coach coach)
        {
            return await this._coachRepo.Update(coach);
        }
        /*
        public async Task<Coach> AssignStudent(Student student)
        {
            return await this._coachRepo.AssignStudent(student);
        }
        */
    }
}