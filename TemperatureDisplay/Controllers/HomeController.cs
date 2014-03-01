using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temperature_Display.Models;

namespace Temperature_Display.Controllers
{
    public class HomeController : Controller
    {
        private TemperatureDisplayContext db = new TemperatureDisplayContext();

        public ActionResult Index()
        {
            var readings = db.TemperatureReadings.OrderByDescending(x=> x.Date).Take(5).ToList();
            return View(readings);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}