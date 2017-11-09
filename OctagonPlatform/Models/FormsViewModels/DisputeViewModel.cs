﻿using OctagonPlatform.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class DisputeViewModel
    {
        public int Id { get; set; }

        public bool Viewed { get; set; }

        public string MessageNumber { get; set; }

        public string NetworkAdjustmentId { get; set; }

        [Required]
        public int TerminalId { get; set; }
        public Terminal Terminal { get; set; }

        [Required]
        public int TransactionId { get; set; }

        public Transaction Transaction { get; set; }

        public string Network { get; set; }

        public DisputeType.Disputes DisputeType { get; set; }

        public string SecuenceNumber { get; set; }

        public decimal AmountRequested { get; set; }

        public decimal DisputedAmount { get; set; }

        public DateTime LastDayToRepresent { get; set; }
    }
}