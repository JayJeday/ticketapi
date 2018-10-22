using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;


namespace TicketCenterAPI.Models
{
    public class AuthUser
    {

        [Key]
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string UserName { get; set; }
        [Required, MinLength(3), MaxLength(50)]
        public string Password { get; set; }
    }


}
