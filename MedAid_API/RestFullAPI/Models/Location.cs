using MedAidAPI.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedAidAPI.Models
{
    public class Location
    {
        public int Id { get; set; }

        public string LocationShortName { get; set; }
        public string LocationTitle { get; set; }
        public string LocationLatLong { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public MedAidAPIUser MedAidUser { get; set; }
        public int LocationTypeId { get; set; }
        [ForeignKey("LocationTypeId")]
        public LocationType LocationType { get; set; }

    }
}
