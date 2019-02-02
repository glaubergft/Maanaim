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

namespace Maanaim.Web.Controllers
{
    public class AdmController : Controller
    {
        private readonly IOptions<MongoDBStorageConfig> config;

        public AdmController(IOptions<MongoDBStorageConfig> config)
        {
            this.config = config;
        }

        public IActionResult Index()
        {
            var colCalendar = new Data.DataCalendar(config.Value.ConnectionString).List().ToList<Calendar>();
            return View(colCalendar);
        }

        public IActionResult Gerenciar()
        {
            var colCalendar = new Data.DataCalendar(config.Value.ConnectionString).List().ToList<Calendar>();
            var colCalendarForm = from calendar in colCalendar
                                  select calendar.ToCalendarForm();
            return View(colCalendarForm.ToList());
        }

        [HttpGet]
        public IActionResult Visualizar(string Id)
        {
            CalendarForm calendarForm = (new Data.DataCalendar(config.Value.ConnectionString)).Find(Id).ToCalendarForm();

            return View("Visualizar", calendarForm);
        }

        [HttpGet]
        public IActionResult IncluirAlterar(string Id)
        {
            CalendarForm calendarForm;
            if (!string.IsNullOrEmpty(Id))
                calendarForm = (new Data.DataCalendar(config.Value.ConnectionString)).Find(Id).ToCalendarForm();
            else
            {
                calendarForm = new CalendarForm();
                calendarForm.StartDate = DateTime.Now.ToString("dd/MM/yyyy") + " 18:00:00";
                calendarForm.EndDate = calendarForm.StartDate;
            }

            return View("IncluirAlterar", calendarForm);
        }

        [HttpPost]
        public IActionResult IncluirAlterar(CalendarForm calendarForm)
        {


            try
            {

                var calendar = calendarForm.ToCalendar();

                if (string.IsNullOrEmpty(calendarForm.Id))
                {
                    (new Data.DataCalendar(config.Value.ConnectionString)).Insert(calendar);
                    calendarForm = new CalendarForm();
                    calendarForm.StartDate = DateTime.Now.ToString("dd/MM/yyyy") + " 18:00:00";
                    calendarForm.EndDate = calendarForm.StartDate;
                    ViewData["Message"] = "Programação incluída com sucesso!";
                }
                else
                {
                    (new Data.DataCalendar(config.Value.ConnectionString)).Update(calendar);
                    ViewData["Message"] = "Programação alterada com sucesso!";
                }



                ViewData["MessageClass"] = "alert-success";

            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Ocorreu um erro. (" + ex.Message + ")";
                ViewData["MessageClass"] = "alert-danger";
            }



            return View(calendarForm);
        }


        [HttpPost]
        public IActionResult Excluir(CalendarForm calendarForm)
        {
            //ViewBag.Message = "Ocorreu um erro. (" + ex.Message + ")";
            //ViewBag.MessageClass = "alert-danger";
            try
            {

                var calendar = calendarForm.ToCalendar();

                if (string.IsNullOrEmpty(calendarForm.Id))
                {
                    throw new Exception("não foi possível excluir");
                }
                else
                {
                    (new Data.DataCalendar(config.Value.ConnectionString)).Delete(calendar);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocorreu um erro. (" + ex.Message + ")";
                ViewBag.MessageClass = "alert-danger";
            }

            var colCalendar = new Data.DataCalendar(config.Value.ConnectionString).List().ToList<Calendar>();
            var colCalendarForm = from calendar in colCalendar
                                  select calendar.ToCalendarForm();
            return View("Gerenciar", colCalendarForm.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}