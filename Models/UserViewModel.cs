using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Cars.Models
{
    public class UserItemViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

    }

    public class UserEditViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

    }


    public class UserSaveViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }

    }
}
