using OctagonPlatform.Controllers.Reports;
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

            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, tzi);


            
            return utcDateTime;
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