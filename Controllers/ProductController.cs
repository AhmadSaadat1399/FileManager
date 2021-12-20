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





               [HttpPost("Upload_File")]
               public async Task Upload(IFormFile file)
               {

               NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=s@123456;");
               con.Open();
               DataSet dataSet = new DataSet();
               NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter("SELECT * FROM  attachmentFiles",con);
               dataAdapter.InsertCommand = new NpgsqlCommand("insert into attachmentFiles(FileName,DateCreation, MyPathFile) " +" values (:F, :D, :M)", con);
               dataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("F",NpgsqlTypes.NpgsqlDbType.Text));
               dataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("D",NpgsqlTypes.NpgsqlDbType.Timestamp));
               dataAdapter.InsertCommand.Parameters.Add(new NpgsqlParameter("M",NpgsqlTypes.NpgsqlDbType.Bytea));
               dataAdapter.Fill(dataSet);
               

               // NpgsqlCommand com = new NpgsqlCommand("INSERT INTO public.'attachmentFiles'("Id", "FileName", "DateCreation", "MyPathFile") VALUES ('2', 'AHMAD', '','', ?)", con);
            //    IDbCommand command = con.CreateCommand();
            //    string sql = "";

            //    com.Connection = con;
            //    com.Parameters.AddWithValue("@FileName", NpgsqlTypes.NpgsqlDbType.Text).Value = file.Name;
            //    com.Parameters.AddWithValue("@DateCreation", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = file.
            //    com.Parameters.AddWithValue("@MyPathFile", NpgsqlTypes.NpgsqlDbType.Bytea).Value = file;
            //    com.ExecuteNonQuery();
            //    con.Close();
               await Task.Delay(10);

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



    }
}