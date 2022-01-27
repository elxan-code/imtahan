using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace imtahan.VwModels
{
    public class VmRegister
    {

        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(100)]
        public string UsarName { get; set; }
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(100)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepaetPassword { get; set; }

    }
}
