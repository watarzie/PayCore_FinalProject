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

namespace PayCore_Final.ServiceProduct
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Product> hibernateRepository;
       


        public ProductService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Product>(session);
            

        }

        public IEnumerable<Product> GetAllByCategoryId(int id)
        {
            return hibernateRepository.Find(x => x.CategoryId==id);
        }
        public Product GetProduct(int id)
        {
            return hibernateRepository.GetById(id);
        }
        


    }
}
