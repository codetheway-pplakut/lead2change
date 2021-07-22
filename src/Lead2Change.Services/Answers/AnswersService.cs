﻿using Lead2Change.Repositories.Answers;
using System;
using System.Collections.Generic;
using System.Text;
using Lead2Change.Data.Contexts;
using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Domain.ViewModels;

namespace Lead2Change.Services.Answers
{
    public class AnswersService : IAnswersService
    {
        IAnswersRepository AnswersRepository;
        public AnswersService(AppDbContext dbContext) 
        {
            this.AnswersRepository = new AnswersRepository(dbContext);
        }
        public async Task<List<Answer>> GetAnswers(Guid interviewID)
        {
            var allAnswers = await this.AnswersRepository.GetAnswers();
            var result = new List<Answer>();
            foreach (Answer answer in allAnswers)
            {

                if (answer.InterviewId.Equals(interviewID))
                {
                    result.Add(answer);

                }
            }
            return result;

        }
        public async Task<Answer> GetAnswer(Guid id)
        {
            return await this.AnswersRepository.GetAnswer(id);
        }
        public async Task<List<Answer>> GetAnswers()
        {
            return await this.AnswersRepository.GetAnswers();
        }
        public async Task<Answer> Update(Answer model)
        {
            return await this.AnswersRepository.Update(model);
        }
        public async Task<Answer> Create(Answer answer)
        {
            return await this.AnswersRepository.Create(answer);
        }
        public async Task<Answer> Delete(Answer answer)
        {
            return await AnswersRepository.Delete(answer);
        }
        public async Task<Answer> AnswerQuestion(Answer answer)
        {
            return await this.AnswersRepository.AnswerQuestion(answer);
        }
    }
}
