using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Exceptions;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public interface ICompanyService
    {
        CompanyDto GetById(int id);
        IEnumerable<CompanyDto> GetAll();
        int Create(CreateCompanyDto dto);
        void Delete(int id);
        void Update(int id, UpdateCompanyDto dto);
    }

    public class CompanyService : ICompanyService
    {
        private readonly WarehauseManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyService(WarehauseManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Update(int id, UpdateCompanyDto dto)
        {
            var company = _dbContext
                .Companies
                .FirstOrDefault(r => r.Id == id);
            if (company is null) throw new NotFoundException("Company not found");
            company.Name = dto.Name;
            company.Description = dto.Description;
            company.Address = dto.Address;
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var company = _dbContext
                .Companies
                .FirstOrDefault(r => r.Id == id);
            if (company is null) throw new NotFoundException("Company not found");
            _dbContext.Companies.Remove(company);
            _dbContext.SaveChanges();
        }

        public CompanyDto GetById(int id)
        {
            var company = _dbContext
                .Companies
                .Include(r => r.Locations)
                .FirstOrDefault(r => r.Id == id);
            if (company is null) throw new NotFoundException("Company not found");
            var result = _mapper.Map<CompanyDto>(company);
            return result;
        }

        public IEnumerable<CompanyDto> GetAll()
        {
            var companies = _dbContext
                .Companies
                .Include(r => r.Locations)
                .ToList();
            var companiesDtos = _mapper.Map<List<CompanyDto>>(companies);
            return companiesDtos;
        }

        public int Create(CreateCompanyDto dto)
        {
            var company = _mapper.Map<Company>(dto);
            _dbContext.Companies.Add(company);
            _dbContext.SaveChanges();
            return company.Id;
        }

        

    }
}
