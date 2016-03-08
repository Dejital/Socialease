using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;
using Socialease.ViewModels;

namespace Socialease.Controllers.Api
{
    [Authorize]
    [Route("api/people/{personId}/pings")]
    public class PingsController : Controller
    {
        private readonly ISocialRepository _repository;
        private readonly ILogger<PingsController> _logger;

        public PingsController(ISocialRepository repository, ILogger<PingsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public JsonResult GetAll(int personId)
        {
            try
            {
                var results = _repository.GetAllPings(personId, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<IEnumerable<PingViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get all pings.", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(new {ex.Message});
            }
        }

        [HttpGet("{pingId}")]
        public JsonResult Get(int personId, int pingId)
        {
            try
            {
                var results = _repository.FindPing(pingId, User.Identity.Name);
                if (results == null)
                {
                    return Json(null);
                }
                return Json(Mapper.Map<PingViewModel>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get ping with id {pingId}.", ex);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { ex.Message });
            }
        }

        [HttpPost("")]
        public JsonResult Post(int personId, [FromBody] PingViewModel vm)
        {
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (!ModelState.IsValid || personId == 0)
            {
                return Json(new {Message = "Failed", ModelState = ModelState});
            }
            try
            {
                var ping = Mapper.Map<Ping>(vm);
                ping.Created = DateTime.Now;
                ping.PersonId = personId;
                ping.UserName = User.Identity.Name;
                _logger.LogInformation("Attempting to save a new ping.");
                _repository.AddPing(ping);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return Json(Mapper.Map<Ping>(ping));
                }
            }
            catch (Exception ex)
            {
                var message = "Failed to save new ping.";
                _logger.LogError(message, ex);
                return Json(message);
            }
            return Json(new {Message = "Failed", ModelState = ModelState});
        }

        [HttpPut("{pingId}")]
        public JsonResult Put(int personId, int pingId, [FromBody] PingViewModel vm)
        {
            var errorMessage = "Failed to update a ping.";
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            if (!ModelState.IsValid || pingId != vm.Id)
            {
                _logger.LogError(errorMessage);
                return Json(errorMessage);
            }
            try
            {
                var ping = Mapper.Map<Ping>(vm);
                ping.UserName = User.Identity.Name;
                _logger.LogInformation("Attempting to update an existing ping.");
                _repository.UpdatePing(ping);
                if (_repository.SaveAll())
                {
                    Response.StatusCode = (int) HttpStatusCode.OK;
                    return Json(Mapper.Map<Ping>(ping));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(errorMessage, ex);
                return Json(errorMessage);
            }
            _logger.LogError(errorMessage);
            return Json(errorMessage);
        }
    }
}
