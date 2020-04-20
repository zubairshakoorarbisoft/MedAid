using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MedAidAPI.Areas.Identity.Data;
using System;
using MedAidAPI.ViewModels;
using Newtonsoft.Json;

namespace MedAidAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MedAidAPIContext _context;
        private readonly UserManager<MedAidAPIUser> _userManager;


        public UserController(MedAidAPIContext context, UserManager<MedAidAPIUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/User
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(_context.Users.ToList());
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public string Post([FromBody] UserViewModel user)
        {
            try
            {
                if (_userManager.FindByEmailAsync(user.EmailAddress).Result == null)
                {
                    MedAidAPIUser newUser = new MedAidAPIUser()
                    {
                        EmailConfirmed = true,
                        UserName  = user.Username,
                        Email = user.EmailAddress
                    };
                    
                    IdentityResult result = _userManager.CreateAsync(newUser, user.Password).Result;
                    return JsonConvert.SerializeObject(result);
                }
                else
                {
                    return JsonConvert.SerializeObject(new { Succeeded = false, Description = "User already exist", Code = "300" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        // PUT: api/User/5
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
