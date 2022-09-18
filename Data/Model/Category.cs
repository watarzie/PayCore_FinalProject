using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual List<Product> Product { get; set; }
    }
}
