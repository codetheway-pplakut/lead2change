using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.Interviews
{
    public interface IInterviewRepository
    {
        public Task<List<Interview>> GetInterviews();
        public Task<Interview> GetInterview(Guid id);
        public Task<Interview> Update(Interview model);
        public Task<Interview> Create(Interview interview);
        public Task<Interview> Delete(Interview model);

        public Task<List<QuestionInInterview>> GetInterviewAndQuestions(Guid interviewId);

    }
}
