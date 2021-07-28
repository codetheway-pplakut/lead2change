using System.Collections.Generic;
using Lead2Change.Domain.Models;
using System.Threading.Tasks;

namespace Lead2Change.Services.QuestionInInterviews
{
    public interface IQuestionInInterviewService
    {
        public  Task<QuestionInInterview> Update(QuestionInInterview model);
        public  Task<QuestionInInterview> Create(QuestionInInterview model);
        public  Task<List<QuestionInInterview>> GetQuestionInInterviews();
        public  Task<QuestionInInterview> Delete(QuestionInInterview model);



    }
}
