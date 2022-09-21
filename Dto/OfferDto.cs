using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class OfferDto
    {
        public virtual int UserId { get; set; }
        [Required]
        public virtual int ProductId { get; set; }
        //public virtual bool IsApproved { get; set; }
        
        public virtual int OfferedPrice { get; set; }
    }
}
