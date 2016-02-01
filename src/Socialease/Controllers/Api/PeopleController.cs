using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Socialease.ViewModels;

namespace Socialease.Controllers.Api
{
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
    }
}
