using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroToleranceWebApp.Models;
using ZeroToleranceWebApp.ViewModels;

namespace ZeroToleranceWebApp.Controllers
{
    public class HomeController : Controller
    {
        ZeroToleranceDBEntities zeroToleranceDBEntities = new ZeroToleranceDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            USER user = zeroToleranceDBEntities.USERs.SingleOrDefault(u => u.Email == loginViewModel.Email && u.Password == loginViewModel.Password);
            if (user != null)
            {
                Session["LoggedInUser"] = user;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Errormessage = "Wrong Credentials";
                return View();
            }

        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(USER user)
        {
            USER User = zeroToleranceDBEntities.USERs.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (User != null)
            {
                ViewBag.Errormessage = "User Already Exists.";
                return View();
            }
            if (ModelState.IsValid)     //lathi
            {
                zeroToleranceDBEntities.USERs.Add(user);
                zeroToleranceDBEntities.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            else
            {
                return View();
            }
        }
    }
}