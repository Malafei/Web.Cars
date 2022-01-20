using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Models;

namespace Web.Cars.Abstract
{
    public interface IUserService
    {
        public Task<string> CreateUser(RegisterVievModel model)
        {
            throw new NotImplementedException();
        }
        public Task<string> DeleteUser(string email)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UserSaveViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
