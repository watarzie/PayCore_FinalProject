using AutoMapper;
using Base.Response;
using Data.Repository;
using NHibernate;
using Service.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base.Concrete
{
    public abstract class BaseService<Dto, Entity> : IBaseService<Dto, Entity> where Entity : class
    {
        private readonly ISession session;
        private readonly IMapper mapper;
        private readonly IHibernateRepository<Entity> hibernateRepository;


        public BaseService(ISession session, IMapper mapper) : base()
        {
            this.session = session;
            this.mapper = mapper;

            hibernateRepository = new HibernateRepository<Entity>(session);
        }
        public virtual BaseResponse<IEnumerable<Dto>> GetAll()
        {
            var tempEntity = hibernateRepository.Entities.ToList();
            var result = mapper.Map<IEnumerable<Entity>, IEnumerable<Dto>>(tempEntity);
            return new BaseResponse<IEnumerable<Dto>>(result);
        }

        public virtual BaseResponse<Dto> GetById(int id)
        {
            var tempEntity = hibernateRepository.GetById(id);
            var result = mapper.Map<Entity, Dto>(tempEntity);
            return new BaseResponse<Dto>(result);
        }

        public virtual BaseResponse<Dto> Insert(Dto insertResource)
        {
            try
            {
                var tempEntity = mapper.Map<Dto, Entity>(insertResource);

                hibernateRepository.BeginTransaction();
                hibernateRepository.Save(tempEntity);
                hibernateRepository.Commit();

                hibernateRepository.CloseTransaction();
                return new BaseResponse<Dto>(mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {
                hibernateRepository.Rollback();
                hibernateRepository.CloseTransaction();
                return new BaseResponse<Dto>(ex.Message);
            }

        }

        public virtual BaseResponse<Dto> Remove(int id)
        {
            try
            {
                var tempEntity = hibernateRepository.GetById(id);
                if (tempEntity is null)
                {
                    return new BaseResponse<Dto>("Record Not Found");
                }

                hibernateRepository.BeginTransaction();
                hibernateRepository.Delete(id);
                hibernateRepository.Commit();
                hibernateRepository.CloseTransaction();

                return new BaseResponse<Dto>(mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {
                hibernateRepository.Rollback();
                hibernateRepository.CloseTransaction();
                return new BaseResponse<Dto>(ex.Message);
            }
        }

        public virtual BaseResponse<Dto> Update(int id, Dto updateResource)
        {
            try
            {
                var tempEntity = hibernateRepository.GetById(id);
                if (tempEntity is null)
                {
                    return new BaseResponse<Dto>("Record Not Found");
                }


                var entity = mapper.Map<Dto, Entity>(updateResource, tempEntity);

                hibernateRepository.BeginTransaction();
                hibernateRepository.Update(entity);
                hibernateRepository.Commit();
                hibernateRepository.CloseTransaction();

                var resource = mapper.Map<Entity, Dto>(entity);
                return new BaseResponse<Dto>(resource);
            }
            catch (Exception ex)
            {
                hibernateRepository.Rollback();
                hibernateRepository.CloseTransaction();
                return new BaseResponse<Dto>(ex.Message);
            }
        }
        





    }
}
