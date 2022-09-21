using Base.Response;
using Data.Model;
using Dto;
using Service.Base.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCore_Final.ServiceProduct
{
    public interface IProductService: IBaseService<ProductDto, Product>
    {
        IEnumerable<Product>  GetAllByCategoryId(int id);
        Product GetProduct(int id);
    


    }
}
