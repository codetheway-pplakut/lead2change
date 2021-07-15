using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Goals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;
using Lead2Change.Repositories.Interviews;

namespace Lead2Change.Services.Interviews
{
    public class InterviewService : IInterviewService
    {
        private IInterviewRepository _interviewRepository;

        public InterviewService(AppDbContext dbContext)
        {
            this._interviewRepository = new InterviewRepository(dbContext);
        }

        public async Task<Interview> GetInterview(Guid id)
        {
            return await this._interviewRepository.GetInterview(id);
        }
        public async Task<Interview> Update(Interview model)
        {
            return await this._interviewRepository.Update(model);
        }
        public async Task<Interview> Create(Interview interview)
        {
            return await this._interviewRepository.Create(interview);
        }
        public async Task<List<Interview>> GetInterviews()
        {
            return await this._interviewRepository.GetInterviews();

        }
        public async Task<Interview> Delete(Interview interview)
        {
            return await this._interviewRepository.Delete(interview);
        }
    }
}
