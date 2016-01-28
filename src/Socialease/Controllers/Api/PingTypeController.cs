using Microsoft.AspNet.Mvc;
using Socialease.Models;

namespace Socialease.Controllers.Api
{
    public class PingTypeController : Controller
    {
        private readonly ISocialRepository _repository;

        public PingTypeController(ISocialRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/pingtypes")]
        public JsonResult Get()
        {
            var results = _repository.GetAllPingTypes();
            return Json(results);
        }
    }
}