using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Models.Dto
{
    public class UserLoginDto
    {
        /// <summary>
        /// The user's email registered in the system
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's password registered with the email in the system
        /// </summary>
        public string Password { get; set; }
    }
}
