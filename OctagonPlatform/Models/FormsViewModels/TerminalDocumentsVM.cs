using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalDocumentsVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public bool IsAddDocuments { get; set; }
        public bool IsDeleteDocuments { get; set; }

        public List<Document> Documents { get; set; }
    }
}