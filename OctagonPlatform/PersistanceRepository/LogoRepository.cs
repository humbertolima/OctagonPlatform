using OctagonPlatform.Models;
using OctagonPlatform.Models.InterfacesRepository;
using System.IO;
using System.Linq;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class LogoRepository: GenericRepository<Partner>,ILogoRepository
    { 
        public void UploadLogo(HttpPostedFileBase file, int partnerId)
        {
            if (file == null) return;
            var partner = Table.SingleOrDefault(x => x.Id == partnerId);
            if (partner == null) return;
            var path = "~/Conten/Uploads/PartnerLogos/" + partner.BusinessName + ".jpg";
            DeleteLogo(path);
            file.SaveAs(path);
            partner.Logo = path;
            Save();
        }

        public void DeleteLogo(string path)
        {

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}