using Microsoft.AspNetCore.Mvc;
using WarehouseManager.Models;
using WarehouseManager.Services;

namespace WarehouseManager.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateCompanyDto dto, [FromRoute] int id)
        {
            _companyService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _companyService.Delete(id);
            return NoContent();
        }

        [HttpPost]
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
        public ActionResult<CompanyDto> Get([FromRoute] int id)
        {
            var company = _companyService.GetById(id);
            return Ok(company);
        }
    }
}
