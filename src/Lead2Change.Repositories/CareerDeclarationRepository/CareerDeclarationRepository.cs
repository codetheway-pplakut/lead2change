using Lead2Change.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lead2Change.Data.Contexts;

namespace Lead2Change.Repositories.CareerDeclarationRepository
{
    public class CareerDeclarationRepository : _BaseRepository, ICareerDeclarationRepository
    {
        private AppDbContext AppDbContext;
        public CareerDeclarationRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.AppDbContext = dbContext;
        }
        public async Task<CareerDeclaration> Create(CareerDeclaration careerDeclaration)
        {
            var result = await this.AppDbContext.AddAsync(careerDeclaration);
            await this.AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<List<CareerDeclaration>> GetCareerDeclarations()
        {
            return await this.AppDbContext.CareerDeclarations.ToListAsync();
        }
        public async Task<CareerDeclaration> GetCareerDeclaration(Guid id)
        {
            return await AppDbContext.CareerDeclarations.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<CareerDeclaration> Update(CareerDeclaration model)
        {
            var result = AppDbContext.CareerDeclarations.Update(model);
            await AppDbContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<CareerDeclaration> Delete(CareerDeclaration model)
        {
            AppDbContext.CareerDeclarations.Remove(model);
            await AppDbContext.SaveChangesAsync();
            return model;
        }
    }
}
