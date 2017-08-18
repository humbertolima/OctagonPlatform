using OctagonPlatform.Models;
using OctagonPlatform.Models.DetailsViewModels;
using System.Collections.Generic;

namespace OctagonPlatform.Repositories.PartnerContacts
{
    public class PartnerContactRepository:GenericRepository<PartnerContact>, IPartnerContactRepository
    {
        public IEnumerable<PartnerContact> GetPartnerContactses()
        {
            throw new System.NotImplementedException();
        }

        public PartnerViewModel PartnerContactDetails()
        {
            throw new System.NotImplementedException();
        }
    }
}