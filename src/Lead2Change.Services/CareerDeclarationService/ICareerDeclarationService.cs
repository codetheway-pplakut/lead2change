using Lead2Change.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lead2Change.Services.CareerDeclarationService
{
    public interface ICareerDeclarationService
    {
        public Task<CareerDeclaration> Create(CareerDeclaration careerDeclaration);
        public Task<List<CareerDeclaration>> GetCareerDeclarations();
        public Task<CareerDeclaration> GetCareerDeclaration(Guid id);
        public Task<CareerDeclaration> Update(CareerDeclaration model);
        public Task<CareerDeclaration> Delete(CareerDeclaration model);
    }
}
