﻿namespace WarehouseManager.Models
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CompanyId { get; set; }

        public List<WarehouseDto> Warehouses { get; set; }
    }
}
