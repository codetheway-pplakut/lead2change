using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Services.Answers
{
    public interface IAnswersService
    {
        public Task<Answer> GetAnswer(Guid id);
        public Task<Answer> Update(Answer model);
        public Task<Answer> Create(Answer answer);
        public Task<Answer> Delete(Answer answer);

    }
}
