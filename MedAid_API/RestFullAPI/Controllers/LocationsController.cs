using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedAidAPI.Areas.Identity.Data;
using MedAidAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MedAidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly MedAidAPIContext _context;
        public LocationsController(MedAidAPIContext context)
        {
            _context = context;
        }
        // GET api/locations
        [HttpGet]
        public string Get()
        {
            var locations = _context.Locations.Include(s => s.LocationType).ToList();
            if (locations.Count > 0)
                return JsonConvert.SerializeObject(locations, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            else
                return "No Location Found";
        }

        [HttpGet]
        [Route("getUserLocations")]
        public string GetUserLocations(string userId)


        {
            var locations = _context.Locations.Where(u => u.UserId == userId).Include(s => s.LocationType).ToList();
            if (locations.Count > 0)
                return JsonConvert.SerializeObject(locations, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
            else
                return "No Location Found for this User";
        }


        // GET api/locations/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(_context.Locations.Include(s => s.LocationType).FirstOrDefault(m => m.Id == id), Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
        }

        // POST: api/locations
        [HttpPost]
        public string Post([FromBody]Location locationModel)
        {
            try
            {
                _context.Locations.Add(locationModel);
                _context.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        // PUT: api/locations/5
        [HttpPost]
        [Route("update")]
        public string update([FromBody]Location locationModel)
        {
            try
            {
                var model = _context.Locations.FirstOrDefault(i => i.Id == locationModel.Id);
                if (model == null)
                {
                    return "404";
                }
                model.LocationLatLong = locationModel.LocationLatLong;
                model.LocationShortName = locationModel.LocationShortName;
                model.LocationTitle = locationModel.LocationTitle;
                model.LocationTypeId = locationModel.LocationTypeId;
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return "true";

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                var model = _context.Locations.FirstOrDefault(i => i.Id == id);
                if (model == null)
                {
                    return "404";
                }

                _context.Locations.Remove(model);
                _context.SaveChanges();
                return "true";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
