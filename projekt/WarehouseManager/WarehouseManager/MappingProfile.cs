using AutoMapper;
using WarehouseManager.Entites;
using WarehouseManager.Models;

namespace WarehouseManager
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<Location, LocationDto>();
            CreateMap<Warehouse, WarehouseDto>();

            CreateMap<CreateCompanyDto, Company>();
            CreateMap<CreateLocationDto, Location>();
            CreateMap<CreateWarehouseDto, Warehouse>();
        }
    }
}
