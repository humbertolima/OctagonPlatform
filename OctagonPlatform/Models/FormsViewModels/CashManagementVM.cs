

namespace OctagonPlatform.Models.FormsViewModels
{
    public class CashManagementVM
    {
        public int Id { get; set; }

        public int PreviousBalance { get; set; }

        public int AmmountLoaded { get; set; }

        public int CurrentBalance { get; set; }

        public int TerminalId { get; set; }
    }
}