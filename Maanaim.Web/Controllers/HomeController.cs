using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Maanaim.Models;
using Microsoft.Extensions.Options;
using Maanaim.Web.Models;
using Maanaim.Model;
using Maanaim.Web.ViewModel;
using Maanaim.Web.ExtensionMethods;


namespace Maanaim.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MongoDBStorageConfig> config;

        public HomeController(IOptions<MongoDBStorageConfig> config)
        {
            this.config = config;
        }

        public IActionResult Index()
        {
            var colCalendar = new Data.DataCalendar(config.Value.ConnectionString).List().ToList<Calendar>();
            return View("~/Views/Adm/Index.cshtml",colCalendar);
        }

        [HttpGet]
        public IActionResult Visualizar(string Id)
        {
            CalendarForm calendarForm = (new Data.DataCalendar(config.Value.ConnectionString)).Find(Id).ToCalendarForm();

            return View("~/Views/Adm/Visualizar.cshtml", calendarForm);
        }
    }
}
