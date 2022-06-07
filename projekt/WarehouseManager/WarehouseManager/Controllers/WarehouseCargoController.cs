using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Models;
using WarehouseManager.Services;

namespace WarehouseManager.Controllers
{
    [Route("api/company/{companyId}/location/{locationId}/warehouse/{warehouseId}/warehouseCargo")]
    [ApiController]
    public class WarehouseCargoController : ControllerBase
    {
        private readonly IWarehouseCargoService _warehouseCargoService;

        public WarehouseCargoController(IWarehouseCargoService warehouseCargoService)
        {
            _warehouseCargoService = warehouseCargoService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int companyId, [FromRoute] int locationId, [FromRoute] int warehouseId,
            [FromBody] CreateWarehouseCargoDto dto)
        {
            var newCargoId = _warehouseCargoService.Create(companyId, locationId, warehouseId, dto);
            return Created($"api/company/{companyId}/location/{locationId}/warehouse/{warehouseId}/warehouseCargo/{newCargoId}", null);
        }

        [HttpDelete("{warehouseCargoId}")]
        public ActionResult Delete([FromRoute] int companyId, [FromRoute] int locationId, [FromRoute] int warehouseId,
            [FromRoute] int cargoId)
        {
            _warehouseCargoService.Delete(companyId, locationId, warehouseId, cargoId);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<WarehouseCargoDto>> Get([FromRoute] int warehouseId)
        {
            var result = _warehouseCargoService.GetAll(warehouseId);
            return Ok(result);
        }

        [HttpGet("{warehouseCargoId}")]
        public ActionResult<WarehouseCargoDto> Get([FromRoute] int companyId, [FromRoute] int locationId,
            [FromRoute] int warehouseId, [FromRoute] int warehouseCargoId)
        {
            WarehouseCargoDto warehouseCargo =
                _warehouseCargoService.GetById(companyId, locationId, warehouseId, warehouseCargoId);
            return Ok(warehouseCargo);
        }

    }
}
