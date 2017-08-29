using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ILogoRepository
    {
        void UploadLogo(HttpPostedFileBase file);

        void EditLogo(HttpPostedFileBase file, string filePath);

        void DeleteLogo(string filePath);
    }
}
