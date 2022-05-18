﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public class CompanyService
    {
        private readonly WarehauseManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public CompanyService(WarehauseManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Update(int id, UpdateCompanyDto dto)
        {
            var company = _dbContext
                .Companies
                .FirstOrDefault(r => r.Id == id);
            if (company is null) return false;

            company.Name = dto.Name;
            company.Description = dto.Description;
            company.Address = dto.Address;
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var company = _dbContext
                .Companies
                .FirstOrDefault(r => r.Id == id);
            if (company is null) return false;
            _dbContext.Companies.Remove(company);
            _dbContext.SaveChanges();
            return true;
        }

        public CompanyDto GetById(int id)
        {
            var company = _dbContext
                .Companies
                .Include(r => r.Locations)
                .FirstOrDefault(r => r.Id == id);
            if (company is null) return null;
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

    }
}
