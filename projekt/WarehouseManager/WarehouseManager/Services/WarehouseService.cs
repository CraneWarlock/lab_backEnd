using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public interface IWarehouseService
    {
        public int Create(int companyId, int locationId, CreateWarehouseDto dto);
        public bool Update(int locationId, int warehouseId, UpdateWarehouseDto dto);
        public bool Delete(int locationId, int warehouseId);
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

            //TODO: exception here
            if (dto.MaximumCapacity <= 0) return 0;
            if (dto.CurrentCapacity != 0)
            {
                dto.CurrentCapacity = 0;
            }

            _dbContext.Warehouses.Add(warehouseEntity);
            _dbContext.SaveChanges();
            return warehouseEntity.Id;
        }

        public bool Update(int locationId, int warehouseId, UpdateWarehouseDto dto)
        {
            var location = GetLocationById(locationId);
            var warehouse = _dbContext
                .Warehouses
                .FirstOrDefault(r => r.Id == locationId);

            if (warehouse is null) return false;
            if (dto.MaximumCapacity <= 0) return false;

            warehouse.WarehouseName = dto.WarehouseName;
            warehouse.Description = dto.Description;
            warehouse.MaximumCapacity = dto.MaximumCapacity;
            _dbContext.SaveChanges();
            return true;
        }

        public bool Delete(int locationId, int warehouseId)
        {
            var location = GetLocationById(locationId);
            if (location is null) return false;
            var warehouse = _dbContext
                .Warehouses
                .FirstOrDefault(r => r.Id == warehouseId);
            if (warehouse is null) return false;

            _dbContext.Warehouses.Remove(warehouse);
            _dbContext.SaveChanges();
            return true;
        }

        public WarehouseDto GetById(int locationId, int warehouseId)
        {
            var location = GetLocationById(locationId);
            var warehouse = _dbContext
                .Warehouses
                .FirstOrDefault(r => r.Id == warehouseId);
            if (warehouse is null || warehouse.LocationId != locationId) return null;

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
            if (location == null) return null;
            return location;
        }

    }
}
