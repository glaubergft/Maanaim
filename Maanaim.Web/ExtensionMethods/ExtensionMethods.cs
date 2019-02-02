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
                                      BackgroundColor = calendar.BackgroundColor,
                                      TextColor = calendar.TextColor
                                  };
            return result;
        }

        public static string StripHTML(this string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", String.Empty).Replace("&nbsp;", " ");
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

        public static string ToFullCalendar(this DateTime dateTime)
        {
            string result = null;

            if (dateTime.TimeOfDay.TotalSeconds == 0)
                result = dateTime.ToString("yyy-MM-dd");
            else
                result = dateTime.ToString("yyy-MM-dd HH:mm:ss");
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
                BackgroundColor = calendarForm.BackgroundColor,
                TextColor = calendarForm.TextColor
            };
            if (!string.IsNullOrEmpty(calendarForm.Id))
            {
                result.Id = ObjectId.Parse(calendarForm.Id);
            }
            return result;
        }
    }
}
