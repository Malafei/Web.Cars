using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Abstract;
using Web.Cars.Data.Identity;
using Web.Cars.Exceptions;
using Web.Cars.Models;
using Web.Cars.Services;

namespace Web.Cars.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AcountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTocenService _tokenService;


        public AcountController(IUserService userService, UserManager<AppUser> userManager, IJwtTocenService tokenService)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterVievModel model)
        {
            try
            {
                string token = await _userService.CreateUser(model);

                return Ok(new
                {
                    token
                });
            }
            catch(AccountException aex)
            {
                return BadRequest(aex._accountError);
            }
            catch(Exception ex)
            {
                return BadRequest(new AccountError("Щось пішло не так! " + ex.Message));
            }


        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginVievModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    string token = _tokenService.CreateToken(user);
                    return Ok(
                        new { token }
                    );
                }
                else
                {

                    var exc = new AccountError();
                    exc.Errors.Invalid.Add("Пароль не вірний!");
                    throw new AccountException(exc);
                }
            }
            catch (AccountException aex)
            {
                return BadRequest(aex._accountError);
            }
            catch
            {
                return BadRequest(new AccountError("Щось пішло не так!"));
            }
        }

    }
}
