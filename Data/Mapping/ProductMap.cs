using Data.Model;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
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
            Property(product => product.UserId);
            Property(product => product.CategoryId);
            Property(product => product.ProductName);
            Property(product => product.Price);
            Property(product => product.IsSold);
            Property(product => product.IsOfferable);
            Property(product => product.Description);
            Property(product => product.Color);
            Property(product => product.Brand);
            Bag(product => product.Offer, map => map.Key(k => k.Column("ProductId")), rel => rel.OneToMany());

        }
    }
}
