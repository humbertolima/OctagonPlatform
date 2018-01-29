using OctagonPlatform.Controllers.Reports;
using OctagonPlatform.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OctagonPlatform.PersistanceRepository
{
    public static class Utils
    {
        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            // Create the result table, and gather all properties of a T        
            DataTable table = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (var prop in props)
            {
                Type propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type 
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    propType = new NullableConverter(propType).UnderlyingType;

                GridMvc.DataAnnotations.GridColumnAttribute dn = prop.GetCustomAttribute(typeof(GridMvc.DataAnnotations.GridColumnAttribute)) as GridMvc.DataAnnotations.GridColumnAttribute;
                string aname = dn?.Title ?? prop.Name ;

                table.Columns.Add(aname, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                table.Rows.Add(values);
            }

            return table;
        }


        public static DataTable ToDataTable(this IEnumerable items, Type type)
        {
            // Create the result table, and gather all properties of a T        
            DataTable table = new DataTable(type.Name);
            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (var prop in props)
            {
                Type propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type 
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    propType = new NullableConverter(propType).UnderlyingType;

                GridMvc.DataAnnotations.GridColumnAttribute dn = prop.GetCustomAttribute(typeof(GridMvc.DataAnnotations.GridColumnAttribute)) as GridMvc.DataAnnotations.GridColumnAttribute;
                string aname = dn?.Title ?? prop.Name;

                table.Columns.Add(aname, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                    values[i] = props[i].GetValue(item, null);

                table.Rows.Add(values);
            }

            return table;
        }



        public static DateTime GetFirstDayOfWeek(DateTime dayInWeek)
        {
            CultureInfo defaultCultureInfo = CultureInfo.CurrentCulture;
            return GetFirstDayOfWeek(dayInWeek, defaultCultureInfo);
        }

        /// <summary>
        /// Returns the first day of the week that the specified date 
        /// is in. 
        /// </summary>
        private static DateTime GetFirstDayOfWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }


        //private async Task<string> RenderPartialViewToString(string viewName, object model)
        //{
        //    if (string.IsNullOrEmpty(viewName))
        //        viewName = ControllerContext.ActionDescriptor.ActionName;

        //    ViewData.Model = model;

        //    using (var writer = new StringWriter())
        //    {
        //        ViewEngineResult viewResult =
        //            _viewEngine.FindView(ControllerContext, viewName, false);

        //        ViewContext viewContext = new ViewContext(
        //            ControllerContext,
        //            viewResult.View,
        //            ViewData,
        //            TempData,
        //            writer,
        //            new HtmlHelperOptions()
        //        );

        //        await viewResult.View.RenderAsync(viewContext);

        //        return writer.GetStringBuilder().ToString();
        //    }
        //}

        public static async Task<string> GenerateReport(ReportsSmartController controller_smart, object model) 
        {

            try{

                Type type = controller_smart.GetType();

                string method_name = type.Name.Substring(0, type.Name.Length - 10);

                //ReportsSmartController handle = controller_smart; // DependencyResolver.Current.GetService(type) as ReportsSmartController;   // Activator.CreateInstance(type);

                MethodInfo method = type.GetMethod(method_name, new Type[] { model.GetType()});

                ActionResult result = await (method.Invoke(controller_smart, new object[] { model })   as  Task<ActionResult>);

                return await Task<string>.Run(async () => {
                    ActionResult ar = result; // await result;

                    if (!(ar is ViewResult))
                    {
                        return  "";
                    }

                    ViewResult vr = (ar as ViewResult);
                    StringWriter sw = new StringWriter();

                   ViewContext viewContext = new ViewContext(controller_smart.ControllerContext, vr.View, vr.ViewData, vr.TempData, sw );

                    vr.View.Render(viewContext, sw);

                    return sw.GetStringBuilder().ToString(); ;
                });

            }
            catch( Exception ex)
            {
                return await Task<string>.FromResult("");
            }
           
        }
        public static DateTime ToTimeZoneTime(DateTime time, string timeZoneId = "Pacific Standard Time")
        {
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return ToTimeZoneTime(time,tzi);
        }

     
        public static DateTime ToTimeZoneTime(DateTime time, TimeZoneInfo tzi)
        {
            var localDateTime = DateTime.SpecifyKind(time, DateTimeKind.Unspecified); 
            DateTime tstTime2 = TimeZoneInfo.ConvertTime(localDateTime,  tzi);
            return tstTime2;
        }
        private static string NextRunDate(Schedule schedule)
        {
            string datestart = schedule.StartDate.ToShortDateString();
            string daterun = "";
            DateTime today = DateTime.Now;          
          
            if (schedule is ScheduleOnce)
            {
                datestart += " " + ((ScheduleOnce)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                    daterun = "One execution";
            }
            if (schedule is ScheduleDaily)
            {
                datestart += " " + ((ScheduleDaily)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {
                    string nextday = today.AddDays(((ScheduleDaily)schedule).RepeatOn).ToShortDateString();
                    daterun = nextday + " " + ((ScheduleDaily)schedule).Time;
                }
            }
            if (schedule is ScheduleWeekly)
            {
                datestart += " " + ((ScheduleWeekly)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {
                    ScheduleWeekly week = ((ScheduleWeekly)schedule);
                    string[] days = week.RepeatOnDaysWeeks.Split('_');
                    int days_week = week.RepeatOnWeeks * 7;
                    DateTime nextweek = today.AddDays(days_week);
                    DateTime first_date_week = Utils.GetFirstDayOfWeek(nextweek);
                    DateTime nextrun = GetNextRun(first_date_week, days[0]);
                    daterun = nextrun.ToShortDateString() + " " + ((ScheduleWeekly)schedule).Time;

                }
            }
            if (schedule is ScheduleMonthly)
            {
                datestart += " " + ((ScheduleMonthly)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {
                    ScheduleMonthly month = ((ScheduleMonthly)schedule);
                    int day = month.RepeatOnDay;
                    int every_month = month.RepeatOnMonth;

                    DateTime next_month = today.AddMonths(every_month);

                    daterun = next_month.Month + "/" + day + "/" + next_month.Year + " " + ((ScheduleMonthly)schedule).Time;

                }
            }
            if (schedule is ScheduleMonthlyRelative)
            {
                datestart += " " + ((ScheduleMonthlyRelative)schedule).Time;
                DateTime dt = Convert.ToDateTime(datestart);
                if (dt > DateTime.Now)
                    daterun = dt.ToString();
                else
                {
                    ScheduleMonthlyRelative month_relative = ((ScheduleMonthlyRelative)schedule);
                    int first = Convert.ToInt32(month_relative.RepeatOnFirst); // 0 is last day
                    string name_day = month_relative.RepeatOnDay;
                    int every_month = month_relative.RepeatOnMonth;
                    DateTime next_month = today.AddMonths(every_month);
                    var firstDayOfMonth = new DateTime(next_month.Year, next_month.Month, 1);

                    DateTime nextrun = new DateTime();
                    DateTime first_date_week = firstDayOfMonth;//si es first == 1 , es la primera semana 
                    if (first > 1)
                    {
                        int days_week = first * 7;
                        DateTime nextweek = firstDayOfMonth.AddDays(days_week);
                        first_date_week = Utils.GetFirstDayOfWeek(nextweek);

                    }
                    if (first == 0) //Ultimo del mes
                    {
                        var last_week = firstDayOfMonth.AddMonths(1).AddDays(-3);
                        first_date_week = Utils.GetFirstDayOfWeek(last_week);
                    }

                    nextrun = GetNextRunMonth(first_date_week, name_day);
                    daterun = nextrun.ToShortDateString() + " " + ((ScheduleMonthlyRelative)schedule).Time;

                }
            }
           
            return daterun;
        }
        private static DateTime GetNextRun(DateTime date_week, string dayrun)
        {
            if (dayrun != "Day" && dayrun != "week_day")
            {
                string day = date_week.DayOfWeek.ToString().Substring(0, 3);
                dayrun = dayrun == "weekend_day" ? "Sat" : dayrun;
                if (day == dayrun)
                    return date_week;
                else
                {
                    DateTime next = date_week.AddDays(1);
                    return GetNextRun(next, dayrun);
                }
            }
            return date_week;


        }
        private static DateTime GetNextRunMonth(DateTime date_week, string dayrun)
        {

            string day = date_week.DayOfWeek.ToString().Substring(0, 3);
            if (day == dayrun)
                return date_week;
            else
            {
                DateTime next = date_week.AddDays(1);
                return GetNextRun(next, dayrun);
            }

        }
        public static DateTime? NextRunDateSchedule(Schedule schedule)
        {
            string datetime = NextRunDate(schedule);
            if (datetime != "One execution")
            {
                DateTime timerun = Utils.ToTimeZoneTime(Convert.ToDateTime(datetime), schedule.User.TimeZoneInfo);
                datetime = timerun.ToShortDateString() + " " + Convert.ToDateTime(datetime).ToShortTimeString();
                return Convert.ToDateTime(datetime);
            }
            return null;
        }

    }


    public static  class ErrorCtrl
    {
        public static void save(Action action)
        {
            try {
                action.Invoke();
            } catch (Exception ex) {

                Console.WriteLine(ex.ToString());
            }
        }
    }


    public static class RazorViewToString
    {
        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
               // var razorViewEngine = new RazorViewEngine();
               // viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


        //public static string RenderViewToString(string controllerName, string viewName, object viewData, Controller ctrl)
        //{
        //    using (var writer = new StringWriter())
        //    {
        //        var routeData = new RouteData();
        //        routeData.Values.Add("controller", controllerName);
        //        var fakeControllerContext = new ControllerContext(new HttpContextWrapper(new HttpContext(new HttpRequest(null, "http://google.com", null), new HttpResponse(null))), routeData, ctrl);
        //        var razorViewEngine = new RazorViewEngine();
        //        var razorViewResult = razorViewEngine.FindView(fakeControllerContext, viewName, "", false);

        //        var viewContext = new ViewContext(fakeControllerContext, razorViewResult.View, new ViewDataDictionary(viewData), new TempDataDictionary(), writer);
        //        razorViewResult.View.Render(viewContext, writer);
        //        return writer.ToString();

        //    }
        //}


    }
}