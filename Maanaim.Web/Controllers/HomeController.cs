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

namespace Maanaim.Controllers
{
    public class HomeController : Controller
    {

        private readonly IOptions<MongoDBStorageConfig> config;

        public HomeController(IOptions<MongoDBStorageConfig> config)
        {
            //var colCalendar = new Data.DataCalendar(config.Value.ConnectionString).List().ToList<Calendar>();
            //int i = colCalendar.Count();
            this.config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public string Index()
        //{
        //    return config.Value.ConnectionString;
        //}

        public IActionResult Gerenciar()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Incluir()
        {
            return View();
        }
        /*
        [HttpPost]
        public JsonResult Incluir(CalendarForm calendarForm)
        {
            dynamic result = new System.Dynamic.ExpandoObject();
            result.ErrorMessage = "";

            try
            {
                string[] startDateSplit = calendarForm.StartDate.Split('/');
                string[] endDateSplit = calendarForm.EndDate.Split('/');

                var startDate = new DateTime(
                    int.Parse(startDateSplit[2].Split(' ')[0]), //ano
                    int.Parse(startDateSplit[1]),
                    int.Parse(startDateSplit[0])
                    );

                var endDate = new DateTime(
                    int.Parse(endDateSplit[2].Split(' ')[0]), //ano
                    int.Parse(endDateSplit[1]),
                    int.Parse(endDateSplit[0])
                    );

                var calendar = new Calendar()
                {
                    Title = calendarForm.Title,
                    Description = calendarForm.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Color = calendarForm.Color
                };

                (new Data.DataCalendar(config.Value.ConnectionString)).Insert(calendar);
                
            }
            catch(Exception ex)
            {
                result.ErrorMessage = ex.Message;
            }

            

            return new JsonResult(result);
        }
        */

        [HttpPost]
        public IActionResult Incluir(CalendarForm calendarForm)
        {
            

            try
            {
                string[] startDateSplit = calendarForm.StartDate.Split('/');
                string[] endDateSplit = calendarForm.EndDate.Split('/');

                var startDate = new DateTime(
                    int.Parse(startDateSplit[2].Split(' ')[0]), //ano
                    int.Parse(startDateSplit[1]),
                    int.Parse(startDateSplit[0])
                    );

                var endDate = new DateTime(
                    int.Parse(endDateSplit[2].Split(' ')[0]), //ano
                    int.Parse(endDateSplit[1]),
                    int.Parse(endDateSplit[0])
                    );

                var calendar = new Calendar()
                {
                    Title = calendarForm.Title,
                    Description = calendarForm.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Ministerio = calendarForm.Ministerio,
                    Color = calendarForm.Color
                };

                (new Data.DataCalendar(config.Value.ConnectionString)).Insert(calendar);

                ViewData["Message"] = "Programação incluída com sucesso!";
                ViewData["MessageClass"] = "alert-success";

            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Ocorreu um erro. (" + ex.Message + ")";
                ViewData["MessageClass"] = "alert-danger";
            }



            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
