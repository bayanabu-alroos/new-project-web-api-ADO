using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TechnicalQuestionAPI_ADO.DTO;

namespace TechnicalQuestionAPI_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EductionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EductionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult ALLEduction_History()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand("SELECT * FROM Eduction_History", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult ALLEduction_HistoryView()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand command = new SqlCommand("SELECT * FROM EducationHistoryView", connection);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertEduction_History([FromBody] Eduction_HistoryDTO dto)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string commandString = "INSERT INTO Eduction_History ([Title], [Specification], [Start_Date], [End_Date], [Description], [Orginzation_Name], [USERID], [NationalityId], [IsActive]) VALUES(@tit,@sep,@start,@endD,@des,@org,@userid,@natid,@isAct)";
            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@tit", dto.Title);
            command.Parameters.AddWithValue("@des", dto.Description);
            command.Parameters.AddWithValue("@sep", dto.Specification);
            command.Parameters.AddWithValue("@start", dto.Start_Date);
            command.Parameters.AddWithValue("@endD", dto.End_Date);
            command.Parameters.AddWithValue("@org", dto.Orginzation_Name);
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
        public IActionResult InsertEduction_HistoryProcedure([FromBody] Eduction_HistoryDTO dto)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string procName = "InsertEductionHistory";
            SqlCommand command = new SqlCommand(procName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@title", dto.Title);
            command.Parameters.AddWithValue("@description", dto.Description);
            command.Parameters.AddWithValue("@Orginzation_Name", dto.Orginzation_Name);
            command.Parameters.AddWithValue("@Specification", dto.Specification);
            command.Parameters.AddWithValue("@End_Date", dto.End_Date);
            command.Parameters.AddWithValue("@Start_Date", dto.Start_Date);
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
        [HttpPut]
        [Route("[action]/{Id}")]
        public IActionResult UpdateEduction_History([FromRoute] int Id, [FromBody] Eduction_HistoryDTO dto)
        {
            // Fetch connection information with the database
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            // Setup SQL command
            string commandString = $"UPDATE Eduction_History SET Title = '{dto.Title}', Specification = '{dto.Specification}', " +
                $"Start_Date = '{dto.Start_Date}', End_Date = '{dto.End_Date}', Description = '{dto.Description}', " +
                $"Orginzation_Name = '{dto.Orginzation_Name}', USERID = '{dto.USERID}', NationalityId = '{dto.NationalityId}', " +
                $"IsActive = '{dto.IsActive}' " +
                $"WHERE Eduction_HistoryId = {Id}";

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
        public IActionResult DeleteEduction_History([FromRoute] int Id)
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = $"DELETE FROM Eduction_History WHERE Eduction_HistoryId = {Id}";
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
