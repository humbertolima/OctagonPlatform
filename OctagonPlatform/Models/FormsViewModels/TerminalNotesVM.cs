﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalNotesVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public bool IsAddNotes { get; set; }
        public bool IsEditNotes { get; set; }
        public bool IsDeleteNotes { get; set; }
        
        public List<Note> Notes { get; set; }
    }
}