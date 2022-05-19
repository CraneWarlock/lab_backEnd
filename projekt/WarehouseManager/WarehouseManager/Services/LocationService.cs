using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public class LocationService
    {
        private readonly WarehauseManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public LocationService(WarehauseManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int companyId, CreateLocationDto dto)
        {
            var company = GetCompanyById(companyId);
            var locationEntity = _mapper.Map<Location>(dto);
            locationEntity.CompanyId = companyId;
            _dbContext.Locations.Add(locationEntity);
            _dbContext.SaveChanges();
            return locationEntity.Id;
        }

        public LocationDto GetById(int companyId, int locationId)
        {
            var company = GetCompanyById(companyId);
            var location = _dbContext.Locations.FirstOrDefault(d => d.Id == locationId);
            if(location is null || location.CompanyId != companyId)
            {
                return null;
            }

            var locationDto = _mapper.Map<LocationDto>(location);
            return locationDto;
        }

        public List<LocationDto> GetAll(int companyid)
        {
            var company = GetCompanyById(companyid);
            var locationDtos = _mapper.Map<List<LocationDto>>(company.Locations);
            return locationDtos;
        }

        private Company GetCompanyById(int companyId)
        {
            var company = _dbContext
                .Companies
                .Include(r => r.Locations)
                .FirstOrDefault(r => r.Id == companyId);
            if (company == null) return null;
            return company;
        }

    }
}
