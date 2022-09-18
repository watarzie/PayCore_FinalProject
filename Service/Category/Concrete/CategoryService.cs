using AutoMapper;
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

namespace Paycore_Final.Service
{
    public class CategoryService : BaseService<CategoryDto, Category>, ICategoryService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<Category> hibernateRepository;

        public CategoryService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Category>(session);
        }
    }
}
