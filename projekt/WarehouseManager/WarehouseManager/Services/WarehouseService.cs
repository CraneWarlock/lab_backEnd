using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Exceptions;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public interface IWarehouseService
    {
        public int Create(int companyId, int locationId, CreateWarehouseDto dto);
        public void Update(int locationId, int warehouseId, UpdateWarehouseDto dto);
        public void Delete(int locationId, int warehouseId);
        public WarehouseDto GetById(int locationId, int warehouseId);
        public List<WarehouseDto> GetAll(int locationId);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly WarehauseManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public WarehouseService(WarehauseManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int companyId, int locationId, CreateWarehouseDto dto)
        {
            var location = GetLocationById(locationId);
            var warehouseEntity = _mapper.Map<Warehouse>(dto);
            warehouseEntity.LocationId = locationId;

            
            if (dto.CurrentCapacity > 0)
                throw new BadRequestException("400 - Bad request\nCurrent capacity must be 0");
            if (dto.MaximumCapacity <= 0) 
                throw new BadRequestException("400 - Bad request\nMaximum capacity cannot be negative");
            

            _dbContext.Warehouses.Add(warehouseEntity);
            _dbContext.SaveChanges();
            return warehouseEntity.Id;
        }

        public void Update(int locationId, int warehouseId, UpdateWarehouseDto dto)
        {
            var location = GetLocationById(locationId);
            var warehouse = _dbContext
                .Warehouses
                .FirstOrDefault(r => r.Id == warehouseId);

            if (warehouse is null) throw new NotFoundException("Warehouse not found");
            if (dto.MaximumCapacity <= 0)
                throw new BadRequestException("400 - Bad request\nMaximum capacity cannot be negative");

            warehouse.WarehouseName = dto.WarehouseName;
            warehouse.Description = dto.Description;
            warehouse.MaximumCapacity = dto.MaximumCapacity;
            _dbContext.SaveChanges();
        }

        public void Delete(int locationId, int warehouseId)
        {
            var location = GetLocationById(locationId);
            var warehouse = _dbContext
                .Warehouses
                .FirstOrDefault(r => r.Id == warehouseId);
            if (warehouse is null) 
                throw new NotFoundException("Warehouse not found");

            _dbContext.Warehouses.Remove(warehouse);
            _dbContext.SaveChanges();
            
        }

        public WarehouseDto GetById(int locationId, int warehouseId)
        {
            var location = GetLocationById(locationId);
            var warehouse = _dbContext
                .Warehouses
                .FirstOrDefault(r => r.Id == warehouseId);
            if (warehouse is null || warehouse.LocationId != locationId)
                throw new NotFoundException("Warehouse not found");

            var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);
            return warehouseDto;
        }

        public List<WarehouseDto> GetAll(int locationId)
        {
            var location = GetLocationById(locationId);
            var warehouseDtos = _mapper.Map<List<WarehouseDto>>(location.Warehouses);
            return warehouseDtos;
        }

        private Location GetLocationById(int locationId)
        {
            var location = _dbContext
                .Locations
                .Include(r => r.Warehouses)
                .FirstOrDefault(r => r.Id == locationId);
            if (location == null) throw new NotFoundException("Location not found");
            return location;
        }

    }
}
