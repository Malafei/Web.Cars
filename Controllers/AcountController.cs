using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Data.Identity;
using Web.Cars.Models;

namespace Web.Cars.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AcountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public AcountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterVievModel model)
        {

            return Ok();
        }
        



    }
}
