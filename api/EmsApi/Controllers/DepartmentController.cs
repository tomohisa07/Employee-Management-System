using EmsApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace EmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<List<Department>>> Get()
        {
            List<Department> department = new List<Department>();

            string query = @"
                            select DepartmentId, DepartmentName from
                            dbo.Department
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
                    await using(SqlCommand myCommand = new SqlCommand(query, myCon))
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
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Department temp = new Department();
                    dataRow = dt.Rows[i];

                    temp.DepartmentId = (int)dataRow["DepartmentId"];
                    temp.DepartmentName = (string)dataRow["DepartmentName"];
                    department.Add(temp);
                }
            }

            return Ok(department);
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult> Post(Department dep)
        {
            string query = @"
                           insert into dbo.Department
                           values (@DepartmentName)
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
                        myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Put(Department dep)
        {
            string query = @"
                           update dbo.Department
                           set DepartmentName= @DepartmentName
                            where DepartmentId=@DepartmentId
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
                        myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                        myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            string query = @"
                           delete from dbo.Department
                            where DepartmentId=@DepartmentId
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
                        myCommand.Parameters.AddWithValue("@DepartmentId", id);

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
            }

            return Ok();
        }
    }
}
