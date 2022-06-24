using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Models;
using WarehouseManager.Services;

namespace WarehouseManager.Controllers
{
    [Route("api/company/{companyId}/location/{locationId}/warehouse/{warehouseId}/warehouseCargo")]
    [ApiController]
    [Authorize]
    public class WarehouseCargoController : ControllerBase
    {
        private readonly IWarehouseCargoService _warehouseCargoService;

        public WarehouseCargoController(IWarehouseCargoService warehouseCargoService)
        {
            _warehouseCargoService = warehouseCargoService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromRoute] int companyId, [FromRoute] int locationId, [FromRoute] int warehouseId,
            [FromBody] CreateWarehouseCargoDto dto)
        {
            var newCargoId = _warehouseCargoService.Create(companyId, locationId, warehouseId, dto);
            return Created($"api/company/{companyId}/location/{locationId}/warehouse/{warehouseId}/warehouseCargo/{newCargoId}", null);
        }

        [HttpDelete("{warehouseCargoId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromRoute] int companyId, [FromRoute] int locationId, [FromRoute] int warehouseId,
            [FromRoute] int warehouseCargoId)
        {
            _warehouseCargoService.Delete(companyId, locationId, warehouseId, warehouseCargoId);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<WarehouseCargoDto>> Get([FromRoute] int warehouseId)
        {
            var result = _warehouseCargoService.GetAll(warehouseId);
            return Ok(result);
        }

        [HttpGet("{warehouseCargoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WarehouseCargoDto> Get([FromRoute] int companyId, [FromRoute] int locationId,
            [FromRoute] int warehouseId, [FromRoute] int warehouseCargoId)
        {
            WarehouseCargoDto warehouseCargo =
                _warehouseCargoService.GetById(companyId, locationId, warehouseId, warehouseCargoId);
            return Ok(warehouseCargo);
        }

    }
}
