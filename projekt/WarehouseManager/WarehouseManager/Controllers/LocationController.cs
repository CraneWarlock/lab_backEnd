using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Models;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Post([FromRoute] int companyId, [FromBody] CreateLocationDto dto)
        {
            var newLocId = _locationService.Create(companyId, dto);
            return Created($"api/company/{companyId}/location/{newLocId}", null);
        }

        [HttpGet("{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LocationDto> Get([FromRoute] int companyId, [FromRoute] int locationId)
        {
            LocationDto location = _locationService.GetById(companyId, locationId);
            return Ok(location);
        }

        [HttpGet]
        public ActionResult<List<LocationDto>> Get([FromRoute] int companyId)
        {
            var result = _locationService.GetAll(companyId);
            return Ok(result);
        }

        [HttpPut("{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update([FromRoute]int companyId,[FromRoute]int locationId, [FromBody]UpdateLocationDto dto)
        {
            _locationService.Update(companyId, locationId, dto);
            return Ok();
        }

        [HttpDelete("{locationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromRoute] int companyId, [FromRoute] int locationId)
        {
            _locationService.Delete(companyId, locationId);
            return NoContent();
        }
    }
}
