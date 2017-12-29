using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OctagonPlatform.Helpers
{
    public static class ViewModelError
    {
        public static string Get(ModelStateDictionary state)
        {
            string error ="";

            foreach (var item in state.Values)
            {
                if (item.Errors.Count() > 0)
                {
                    foreach (var item2 in item.Errors)
                    {
                        error = error + " " + item2.ErrorMessage;
                        if (item2.Exception != null)
                        {
                            error = error + " " + item2.Exception.Message;
                        }
                    }
                }
                
            }
            return error;
        }
    }
}