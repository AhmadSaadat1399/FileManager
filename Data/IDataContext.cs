using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Product.Models;

namespace Product.Data
{
     public interface IDataContext
     {
          DbSet<MyProduct> Products { get; set; }
          Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

     }
}