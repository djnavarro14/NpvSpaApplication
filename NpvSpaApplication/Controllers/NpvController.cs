using NpvSpaApplication.Models;
using System.Web.Mvc;

namespace NpvSpaApplication.Controllers
{
    public class NpvController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Npv Calculator";

            return View(new NpvObjectModel());
        }
    }
}
