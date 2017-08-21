using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OctagonPlatform.Models;

namespace OctagonPlatform.Repositories.User
{
    public class UserRepository : GenericRepository<Models.User>
    {

        public UserRepository()
        {
            var user = this.Table.Single();
        }
    }
}