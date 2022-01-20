using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Cars.Exceptions
{
    public class AccountError
    {
        public AccountError() { }
        public AccountError(string message)
        {
            Errors.Invalid.Add(message);
        }
        public AccountErrorItem Errors { get; set; } = new AccountErrorItem();
    }

    public class AccountErrorItem
    {
        public List<string> Invalid { get; set; } = new List<string>();
    }

}
