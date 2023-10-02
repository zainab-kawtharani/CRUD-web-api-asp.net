using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{   //localhosst/portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult getStudents()
        {
            string[] students =new string[]{ "zeinab", "abdallah", "fatima", "ahmad" };
            return Ok(students);
        }
    }
}
