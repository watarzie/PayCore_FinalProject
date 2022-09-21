using Data.Model;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("user");
            Id(user => user.Id, map => map.Generator(Generators.Identity));
            Property(user => user.Email);
            Property(user => user.Password);
            Property(user => user.UserName);
            Property(user => user.Name);
            Property(user => user.LastActivity);
            Bag(user => user.Offer, map => map.Key(k => k.Column("UserId")), rel => rel.OneToMany());
            Bag(user => user.Product, map => map.Key(k => k.Column("UserId")), rel => rel.OneToMany());
        }
    }
}
