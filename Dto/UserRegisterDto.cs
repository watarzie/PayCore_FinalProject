using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class UserRegisterDto
    {
        [Required]
        public virtual string UserName { get; set; }

        [Required]
        [EmailAddress]
        public virtual string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public virtual string Password { get; set; }
        [Required]
        public virtual string Name { get; set; }


        public virtual DateTime LastActivity { get; set; }
    }
}
