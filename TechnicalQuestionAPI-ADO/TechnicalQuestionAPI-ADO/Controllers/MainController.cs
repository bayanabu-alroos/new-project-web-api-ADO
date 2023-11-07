﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using TechnicalQuestionAPI_ADO.DTO;

namespace TechnicalQuestionAPI_ADO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MainController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("ALLSkills")]
        public IActionResult ALLSkills()
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            SqlCommand command = new SqlCommand("SELECT * FROM AllSkills", connection);
            //Execute Query Command
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);

        }
        [HttpGet]
        [Route("GetSKILLsInformation")]
        public IActionResult FetchAllSkillsInTheSystem()
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            SqlCommand command = new SqlCommand("SELECT * FROM [Skills]", connection);
            //Execute Query Command
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            return Ok(datatable);
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult InsertSkills([FromBody] SkillDTO dto)
        {
            //Execute NON Query Command (# rows effeted)
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = "INSERT INTO [dbo].[Skills]([Title],[Description],[Rate],[ISACTIVE])VALUES(@tit,@des,@rate,@isAct)";
            SqlCommand command = new SqlCommand(commandString, connection);
            command.Parameters.AddWithValue("@tit", dto.Title);
            command.Parameters.AddWithValue("@des", dto.Description);
            command.Parameters.AddWithValue("@rate", dto.Rate);
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
        [Route("CreateSkills")]
        public IActionResult InsertSkill([FromBody] SkillDTO dto)
        {
            //Call Procdure in ADO.NET
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string procName = "InsertSkillRecord";
            SqlCommand command = new SqlCommand(procName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@title", dto.Title);
            command.Parameters.AddWithValue("@description", dto.Description);
            command.Parameters.AddWithValue("@rate", dto.Rate);
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
        public IActionResult UpdateSkills([FromRoute] int Id, [FromBody] SkillDTO dto)
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = $"UPDATE SKILLS SET Title ='{dto.Title}' ,Description = '{dto.Description}',Rate = '{dto.Rate}' WHERE SKILLSID = {Id}";
            SqlCommand command = new SqlCommand(commandString, connection);
            connection.Open();
            int rows = command.ExecuteNonQuery();
            connection.Close();
            if (rows > 0)
                return Ok();
            else
                return BadRequest("Insert Operation has beem Failed");
        }

        [HttpDelete]
        [Route("[action]/{Id}")]
        public IActionResult DeleteSkills([FromRoute] int Id)
        {
            // fetch connection information with database 
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //setup sql command 
            string commandString = $"DELETE FROM SKILLS WHERE SKILLSID = {Id}";
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
