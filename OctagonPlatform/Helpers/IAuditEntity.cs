using OctagonPlatform.Models;
using System;

namespace OctagonPlatform.Helpers
{
    internal interface IAuditEntity
    {
        DateTime? CreatedAt { get; set; }

        User CreatedBy { get; set; }

        DateTime? DeletedAt { get; set; }

        User DeletedBy { get; set; }

        DateTime? UpdatedAt { get; set; }

        User UpdatedBy { get; set; }
    }
}
