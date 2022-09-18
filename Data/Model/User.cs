using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime LastActivity { get; set; }
    }
}
