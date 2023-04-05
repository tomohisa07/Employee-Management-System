using EmsApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<Employee> employees = new List<Employee>();
            string query = @"
                            select EmployeeId, EmployeeName,Department,
                            convert(varchar(10),DateOfJoining,120) as DateOfJoining,PhotoFileName
                            from
                            dbo.Employee
                            ";

            DataTable dt = new DataTable();
            DataRow dataRow;

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            try
            {
                await using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    await using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        dt.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Exception Error.");
            };

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee temp = new Employee();
                    dataRow = dt.Rows[i];

                    temp.EmployeeId = (int)dataRow["EmployeeId"];
                    temp.EmployeeName = (string)dataRow["EmployeeName"];
                    temp.Department = (string)dataRow["Department"];
                    temp.DateOfJoining = (string)dataRow["DateOfJoining"];
                    temp.PhotoFileName = (string)dataRow["PhotoFileName"];
                    employees.Add(temp);
                }
            }

            return Ok(employees);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Employee emp)
        {
            string query = @"
                           insert into dbo.Employee
                           (EmployeeName,Department,DateOfJoining,PhotoFileName)
                    values (@EmployeeName,@Department,@DateOfJoining,@PhotoFileName)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            try
            {
                await using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    await using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                        myCommand.Parameters.AddWithValue("@Department", emp.Department);
                        myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                        myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Exception Error.");
            };

            return Ok();
        }


        [HttpPut]
        public async Task<ActionResult> Put(Employee emp)
        {
            string query = @"
                           update dbo.Employee
                           set EmployeeName= @EmployeeName,
                            Department=@Department,
                            DateOfJoining=@DateOfJoining,
                            PhotoFileName=@PhotoFileName
                            where EmployeeId=@EmployeeId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            try
            {
                await using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    await using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                        myCommand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                        myCommand.Parameters.AddWithValue("@Department", emp.Department);
                        myCommand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                        myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Exception Error.");
            };

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            string query = @"
                           delete from dbo.Employee
                            where EmployeeId=@EmployeeId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            try
            {
                await using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    await using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@EmployeeId", id);

                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Exception Error.");
            };

            return Ok();
        }


        [Route("SaveFile")]
        [HttpPost]
        public async Task<ActionResult> SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return BadRequest("Exception Error.");
            }

            return Ok();

        }
    }
}
