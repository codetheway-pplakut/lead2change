using System.Collections.Generic;
using Lead2Change.Domain.Models;

using System.Threading.Tasks;

namespace Lead2Change.Repositories.QuestionInInterviews
{
    public interface IQuestionInInterviewRepository
    {
        public Task<List<QuestionInInterview>> GetQuestionInInterviews();
        public Task<QuestionInInterview> Update(QuestionInInterview model);
        public Task<QuestionInInterview> Create(QuestionInInterview model);
        public Task<QuestionInInterview> Delete(QuestionInInterview model);



    }
}
