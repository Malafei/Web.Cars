using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Cars.Data.Identity
{
    public class AppUser : IdentityUser<long>
    {
        [StringLength(255)]
        public string Photo { get; set; }
        [StringLength(255)]
        public string Login { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
