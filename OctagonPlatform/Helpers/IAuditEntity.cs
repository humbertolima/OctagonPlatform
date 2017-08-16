using OctagonPlatform.Models;
using System;

namespace OctagonPlatform.Helpers
{
    internal interface IAuditEntity
    {
        DateTime? CreatedAt { get; set; }

        int CreatedBy { get; set; }

        DateTime? DeletedAt { get; set; }

        int DeletedBy { get; set; }

        DateTime? UpdatedAt { get; set; }

        int UpdatedBy { get; set; }
    }
}
