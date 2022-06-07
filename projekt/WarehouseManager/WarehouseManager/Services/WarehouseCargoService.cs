using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseManager.Entites;
using WarehouseManager.Exceptions;
using WarehouseManager.Models;

namespace WarehouseManager.Services
{
    public interface IWarehouseCargoService
    {
        public int Create(int companyId, int locationId, int warehouseId, CreateWarehouseCargoDto dto);
        public void Delete(int companyId, int locationId, int warehouseId, int warehouseCargoId);
        public List<WarehouseCargoDto> GetAll(int warehouseId);
        public WarehouseCargoDto GetById(int companyId, int locationId, int warehouseId, int warehouseCargoId);
    }

    public class WarehouseCargoService : IWarehouseCargoService
    {
        private readonly WarehauseManagerDbContext _dbContext;
        private readonly IMapper _mapper;

        public WarehouseCargoService(WarehauseManagerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int companyId, int locationId, int warehouseId, CreateWarehouseCargoDto dto)
        {
            var location = GetLocationById(locationId);
            var company = GetCompanyById(companyId);
            var warehouse = GetWarehouseById(warehouseId);
            var warehouseCargoEntity = _mapper.Map<WarehouseCargo>(dto);
            warehouseCargoEntity.WarehouseId = warehouseId;

            if (dto.Volume < 0) 
                throw new BadRequestException("400 - Bad request\nVolume cannot be negative");



            if (warehouse.StorageType == StorageType.Hall &&
                (dto.CargoType == CargoType.Liquid || dto.CargoType == CargoType.BulkMaterial))
            {
                throw new BadRequestException("400 - Bad request\nWrong type for the storage");
            }

            if (warehouse.StorageType == StorageType.Silo &&
                (dto.CargoType == CargoType.Crate || dto.CargoType == CargoType.Palette))
            {
                throw new BadRequestException("400 - Bad request\nWrong type for the storage");
            }

            if (warehouse.StorageType == StorageType.Silo &&
                (dto.CargoType == CargoType.Liquid && warehouse.WarehousesCargo.Exists(r => r.CargoType == CargoType.BulkMaterial)))
            {
                throw new BadRequestException("400 - Bad request\nWrong type for the storage");
            }
            if (warehouse.StorageType == StorageType.Silo &&
                (dto.CargoType == CargoType.BulkMaterial && warehouse.WarehousesCargo.Exists(r => r.CargoType == CargoType.Liquid)))
            {
                throw new BadRequestException("400 - Bad request\nWrong type for the storage");
            }

            decimal toAdd = dto.Volume;
            warehouse.CurrentCapacity += toAdd;
            _dbContext.WarehouseCargoes.Add(warehouseCargoEntity);
            _dbContext.SaveChanges();
            return warehouseCargoEntity.Id;
        }

        public void Delete(int companyId, int locationId, int warehouseId, int warehouseCargoId)
        {
            var location = GetLocationById(locationId);
            var company = GetCompanyById(companyId);
            var warehouse = GetWarehouseById(warehouseId);
            var warehouseCargo = _dbContext
                .WarehouseCargoes
                .FirstOrDefault(r => r.Id == warehouseCargoId);
            if (warehouseCargo == null)
                throw new NotFoundException("Cargo not found");

            decimal toSubtract = warehouseCargo.Volume;
            if (toSubtract > warehouse.CurrentCapacity) warehouse.CurrentCapacity = 0;
            warehouse.CurrentCapacity -= toSubtract;
            _dbContext.WarehouseCargoes.Remove(warehouseCargo);
            _dbContext.SaveChanges();
        }

        public List<WarehouseCargoDto> GetAll(int warehouseId)
        {
            var warehouse = GetWarehouseById(warehouseId);
            var warehouseCargoDtos = _mapper.Map<List<WarehouseCargoDto>>(warehouse.WarehousesCargo);
            return warehouseCargoDtos;
        }

        public WarehouseCargoDto GetById(int companyId, int locationId, int warehouseId, int warehouseCargoId)
        {
            var location = GetLocationById(locationId);
            var company = GetCompanyById(companyId);
            var warehouse = GetWarehouseById(warehouseId);
            var warehouseCargo = _dbContext
                .WarehouseCargoes
                .FirstOrDefault(r => r.Id == warehouseCargoId);
            if (warehouseCargo == null)
                throw new NotFoundException("Cargo not found");
            var cargoDto = _mapper.Map<WarehouseCargoDto>(warehouseCargo);
            return cargoDto;
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

        private Company GetCompanyById(int companyId)
        {
            var company = _dbContext
                .Companies
                .Include(r => r.Locations)
                .FirstOrDefault(r => r.Id == companyId);
            if (company == null) throw new NotFoundException("Company not found");
            return company;
        }

        private Warehouse GetWarehouseById(int warehouseId)
        {
            var warehouse = _dbContext
                .Warehouses
                .Include(r => r.WarehousesCargo)
                .FirstOrDefault(r => r.Id == warehouseId);
            if (warehouse == null) throw new NotFoundException("Warehouse not found");
            return warehouse;
        }
    }
}
