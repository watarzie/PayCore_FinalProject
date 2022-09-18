using AutoMapper;
using Data.Model;
using Data.Repository;
using Dto;
using NHibernate;
using Service.Base.Concrete;
using Service.UserRegister.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserRegister.Concrete
{
    public class UserRegisterService:BaseService<UserRegisterDto,User>,IUserRegisterService
    {
        protected readonly ISession session;
        protected readonly IMapper mapper;
        protected readonly IHibernateRepository<User> hibernateRepository;

        public UserRegisterService(ISession session, IMapper mapper) : base(session, mapper)
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<User>(session);
        }
    }
}
