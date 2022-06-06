using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Exceptions;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public interface ILocationService
    {
        int Create(int companyId, CreateLocationDto dto);
        void Update(int companyId, int locationId, UpdateLocationDto dto);
        void Delete(int companyId, int locationId);
        LocationDto GetById(int companyId, int locationId);
        List<LocationDto> GetAll(int companyid);
    }

    public class LocationService : ILocationService
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

        public void Update(int companyId, int locationId, UpdateLocationDto dto)
        {
            var company = GetCompanyById(companyId);
            var location = _dbContext
                .Locations
                .FirstOrDefault(r => r.Id == locationId);
            if(location is null) throw new NotFoundException("Location not found");
            location.LocationName = dto.LocationName;
            location.Description = dto.Description;
            location.Address = dto.Address;
            _dbContext.SaveChanges();
        }

        public void Delete(int companyId, int locationId)
        {
            var company = GetCompanyById(companyId);
            var location = _dbContext
                .Locations
                .FirstOrDefault(r => r.Id == locationId);
            if (location is null) throw new NotFoundException("Location not found");
            _dbContext.Locations.Remove(location);
            _dbContext.SaveChanges();
        }

        public LocationDto GetById(int companyId, int locationId)
        {
            var company = GetCompanyById(companyId);
            var location = _dbContext
                .Locations
                .FirstOrDefault(d => d.Id == locationId);
            if(location is null || location.CompanyId != companyId)
            {
                if (location is null) throw new NotFoundException("Location not found");
            }

            var locationDto = _mapper.Map<LocationDto>(location);
            return locationDto;
        }

        public List<LocationDto> GetAll(int companyId)
        {
            var company = GetCompanyById(companyId);
            var locationDtos = _mapper.Map<List<LocationDto>>(company.Locations);
            return locationDtos;
        }

        private Company GetCompanyById(int companyId)
        {
            var company = _dbContext
                .Companies
                .Include(r => r.Locations)
                .FirstOrDefault(r => r.Id == companyId);
            if (company == null) throw new NotFoundException("Company not found");
            return company;
        }

    }
}
