using MedAidAPI.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedAidAPI.Models
{
    public class Alarm
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Prescription { get; set; }
        public string AlarmTimesCommaSeprated { get; set; }

        public string FromLocationLatLong { get; set; }

        public string ToLocationLatLong { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public MedAidAPIUser MedAidUser { get; set; }
        
        [DefaultValue(true)]
        public bool isActive { get; set; }

        public int? AlarmTypeId { get; set; }
        [ForeignKey("AlarmTypeId")]
        public AlarmType AlarmType { get; set; }

    }
}
