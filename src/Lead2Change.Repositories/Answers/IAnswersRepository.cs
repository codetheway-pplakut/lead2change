using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Repositories.Answers
{
    public interface IAnswersRepository
    {
        public Task<List<Answer>> GetAnswers();
        public Task<Answer> GetAnswer(Guid id);
        public Task<Answer> Update(Answer model);
        public Task<Answer> Create(Answer answer);
        public Task<Answer> Delete(Answer model)
;
    }
}
