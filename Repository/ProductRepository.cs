using System;
using System.Threading.Tasks;
using Product.Models;
using Product.Data;
using Microsoft.EntityFrameworkCore;

namespace Product.Repository
{
     public class ProductRepository : IProductRepository
     {
          private readonly IDataContext _context;


          public ProductRepository(IDataContext context)
          {
               _context = context;
          }
          public async Task Add(MyProduct product)
          {
               _context.Products.Add(product);
               await _context.SaveChangesAsync();

               // throw new NotImplementedException();
          }

          public async Task Delete(int id)
          {

               var itemToDelete = await _context.Products.FindAsync(id);
               if (itemToDelete == null)
                    throw new NullReferenceException();
               _context.Products.Remove(itemToDelete);
               await _context.SaveChangesAsync();
          }

          public async Task<MyProduct> Get(int id)
          {
               return await _context.Products.FindAsync(id);
          }

          public async Task<IEnumerable<MyProduct>> GetAll()
          {
               return await _context.Products.ToListAsync();
          }

          public async Task Update(MyProduct product)
          {
               var itemToUpdate = await _context.Products.FindAsync(product.ProductId);
               if
               (itemToUpdate == null)
                    throw new NullReferenceException();
               itemToUpdate.Name = product.Name;
               itemToUpdate.Price = product.Price;
               await _context.SaveChangesAsync();

          }

     }
}