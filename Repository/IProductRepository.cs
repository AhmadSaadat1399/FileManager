using System;
using System.Threading.Tasks;
using Product.Models;

namespace Product.Repository
{
     public interface IProductRepository
     {

          Task<MyProduct> Get(int id);
          Task<IEnumerable<MyProduct>> GetAll();
          Task Add(MyProduct product);
          Task Delete(int id);
          Task Update(MyProduct product);

     }
}