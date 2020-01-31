using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedAidAPI.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreLatLong { get; set; }
        public string StoreLocationTitle { get; set; }
        public string StoreContactNo { get; set; }
        public string StoreEmailAddress { get; set; }
    }
}
