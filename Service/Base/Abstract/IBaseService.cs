using Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base.Abstract
{
    public interface IBaseService<Dto,Entity>
    {
        BaseResponse<Dto> GetById(int id);
        BaseResponse<IEnumerable<Dto>> GetAll();
        BaseResponse<Dto> Insert(Dto insertResource);
        BaseResponse<Dto> Update(int id, Dto updateResource);
        BaseResponse<Dto> Remove(int id);
    }
}
