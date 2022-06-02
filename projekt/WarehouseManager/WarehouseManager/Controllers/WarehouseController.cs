using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Models;
using WarehouseManager.Services;

namespace WarehouseManager.Controllers
{   
    [Route("api/company/{companyId}/location/{locationId}/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int companyId, [FromRoute] int locationId, [FromBody] CreateWarehouseDto dto)
        {
            if (dto.CurrentCapacity > 0) return BadRequest();
            var newWarId = _warehouseService.Create(companyId, locationId, dto);
            return Created($"api/company/{companyId}/location/{locationId}/warehouse/{newWarId}", null);
        }

        [HttpGet("{warehouseId}")]
        public ActionResult<WarehouseDto> Get([FromRoute] int locationId, [FromRoute] int warehouseId)
        {
            WarehouseDto warehouse = _warehouseService.GetById(locationId, warehouseId);
            return Ok(warehouse);
        }

        [HttpGet]
        public ActionResult<List<WarehouseDto>> Get([FromRoute] int locationId)
        {
            var result = _warehouseService.GetAll(locationId);
            return Ok(result);
        }

        [HttpPut("{warehouseId}")]
        public ActionResult Update([FromRoute] int locationId, [FromRoute] int warehouseId,
            [FromBody] UpdateWarehouseDto dto)
        {
            _warehouseService.Update(locationId, warehouseId, dto);
            return Ok();
        }

        [HttpDelete("{warehouseId}")]
        public ActionResult Delete([FromRoute] int locationId, [FromRoute] int warehouseId)
        {
            _warehouseService.Delete(locationId, warehouseId);
            return NoContent();
        }
    }
}
