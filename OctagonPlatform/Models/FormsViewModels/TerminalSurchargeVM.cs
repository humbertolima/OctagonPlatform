using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalSurchargeVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public List<Surcharge> Surcharges { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double SurchargeAmountFee { get; set; }

        [Required]
        [Display(Name = "Percent Surcharge Fee")]
        public double SurchargePercentageFee { get; set; }

        [Required]
        [Display(Name = "Surcharged Type")]
        public SurchargedBy.SurchargeTypes SurchargeType { get; set; }
    }
}