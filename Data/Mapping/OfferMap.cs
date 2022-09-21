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
    public class OfferMap:ClassMapping<Offer>
    {
        public OfferMap()
        {
            Table("offer");
            Id(offer => offer.Id, map => map.Generator(Generators.Identity));
            Property(offer => offer.UserId);
            Property(offer => offer.ProductId);
            Property(offer => offer.IsApproved);
            Property(offer => offer.OfferedPrice);
            
        }

    }
}
