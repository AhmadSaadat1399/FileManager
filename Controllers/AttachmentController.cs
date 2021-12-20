using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace FileManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly ILogger<AttachmentController> _logger;
        public AttachmentController(ILogger<AttachmentController> logger)
        {
            _logger = logger;

        }

        [HttpPost("Single_File")]
        public async Task Upload(IFormFile file, string FileName, int id)
        {

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=s@123456;");
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand("INSERT INTO attachmentFiles (FileName, DateCreation,MyPathFile)VALUES (@FileName,@DateCreation,@MyPathFile)", con);
            com.Connection = con;
            com.Parameters.AddWithValue("@FileName", NpgsqlTypes.NpgsqlDbType.Text).Value = FileName;
            com.Parameters.AddWithValue("@DateCreation", NpgsqlTypes.NpgsqlDbType.Timestamp);
            com.Parameters.AddWithValue("@MyPathFile", NpgsqlTypes.NpgsqlDbType.Bytea).Value = file;
            com.ExecuteNonQuery();
            con.Close();
            await Task.Delay(10);
            // _logger.LogInformation($"validating the file {file.FileName}");
            // _logger.LogInformation("saving file");
            // await Task.Delay(2000); // validate file type/format/size, scan virus, save it to a storage
            // _logger.LogInformation("file saved.");


        }

    }
}