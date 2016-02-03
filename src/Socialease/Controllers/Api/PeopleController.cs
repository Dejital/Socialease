using System;
using System.Net;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Socialease.ViewModels;

namespace Socialease.Controllers.Api
{
    [Authorize]
    [Route("api/people")]
    public class PeopleController : Controller
    {
        private readonly ISocialRepository _repository;
        private readonly ILogger<PingTypeController> _logger;

        public PeopleController(ISocialRepository repository, ILogger<PingTypeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var results = _repository.GetPersonById(id);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<PersonViewModel>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get person of id #{id}.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
        }

        [HttpPost("")]
        public JsonResult Post([FromBody] PersonViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var person = Mapper.Map<Person>(vm);
                    _logger.LogInformation("Attempting to save a new Person.");
                    _repository.AddPerson(person);
                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<Person>(person));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new person.", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json("Failed to save new person.");
            }
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState});
        }
    }
}
