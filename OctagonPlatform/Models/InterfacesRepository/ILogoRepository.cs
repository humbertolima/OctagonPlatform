using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ILogoRepository
    {
        string ShowLogo(int partnerId);

        bool UploadLogo(HttpPostedFileBase file);

        bool DeleteLogo(string path);
    }
}
