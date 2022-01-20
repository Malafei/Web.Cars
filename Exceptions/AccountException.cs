using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Cars.Exceptions;

namespace Web.Cars
{
    public class AccountException : Exception
    {
        public AccountException(AccountError accountError)
        {
            _accountError = accountError;
        }
        public AccountError _accountError { get; private set; }
    }
}
