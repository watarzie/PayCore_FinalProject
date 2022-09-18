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
            Id(x => x.Id, x =>
            {
                x.Type(NHibernateUtil.Int32);
                x.Column("Id");
                x.UnsavedValue(0);
                x.Generator(Generators.Increment);
            });

            Property(b => b.Name, x =>
            {

                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.Email, x =>
            {
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.Password, x =>
            {
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });


            Property(b => b.UserName, x =>
            {
                x.Type(NHibernateUtil.String);
                x.NotNullable(true);
            });

            Property(b => b.LastActivity, x =>
            {
                x.Type(NHibernateUtil.DateTime);
                x.NotNullable(true);
            });

            Table("user");
        }
    }
}
