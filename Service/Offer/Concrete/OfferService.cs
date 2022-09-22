using AutoMapper;
using Base.Response;
using Data.Model;
using Data.Repository;
using Dto;
using NHibernate;
using Service.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore_Final.ServiceOffer
{
    public class OfferService : BaseService<OfferDto, Offer>, IOfferService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Offer> hibernateRepository;


        public OfferService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Offer>(session);

        }
        public IEnumerable<Offer> GetAllByOffers(int id)
        {
            return hibernateRepository.Find(x => x.UserId == id);
        }
        public IEnumerable<Offer> GetAllByOffersProduct(int id)
        {
            return hibernateRepository.Find(x => x.ProductId == id);
        }
        public Offer GetOffer(int id)
        {
            return hibernateRepository.GetById(id);
        }
       


    }
}
