using AutoMapper;
using CompanyEmployees.ModelBinder;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repository.Company.GetAllCompanies(false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companiesDto);
        }
        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id, false);
            if (company == null)
            {
                _logger.LogInfo($"Company with Id:{id} doesn't exit in the database");
                return NotFound();  
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto); 
            }
        }
        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");
                return BadRequest("CompanyForCreationDto object is null");
            }

            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            /*CreateAtRoute vazifasi:
             * "CompanyById" nomli get metodidan "id" si "companyToReturn.id" teng bulgan "companyToReturn" ni qaytar degani*/
            return CreatedAtRoute("CompanyById", 
                                      new { Id = companyToReturn.id },
                                      companyToReturn);
        }

        [HttpGet("collection/{ids}",Name ="CompanyCollection")]

        public IActionResult GetCompanyCollection([ModelBinder(BinderType =
                                    typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var companyEntities = _repository.Company.GetByIds(ids,false);

            if(ids.Count() != companyEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            return Ok(companiesToReturn);
        }
        [HttpPost("collection")]
        public IActionResult CreateCompanyColection([FromBody]
                            IEnumerable<CompanyForCreationDto> companyCollection)
        {
            if (companyCollection == null)
            {
                _logger.LogError("Company Collection sent from client is null");
                return BadRequest("Company Collection is null");
            }
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntities)
            {
                _repository.Company.CreateCompany(company);
            }
            _repository.Save();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.id));

            return CreatedAtRoute("CompanyCollection", new { ids},
                companyCollectionToReturn
                );
        }
        [HttpDelete]
        public IActionResult DeleteCompany(Guid Id)
        {
            var company = _repository.Company.GetCompany(Id,false); 
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {Id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Company.DeleteCompany(company);
            _repository.Save();
            return NoContent();
        }
        [HttpPut]
        public IActionResult UpdateCompany(Guid id,[FromBody]
                CompanyForUpdateDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyForUpdateDto object sent from client is null.");
                return BadRequest("CompanyForUpdateDto object is null");
            }
            var companyEntity = _repository.Company.GetCompany(id,true);
            if (companyEntity == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(company, companyEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
