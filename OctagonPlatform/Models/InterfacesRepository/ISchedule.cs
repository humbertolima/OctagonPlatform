

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OctagonPlatform.Models.InterfacesRepository
{
    public interface ISchedule : IGenericRepository<Schedule>
    {
        IEnumerable<Schedule> GetScheduleByUser(int userId);
        IEnumerable<Schedule> GetScheduleByParent(int partnerId);
        IEnumerable<string> GetAllSchedule(string value, int userId);
    }

}