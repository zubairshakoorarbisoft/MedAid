using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedAidAPI.Models
{
    public class AlarmType
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }

        public ICollection<Alarm> Alarms { get; set; }
    }
}
