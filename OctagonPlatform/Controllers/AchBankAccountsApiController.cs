using OctagonPlatform.Models;
using System.Web.Http;

namespace OctagonPlatform.Controllers
{
    public class AchBankAccountsApiController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AchBankAccountsApiController()
        {
            _context = new ApplicationDbContext();
        }
        public AchBankAccountsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Devolver 
    }
}
