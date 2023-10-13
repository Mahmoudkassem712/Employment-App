using EmploymentApp.Application.Business;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentApp.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        protected readonly UserService _userService;

        public UserController( IConfiguration configuration)
        {
            _userService = new UserService(configuration.GetConnectionString("DefaultConnection"));
        }
      
    }
}
