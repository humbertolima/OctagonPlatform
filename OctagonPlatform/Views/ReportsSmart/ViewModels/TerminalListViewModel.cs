using GridMvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Views.ReportsSmart.ViewModels
{
    [GridTable(PagingEnabled = true, PageSize = 20)]
    public class TerminalListViewModel
    {
      
        [DisplayName("Email")]       
        [GridColumn(Title = "Email", SortEnabled = true, FilterEnabled = true)]
        public string Email { get; set; }
       
    }
}