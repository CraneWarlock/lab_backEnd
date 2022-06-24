using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Models;
using WarehouseManager.Services;

namespace WarehouseManager.Controllers
{
    [Route("api/company")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult Update([FromBody] UpdateCompanyDto dto, [FromRoute] int id)
        {
            _companyService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            _companyService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateCompany([FromBody] CreateCompanyDto dto)
        {
            var id = _companyService.Create(dto);
            return Created($"/api/company/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDto>> GetAll()
        {
            var companiesDtos = _companyService.GetAll();
            return Ok(companiesDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public ActionResult<CompanyDto> Get([FromRoute] int id)
        {
            var company = _companyService.GetById(id);
            return Ok(company);
        }
    }
}
