using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Linq;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class LogoRepository: ILogoRepository
    {
        private readonly ApplicationDbContext _context;

        public LogoRepository()
        {
            _context = new ApplicationDbContext();
        }
        public string ShowLogo(int partnerId)
        {
            var partnerLogo = _context.Partners.FirstOrDefault(x => x.Id == partnerId && !x.Deleted)?.Logo;
            return partnerLogo + "/logo.jpg";
        }

        public bool UploadLogo(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }

        public bool DeleteLogo(string path)
        {
            throw new NotImplementedException();
        }
    }
}