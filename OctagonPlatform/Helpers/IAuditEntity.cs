using System;
using System.ComponentModel.DataAnnotations;

namespace OctagonPlatform.Helpers
{
    internal interface IAuditEntity
    {
        [DataType(DataType.DateTime, ErrorMessage = "Created date is not valid")]
        [Display(Name = "Created At")]
        DateTime? CreatedAt { get; set; }

        [Display(Name = "Created By")]
        int? CreatedBy { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Created date is not valid")]
        [Display(Name = "Deleted At")]
        DateTime? DeletedAt { get; set; }

        [Display(Name = "Deleted By")]
        int? DeletedBy { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Updated date is not valid")]
        [Display(Name = "Updated At")]
        DateTime? UpdatedAt { get; set; }

        [Display(Name = "Updated By")]
        int? UpdatedBy { get; set; }

        string UpdatedByName { get; set; }

        string CreatedByName { get; set; }

        string DeletedByName { get; set; }
    }
}
