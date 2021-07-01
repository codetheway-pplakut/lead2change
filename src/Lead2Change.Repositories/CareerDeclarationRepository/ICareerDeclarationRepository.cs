using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lead2Change.Domain.Models;

namespace Lead2Change.Repositories.CareerDeclarationRepository
{
    public interface ICareerDeclarationRepository
    {
        public Task<CareerDeclaration> Create(CareerDeclaration careerDeclaration);
        public Task<List<CareerDeclaration>> GetCareerDeclarations();
        public Task<CareerDeclaration> GetCareerDeclaration(Guid id);
        public Task<CareerDeclaration> Update(CareerDeclaration model);
        public Task<CareerDeclaration> Delete(CareerDeclaration model);
    }
}
