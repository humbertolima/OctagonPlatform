using OctagonPlatform.Models.InterfacesRepository;
using System;
using System.Web;

namespace OctagonPlatform.PersistanceRepository
{
    public class LogoRepository: ILogoRepository
    {
        public void UploadLogo(HttpPostedFileBase file)
        {
            throw new NotImplementedException();
        }

        public void EditLogo(HttpPostedFileBase file, string filePath)
        {
            throw new NotImplementedException();
        }

        public void DeleteLogo(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}