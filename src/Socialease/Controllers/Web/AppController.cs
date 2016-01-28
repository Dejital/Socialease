using System.Linq;
using Microsoft.AspNet.Mvc;
using Socialease.Models;
using Socialease.Services;
using Socialease.ViewModels;

namespace Socialease.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly ISocialRepository _repository;

        public AppController(IMailService mailService, ISocialRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var pingTypes = _repository.GetAllPingTypes();
            return View(pingTypes);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            var email = "sergey@sergeyk.com";
            _mailService.SendMail(email, email, model.Name, model.Message);
            return View();
        }
    }
}
