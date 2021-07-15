using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Lead2Change.Services.Answers
{
    public interface IAnswersService
    {
        public Task<Answer> Create(Answer answer);
        public Task<Answer> GetAnswer(Guid id);
        public Task<Answer> Delete(Answer answer);

    }
}
