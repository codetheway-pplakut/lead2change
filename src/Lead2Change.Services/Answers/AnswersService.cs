using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Domain.Models;
using Lead2Change.Repositories.Answers;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;
using Lead2Change.Repositories;
namespace Lead2Change.Services.Answers
{
    public class AnswersService : IAnswersService
    { 
                private IAnswersRepository AnswersRepository;

    
         public AnswersService(AppDbContext dbContext)
    {
        this.AnswersRepository = new AnswersRepository(dbContext);
    }
        public async Task<Answer> Create(Answer answer)
        {
            return await this.AnswersRepository.Create(answer);
        }
        public async Task<Answer> GetAnswer(Guid id)
        {
            return await this.AnswersRepository.GetAnswer(id);
        }
        public async Task<Answer> Delete(Answer answer)
        {
            return await AnswersRepository.Delete(answer);
        }
    }
}
