using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class AccountDetail
    {
        public virtual int Id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
