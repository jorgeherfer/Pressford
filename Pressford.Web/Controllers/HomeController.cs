using Pressford.DataAccess;
using Pressford.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pressford.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserRepository userRepository;
        public HomeController()
        {
            userRepository = new UserRepository();
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyAccount()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult News()
        {
            ViewBag.IsUserPublisher = userRepository.GetUser(User.Identity.Name).UserType == DataAccess.Model.UserType.Publisher;
            return View();
        }

    }
}