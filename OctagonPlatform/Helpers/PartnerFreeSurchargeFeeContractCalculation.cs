using OctagonPlatform.Models;
using System.Linq;

namespace OctagonPlatform.Helpers
{
    public static class PartnerFreeSurchargeFeeContractCalculation
    {
        public static void Calculation()
        {
            var context = new ApplicationDbContext();
            var partners = context.Partners.Where(x => !x.Deleted).ToList();

            foreach (var item in partners)
            {
                item.FreeSurchargeFeeContractCalculation();
            }
        }
    }
}