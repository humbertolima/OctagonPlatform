using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OctagonPlatform.Models
{
    [Table("CultureInfo")]
    public class CultureInfoModel
    {
       
        [Key]
        public int Id { get; set; }
        
        [StringLength(10)]
        public string Name { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }     


    }
}