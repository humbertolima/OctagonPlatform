using OctagonPlatform.Models;
using OctagonPlatform.Models.DetailsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Repositories.PartnerContacts
{
    internal interface IPartnerContactRepository: IGenericRepository<PartnerContact>
    {
        IEnumerable<PartnerContact> GetPartnerContactses();

        PartnerDetailsViewModel PartnerContactDetails();


    }
}
