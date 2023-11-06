using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TechnicalQuestionAPI_ADO.DTO;

namespace TechnicalQuestionAPI_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpierincesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ExpierincesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult ALLExpierinces()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand("SELECT * FROM Expierinces", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertExpierinces([FromBody] ExpierincesDTO dto)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string commandString = "INSERT INTO Expierinces ([Title],[StartDate],[Enddate],[Description],[CompanyName],[USERID],[NationalityId],[ISActive]) VALUES(@tit,@start,@endD,@des,@comp,@userid,@natid,@isAct)";
            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@tit", dto.Title);
            command.Parameters.AddWithValue("@des", dto.Description);
            command.Parameters.AddWithValue("@comp", dto.CompanyName);
            command.Parameters.AddWithValue("@start", dto.StartDate);
            command.Parameters.AddWithValue("@endD", dto.Enddate);
            command.Parameters.AddWithValue("@userid", dto.USERID);
            command.Parameters.AddWithValue("@natid", dto.NationalityId);
            command.Parameters.AddWithValue("@isAct", dto.IsActive);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has been Failed");
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertExpierinceProcedure([FromBody] ExpierincesDTO dto)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string procName = "InsertExpierince";
            SqlCommand command = new SqlCommand(procName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Title", dto.Title);
            command.Parameters.AddWithValue("@description", dto.Description);
            command.Parameters.AddWithValue("@StartDate", dto.StartDate);
            command.Parameters.AddWithValue("@Enddate", dto.Enddate);
            command.Parameters.AddWithValue("@CompanyName", dto.CompanyName);
            command.Parameters.AddWithValue("@USERID", dto.USERID);
            command.Parameters.AddWithValue("@NationalityId", dto.NationalityId);
            command.Parameters.AddWithValue("@IsActive", dto.IsActive);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has been Failed");
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult ALLExpierincesView()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand("SELECT * FROM ExpierincesView", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);
        }
        [HttpPut]
        [Route("[action]/{Id}")]
        public IActionResult UpdateExpierinces([FromRoute] int Id, [FromBody] ExpierincesDTO dto)
        {
            // fetch connection information with the database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            // Setup SQL command
            string commandString = $"UPDATE Expierinces SET Title = '{dto.Title}', CompanyName = '{dto.CompanyName}', " +
                $"StartDate = '{dto.StartDate}', Enddate = '{dto.Enddate}', Description = '{dto.Description}', " +
                $"USERID = '{dto.USERID}', NationalityId = '{dto.NationalityId}', IsActive = '{dto.IsActive}' " +
                $"WHERE ExpierincesID = {Id}";

            SqlCommand command = new SqlCommand(commandString, connection);
            connection.Open();

            int rows = command.ExecuteNonQuery();

            connection.Close();

            if (rows > 0)
                return Ok();
            else
                return BadRequest("Update operation has been Failed");
        }
        [HttpDelete]
        [Route("[action]/{Id}")]
        public IActionResult DeleteExpierince([FromRoute] int Id)
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = $"DELETE FROM Expierinces WHERE ExpierincesID = {Id}";
            SqlCommand command = new SqlCommand(commandString, connection);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has been Failed");
        }
    }
}
