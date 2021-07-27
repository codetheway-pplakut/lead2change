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
    public interface IInterviewService
    {
        public Task<Interview> GetInterview(Guid id);
        public Task<Interview> Update(Interview model);
        public Task<Interview> Create(Interview interview);
        public Task<List<Interview>> GetInterviews();
        public Task<Interview> Delete(Interview interview);
        public Task<List<QuestionInInterview>> GetInterviewAndQuestions(Guid interviewId);
        public Task Save();



    }
}
