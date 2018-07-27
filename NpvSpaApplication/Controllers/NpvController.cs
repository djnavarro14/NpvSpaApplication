using NpvSpaApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NpvSpaApplication.Controllers
{
    public class NpvController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(new NpvObjectModel());
        }
        public ActionResult List()
        {
            ViewBag.Title = "List Page";

            return View();
        }
        public ActionResult Details()
        {
            ViewBag.Title = "Details Page";

            return View();
        }
    }
}
