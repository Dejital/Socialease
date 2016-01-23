using Microsoft.AspNet.Mvc;
using Socialease.Services;
using Socialease.ViewModels;

namespace Socialease.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
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
