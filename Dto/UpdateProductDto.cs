using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class UpdateProductDto
    {
        
        [Required]
        public virtual bool IsSold { get; set; }
    }
}
