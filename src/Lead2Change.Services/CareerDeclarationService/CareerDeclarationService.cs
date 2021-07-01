using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lead2Change.Repositories.CareerDeclarationRepository;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Services.CareerDeclarationService
{
    public class CareerDeclarationService : _BaseService, ICareerDeclarationService
    {
        private ICareerDeclarationRepository _repository;

        public CareerDeclarationService(AppDbContext dbContext) : base(dbContext)
        {
            _repository = new CareerDeclarationRepository(dbContext);
        }

        public async Task<CareerDeclaration> Create(CareerDeclaration careerDeclaration)
        {
            return await _repository.Create(careerDeclaration);
        }

        public async Task<CareerDeclaration> Delete(CareerDeclaration model)
        {
            return await _repository.Delete(model);
        }

        public async Task<CareerDeclaration> GetCareerDeclaration(Guid id)
        {
            return await _repository.GetCareerDeclaration(id);
        }

        public async Task<List<CareerDeclaration>> GetCareerDeclarations()
        {
            return await _repository.GetCareerDeclarations();
        }

        public async Task<CareerDeclaration> Update(CareerDeclaration model)
        {
            return await _repository.Update(model);
        }
    }
}
