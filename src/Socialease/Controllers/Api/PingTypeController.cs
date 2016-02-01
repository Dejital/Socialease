using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Socialease.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Socialease.Controllers.Api
{
    [Route("api/pingtypes")]
    public class PingTypeController : Controller
    {
        private readonly ISocialRepository _repository;
        private readonly ILogger<PingTypeController> _logger;

        public PingTypeController(ISocialRepository repository, ILogger<PingTypeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var results = Mapper.Map<IEnumerable<PingTypeViewModel>>(_repository.GetAllPingTypes());
            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]PingTypeViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pingType = Mapper.Map<PingType>(vm);
                    _logger.LogInformation("Attempting to save a new Ping Type.");
                    _repository.AddPingType(pingType);
                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int) HttpStatusCode.Created;
                        return Json(Mapper.Map<PingTypeViewModel>(pingType));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save a new Ping Type.", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message });
            }
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState});
        }
    }
}