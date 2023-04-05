using EmsApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EmsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public UserController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            string query = @"select ID, Email, Password from [User] where Email=@Email and Password=@Password";

            DataTable dt = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;

            try
            {
                await using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    await using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Email", user.Email);
                        myCommand.Parameters.AddWithValue("@Password", user.Password);
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

            if (dt.Rows.Count == 0)
            {
                return NotFound();
            }
            else
            {
                DataRow dataRow = dt.Rows[0];
                var claims = new[] { new Claim(ClaimTypes.Email, (string)dataRow["Email"]) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // 認証クッキーをレスポンスに追加
                await HttpContext.SignInAsync(principal);

                return Ok();
            }

        }
    }
}
