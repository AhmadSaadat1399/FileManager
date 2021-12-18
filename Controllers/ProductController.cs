using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.Repository;
using Product.Models;
using Product.Dtos;
using System.Data;
using System.Data.SqlTypes;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Npgsql;
//using Product.Models;

namespace Product.Controllers
{
     [Route("[controller]")]
     [ApiController]

     public class ProductController : ControllerBase
     {
          private readonly ILogger<ProductController> _logger;
          private readonly IProductRepository _ProductRepository;
          public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
          {
               _ProductRepository = productRepository;
               _logger = logger;

               // if (!Directory.Exists(folderPath))
               // {
               //      Directory.CreateDirectory(folderPath);
               // }

          }

          [HttpPost("Single_File")]
          public async Task Upload(IFormFile file)
          {
               using var conn = new NpgsqlConnection();
               using var cmd = new NpgsqlCommand();

               string sQL = "insert into picturetable (id, photo) VALUES(65, @Image)";
               using (var command = new NpgsqlCommand(sQL, conn))
               {
                    NpgsqlParameter param = command.CreateParameter();
                    param.ParameterName = "@Image";
                    param.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bytea;
                    // param.Value = ImgByteA;
                    command.Parameters.Add(param);

                    conn.Open();
                    command.ExecuteNonQuery();
                          _logger.LogInformation($"validating the file {file.FileName}");
                    _logger.LogInformation("saving file");
                    await Task.Delay(2000); // validate file type/format/size, scan virus, save it to a storage
                    _logger.LogInformation("file saved.");

               }
          }


          [HttpGet("{id}")]
          public async Task<ActionResult<MyProduct>> GetProduct(int id)
          {
               var product = await _ProductRepository.Get(id);
               if (product == null)
                    return NotFound();
               return Ok(product);

          }

          [HttpGet]
          public async Task<ActionResult<IEnumerable<MyProduct>>> GetProducts()
          {
               var product = await _ProductRepository.GetAll();
               return Ok(product);
          }

          [HttpPost]
          public async Task<ActionResult> CraeteProduct(CreateProductDto createProductDto)
          {
               MyProduct product = new()
               {
                    Name = createProductDto.Name,
                    Price = createProductDto.Price,
                    DataCreated = DateTime.UtcNow,
                    
               };
               await _ProductRepository.Add(product);
               return Ok();
          }

          [HttpDelete("{id}")]
          public async Task<ActionResult> DeleteProduct(int id)
          {
               await _ProductRepository.Delete(id);
               return Ok();
          }


          [HttpPut("{id}")]
          public async Task<ActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
          {
               MyProduct product = new()
               {
                    Name = updateProductDto.Name,
                    Price = updateProductDto.Price
               };
               await _ProductRepository.Update(product);
               return Ok();
          }



     }}