using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
            var user = await _userManager.FindByEmailAsync(model.Email); // шукаєм чи є такій логін в базі
            if (user != null) // якщо юзер не дорівнює налл це означає що в базі є такий користувач і наш емаіл не буде універсальний
                return ValidationProblem();

            var ext = Path.GetExtension(model.Photo.FileName); //отримуємо формат файлу
            string fileName = Path.GetRandomFileName() + ext; //генеруєм рандом імя та додає формат

            var dir = Path.Combine(Directory.GetCurrentDirectory(), "images"); //витягуємо шлях
            var filePath = Path.Combine(dir, fileName); // комбінуєм шлях і назву файла
            using (var stream = System.IO.File.Create(filePath)) { model.Photo.CopyTo(stream); } // кідаєм в поток і записуєм у папку


            user = new AppUser
            {
                Email = model.Email,
                Login = model.Login,
                UserName = model.Login,
                Photo = fileName

            };

            var result = await _userManager.CreateAsync(user, model.Password);

            return Ok();
        }
        



    }
}
