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
        private readonly SocialContext _context;

        public AppController(IMailService mailService, SocialContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            var people = _context.People.OrderBy(p => p.Name).ToList();
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
