using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imtahan.Models
{
    public class CustomUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
