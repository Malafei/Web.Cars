using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Data.Identity;
using Web.Cars.Models;

namespace Web.Cars.Validators
{
    public class ValidatorRegisterViewModel : AbstractValidator<RegisterVievModel>
    {
        private readonly UserManager<AppUser> _userManager;
        public ValidatorRegisterViewModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Поле пошта є обов'язковим!")
               .EmailAddress().WithMessage("Пошта є не коректною!")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Email).Must(BeUniqueEmail).WithName("Email").WithMessage("Дана пошта уже зареєстрована!");
               });
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Поле пароль є обов'язковим!")
                .MinimumLength(8).WithMessage("Поле пароль має містити міннімум 8 символів!");

            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Поле логін є обов'язковим!")
                .MinimumLength(6).WithMessage("Поле логін має містити міннімум 6 символів!");

            RuleFor(x => x.Photo)
                .NotEmpty().WithMessage("Поле фото є обов'язковим!");

            //.Matches("[A-Z]").WithName("Password").WithMessage("Password must contain one or more capital letters.")
            //.Matches("[a-z]").WithName("Password").WithMessage("Password must contain one or more lowercase letters.")
            //.Matches(@"\d").WithName("Password").WithMessage("Password must contain one or more digits.")
            //.Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithName("Password").WithMessage("Password must contain one or more special characters.")
            //.Matches("^[^£# “”]*$").WithName("Password").WithMessage("Password must not contain the following characters £ # “” or spaces.");
            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Поле є обов'язковим!")
                .Equal(x => x.Password).WithMessage("Поролі не співпадають!");
        }
        private bool BeUniqueEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result == null;
        }
    }
}
