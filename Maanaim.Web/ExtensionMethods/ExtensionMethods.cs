using Maanaim.Model;
using Maanaim.Web.ViewModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maanaim.Web.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static CalendarForm ToCalendarForm(this Calendar calendar)
        {
            var result = new CalendarForm
                                  {
                                      Id = calendar.Id.ToString(),
                                      Title = calendar.Title,
                                      Description = calendar.Description,
                                      StartDate = calendar.StartDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                      EndDate = calendar.EndDate.ToString("dd/MM/yyyy HH:mm:ss"),
                                      Ministry = calendar.Ministry,
                                      Color = calendar.Color
                                  };
            return result;
        }

        public static DateTime FromBrToDateTime(this string dateTime)
        {
            string[] dateSplit = dateTime.Split('/');
            string[] timeSplit = dateTime.Split(' ')[1].Split(':');
           
            var result = new DateTime(
                int.Parse(dateSplit[2].Split(' ')[0]), //ano
                int.Parse(dateSplit[1]),
                int.Parse(dateSplit[0]),
                int.Parse(timeSplit[0]),
                int.Parse(timeSplit[1]),
                int.Parse(timeSplit[2])
                );

            return result;
        }

        public static Calendar ToCalendar(this CalendarForm calendarForm)
        {
            DateTime startDate;
            DateTime endDate;

            if (!string.IsNullOrEmpty(calendarForm.StartDate))
                startDate = calendarForm.StartDate.FromBrToDateTime();
            else
                startDate = new DateTime();

            if (!string.IsNullOrEmpty(calendarForm.EndDate))
                endDate = calendarForm.EndDate.FromBrToDateTime();
            else
                endDate = new DateTime();

            var result = new Calendar()
            {
                Title = calendarForm.Title,
                Description = calendarForm.Description,
                StartDate = startDate,
                EndDate = endDate,
                Ministry = calendarForm.Ministry,
                Color = calendarForm.Color
            };
            if (!string.IsNullOrEmpty(calendarForm.Id))
            {
                result.Id = ObjectId.Parse(calendarForm.Id);
            }
            return result;
        }
    }
}
