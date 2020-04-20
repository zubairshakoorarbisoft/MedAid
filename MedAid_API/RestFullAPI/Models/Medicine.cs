using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedAidAPI.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SaltFormula { get; set; }
        public string Brand { get; set; }
        public string ForDiseas { get; set; }
        public double AvailableQuantity { get; set; }
        public double RetailPrice { get; set; }
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
