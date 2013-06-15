using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BeerApp.API.Migrations;

namespace BeerApp.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var configuration = new Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
            ViewBag.Message = "Your app description page.";


            return View();
        }
    }
}
