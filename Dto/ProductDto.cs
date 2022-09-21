using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class ProductDto
    {
        public virtual int UserId { get; set; }
        [Required]
        public virtual int CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public virtual string ProductName { get; set; }

        [Required]
        [MaxLength(500)]
        public virtual string Description { get; set; }

        [Required]
        public virtual string Color { get; set; }

        [Required]
        public virtual string Brand { get; set; }
        [Required]
        public virtual int Price { get; set; }
        public virtual bool IsOfferable { get; set; } 
        public virtual bool IsSold { get; set; } 


    }
}
