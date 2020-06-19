using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPersons.Entity;
using APIPersons.Models;
using APIPersons.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonsController(PersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new List<PersonModel>();

            try
            {
                result = _mapper.Map<List<PersonModel>>(_personRepository.GetAllPersons());
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);    
            }

            try
            {
                await _personRepository.AddPerson(_mapper.Map<PersonEntity>(personModel));
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> Delete(int personId)
        {

            try
            {
                if (personId > 0)
                {
                    var result = await _personRepository.DeletePerson(personId);
                    if (result)
                        return Ok();
                    else
                        return NotFound();
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
        }


        [HttpGet("oldest")]
        public IActionResult GetOldestPerson()
        {
            var result = new PersonModel();

            try
            {
                result = _mapper.Map<PersonModel>(_personRepository.GetOldestPerson());
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure " + ex.Message);
            }
            return Ok(result);
        }

    }
}