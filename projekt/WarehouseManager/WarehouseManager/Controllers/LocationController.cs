using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Services;

namespace WarehouseManager.Controllers
{
    [Route("api/company/{companyId}/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


    }
}
