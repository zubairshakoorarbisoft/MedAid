using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAidAPI.Areas.Identity.Data;
using MedAidAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MedAidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicinesController : ControllerBase
    {
        private readonly MedAidAPIContext _context;
        public MedicinesController(MedAidAPIContext context)
        {
            _context = context;
        }
        // GET api/medicines
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(_context.Medicines.Include(s => s.Store).ToList());
        }

        // GET api/medicines/5
        [HttpGet]
        [Route("searchmedicinebyname")]
        public string SearchMedicineByName(string searchMedicine)
        {
            return JsonConvert.SerializeObject(_context.Medicines.Where(m => m.Name.Contains(searchMedicine)).Include(s => s.Store).ToList());
        }

        // GET api/medicines/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(_context.Medicines.Include(s => s.Store).FirstOrDefault(m => m.Id == id));
        }

        // POST: api/Medicines
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Medicines/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
