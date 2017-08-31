using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ILogoRepository
    {

        void UploadLogo(HttpPostedFileBase file, int partnerId);

        void DeleteLogo(string path);
    }
}
