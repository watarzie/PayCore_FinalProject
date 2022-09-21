using AutoMapper;
using Data.Model;
using Data.Repository;
using Dto;
using NHibernate;
using Paycore_Final.UpdateProduct;
using Service.Base.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore_Final.UpdateProduct
{
    public class UpdateProductService: BaseService<UpdateProductDto, Product>, IUpdateProductService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Product> hibernateRepository;



        public UpdateProductService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Product>(session);


        }
    }
}
