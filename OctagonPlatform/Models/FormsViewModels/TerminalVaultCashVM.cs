using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.FormsViewModels
{
    public class TerminalVaultCashVM
    {
        [Required]
        public int Id { get; set; }

        public bool IsAddVaultCash { get; set; }
        public bool IsEditVaultCash { get; set; }
        public bool IsDeleteVaulCash { get; set; }

        [Required]
        public string TerminalId { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public VaultCash VaultCash { get; set; }
    }
}