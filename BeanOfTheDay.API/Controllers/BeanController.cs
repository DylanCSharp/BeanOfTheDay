using BeanOfTheDay.Library.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace BeanOfTheDay.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BeanController(ILogger<BeanController> logger, IConfiguration configuration) : ControllerBase
    {
        private readonly string? _connectionString = configuration.GetConnectionString("BeanOfTheDayDB");

        //Getting all the beans
        [HttpGet("GetBeans")]
        public async Task<IEnumerable<Bean>> GetBeansAsync()
        {
            try
            {
                const string query = "[BOTD].[GetBeans]";

                await using var conn = new SqlConnection(_connectionString);

                return await conn.QueryAsync<Bean>(query, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return [];
            }
        }
        
        //Getting bean of the day
        [HttpGet("GetBeanOfTheDay")]
        public async Task<Bean> GetBeanOfTheDayAsync()
        {
            try
            {
                const string query = "[BOTD].[GetBeanOfTheDay]";

                await using var conn = new SqlConnection(_connectionString);

                return await conn.QueryFirstAsync<Bean>(query, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return new Bean();
            }
        }
        
        //Getting specific bean by id
        [HttpGet("GetBeansById/{id:int}")]
        public async Task<IEnumerable<Bean>> GetBeanByIdAsync(int id)
        {
            try
            {
                const string query = "[BOTD].[GetBeanById]";

                await using var conn = new SqlConnection(_connectionString);
                
                DynamicParameters parameters = new();
                parameters.Add("@Id", id);

                return await conn.QueryAsync<Bean>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return [];
            }
        }
        
        //Getting specific bean by search
        //I've already filtered item(s) on the front-end, this is how I would do it via API
        [HttpGet("GetBeansBySearch")]
        public async Task<IEnumerable<Bean>> GetBeanBySearchAsync(string searchTerm)
        {
            try
            {
                const string query = "[BOTD].[BeanSearch]";

                await using var conn = new SqlConnection(_connectionString);
                
                DynamicParameters parameters = new();
                parameters.Add("@SearchTerm", searchTerm);

                return await conn.QueryAsync<Bean>(query, parameters, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return [];
            }
        }

        //Deleting a bean
        [HttpDelete("DeleteBean")]
        public async Task<ActionResult> DeleteBean(string id)
        {
            try
            {
                const string query = "[BOTD].[DeleteBean]";

                await using var conn = new SqlConnection(_connectionString);

                DynamicParameters parameters = new();
                parameters.Add("@Id", id);

                await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return BadRequest("Failed to delete bean");
            }
        }

        //Both add and edit
        [HttpPost("MergeBean")]
        public async Task<ActionResult> MergeBean(Bean bean)
        {
            try
            {
                const string query = "[BOTD].[MergeBean]";
                
                await using var conn = new SqlConnection(_connectionString);
                
                DynamicParameters parameters = new();
                parameters.Add("@Id", bean.Id);
                parameters.Add("@Index", bean.Index);
                parameters.Add("@Name", bean.Name);
                parameters.Add("@Description", bean.Description);
                parameters.Add("@IsBOTD", bean.IsBOTD);
                parameters.Add("@Colour", bean.Colour);
                parameters.Add("@Country", bean.Country);
                parameters.Add("@Image", bean.Image);
                parameters.Add("@Cost", bean.Cost);

                await conn.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return BadRequest("Failed to merge bean");
            }
        }

        [HttpPost("ImportBeans")]
        public async Task<IActionResult> ImportBeans(IFormFile? file)
        {
            try
            {
                if (file == null)
                    return BadRequest("No file received");

                await using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                var jsonData = await reader.ReadToEndAsync();

                // Deserialize JSON data into a list of objects
                var beans = JsonSerializer.Deserialize<List<Bean>>(jsonData);

                if (beans == null || beans.Count == 0)
                    return BadRequest("Invalid or empty JSON file.");

                const string query = "[BOTD].[ImportBeans]";

                DataTable dt = new();
                dt.Columns.Add("Id", typeof(string));
                dt.Columns.Add("Index", typeof(string));
                dt.Columns.Add("IsBOTD", typeof(string));
                dt.Columns.Add("Cost", typeof(string));
                dt.Columns.Add("Image", typeof(string));
                dt.Columns.Add("Colour", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Country", typeof(string));

                foreach (var item in beans)
                {
                    dt.Rows.Add(item.Id, item.Index, item.IsBOTD, item.Cost, item.Image, item.Colour, item.Name, item.Description, item.Country);
                }

                await using var conn = new SqlConnection(_connectionString);

                var result = await conn.ExecuteScalarAsync<string>(query, new { Input = dt }, commandType: CommandType.StoredProcedure);
                if (result is null)
                    return BadRequest("Failed");

                return Ok(new { Message = "Data merged successfully!" });
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
