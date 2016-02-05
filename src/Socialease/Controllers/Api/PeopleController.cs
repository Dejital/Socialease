using System;
using System.Collections.Generic;
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
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(ISocialRepository repository, ILogger<PeopleController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var people = _repository.GetUserPeople(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<PersonViewModel>>(people);

            return Json(results);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            try
            {
                var results = _repository.GetPersonById(id, User.Identity.Name);
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
                    person.UserName = User.Identity.Name;
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
                var message = "Failed to save new person.";
                _logger.LogError(message, ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(message);
            }
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState});
        }

        [HttpPost("{id}")]
        public JsonResult Post(int id, [FromBody] PersonViewModel vm)
        {
            var errorMessage = "Failed to update a person.";
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            if (id != vm.Id)
            {
                _logger.LogError(errorMessage);
                return Json(errorMessage);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    var person = Mapper.Map<Person>(vm);
                    person.UserName = User.Identity.Name;
                    _logger.LogInformation("Attempting to update an existing person.");
                    _repository.UpdatePerson(person);
                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.OK;
                        return Json(Mapper.Map<Person>(person));
                    }
                    _logger.LogError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(errorMessage, ex);
                return Json(errorMessage);
            }
            return Json(errorMessage);
        }
    }
}
