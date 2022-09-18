using Data.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ProductMap:ClassMapping<Product>
    {
        public ProductMap()
        {
            Table("product");
            Id(product => product.Id, map => map.Generator(Generators.Identity));
            Property(product => product.ProductName);
            Property(product => product.Price);
            Property(product => product.IsSold);
            Property(product => product.IsOfferable);
            Property(product => product.Description);
            Property(product => product.Color);
            Property(product => product.Brand);
            ManyToOne(employee => employee.Category, map => map.Column("CategoryId"));

        }
    }
}
