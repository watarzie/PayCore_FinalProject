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
    public class CategoryMap:ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("category");
            Id(category => category.Id, map => map.Generator(Generators.Identity));
            Property(category => category.CategoryName);
            Bag(product => product.Product,map => map.Key(k => k.Column("CategoryId")), rel => rel.OneToMany());
            
        }
     
    }
}
