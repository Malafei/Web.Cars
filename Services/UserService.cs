using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Abstract;
using Web.Cars.Data;
using Web.Cars.Data.Identity;
using Web.Cars.Exceptions;
using Web.Cars.Models;

namespace Web.Cars.Services
{
    public class UserService : IUserService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly AppEFContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        private readonly IJwtTocenService _jwtTokenService;

        public UserService( AppEFContext context, UserManager<AppUser> userManager, IJwtTocenService jwtTokenService, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtTokenService = jwtTokenService;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<string> CreateUser(RegisterVievModel model)
        {
            var user = _mapper.Map<AppUser>(model);

            string fileName = String.Empty;
            if (model.Photo != null)
            {
                string randomFilename = Path.GetRandomFileName() +
                    Path.GetExtension(model.Photo.FileName); //генеруєм рандом імя та додає формат

                string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "images");  //витягуємо шлях
                fileName = Path.Combine(dirPath, randomFilename);  // комбінуєм шлях і назву файла
                using (var stream = System.IO.File.Create(fileName))
                {
                    model.Photo.CopyTo(stream);  // кідаєм в поток і записуєм у папку
                }
                user.Photo = randomFilename;
            }

            // закидаємо юзера в базу
            var result = await _userManager.CreateAsync(user, model.Password);
            // по замовчувані права юзер
            if (!result.Succeeded)
            {
                if (!string.IsNullOrEmpty(fileName))
                    System.IO.File.Delete(fileName);
                AccountError accountError = new AccountError();
                foreach (var item in result.Errors)
                {
                    accountError.Errors.Invalid.Add(item.Description);
                }
                throw new AccountException(accountError); 
            }
            await _userManager.AddToRoleAsync(user, "User");

            //якщо є помилки виводимо їх форічом
            return _jwtTokenService.CreateToken(user);
        }

        public async Task<string> DeleteUser(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (user.Photo != null)
                {
                    var directory = Path.Combine(Directory.GetCurrentDirectory(), "images");
                    var FilePath = Path.Combine(directory, user.Photo);
                    File.Delete(FilePath);
                }
                return "Good";
            }
            else
                return "Bad";
        }
        public void UpdateUser(UserSaveViewModel model)
        {
            try
            {
                var user = _context.Users
                    .SingleOrDefault(x => x.Id == model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.Login = model.Login;
                    string fileName = String.Empty;
                    if (model.Image != null)
                    {
                        string randomFilename = user.Photo;

                        string dirPath = Path.Combine(Directory.GetCurrentDirectory(), "images");
                        fileName = Path.Combine(dirPath, randomFilename);
                        using (var file = File.Create(fileName))
                        {
                            model.Image.CopyTo(file);
                        }
                    }
                    _context.SaveChanges();

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
