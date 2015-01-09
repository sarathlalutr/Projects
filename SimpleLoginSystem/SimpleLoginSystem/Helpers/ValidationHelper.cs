using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleLoginSystem.Helpers
{
    public sealed class ValidationHelper
    {
        public const string Email = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
    }
}