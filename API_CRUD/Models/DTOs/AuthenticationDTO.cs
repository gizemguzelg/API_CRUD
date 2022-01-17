using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Models.DTOs
{
    public class AuthenticationDTO
    {
        [Required(ErrorMessage = "Type into user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Type into password")]
        public string Password { get; set; }
    }
}
