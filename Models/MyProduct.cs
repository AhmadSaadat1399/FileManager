using System;
using System.ComponentModel.DataAnnotations;

namespace Product.Models
{
     public class MyProduct
     {
          [Key]
          public int ProductId { get; set; }
          public string? Name { get; set; }
          public decimal Price { get; set; }
          public DateTime DataCreated { get; set; }
          public Byte[] AttachmentFile { get; set; }

     }
}