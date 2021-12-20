using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Product.Models;
using Product.Data;
using FileManager.Models;
namespace Product.Data
{
     public class DataContext: DbContext, IDataContext
     {
          
          public DataContext(DbContextOptions<DataContext> options): base(options)
          {
               
          }
          public DbSet<MyProduct> Products { get; set; }
          public DbSet<AttachmentFile> attachmentFiles { get; set; }
     }
}