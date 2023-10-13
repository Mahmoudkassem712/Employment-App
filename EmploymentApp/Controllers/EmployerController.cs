using Microsoft.AspNetCore.Mvc;
using EmploymentApp.Application.Business;
using EmploymentApp.Application.Business.EnumsAndConst;
using Business.Models;


namespace EmploymentApp.Controllers
{
    [Route("api/Employer")]
    public class EmployerController : Controller
    {
        protected readonly VacancyService _VacancyService;
        protected readonly UserService _userService;
        private readonly ILogger _logger;


        public EmployerController(ILoggerFactory logger , IConfiguration configuration)
        {
            _logger = logger.CreateLogger("EmployerController");
            _VacancyService = new VacancyService(logger , configuration.GetConnectionString("DefaultConnection"));
            _userService = new UserService(configuration.GetConnectionString("DefaultConnection"));
        }

        [Route("GetEmployers")]
        public IActionResult EmployerIndex()
        {
            var data = _userService.GetAllUsersByType(UserTypeEnum.Employer);
            return Json(data);
        }

        [HttpPost]
        [Route("AddVacancy")]
        public IActionResult AddVacancy([FromBody]VacancyModel vacancyModel)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.CreateVacancy(vacancyModel);
                return Json(data);
            }
            return Json(ModelState);
        }


        [HttpPost]
        [Route("UpdateVacancy")]
        public IActionResult UpdateVacancy([FromBody] VacancyModel vacancyModel)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.UpdateVacancy(vacancyModel);
                return Json(data);
            }
            return Json(ModelState);
        }
        [HttpDelete]
        [Route("DeleteVacancy/{Id}")]
        public IActionResult DeleteVacancy([FromRoute] int Id)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.DeleteVacancy(Id);
                return Json(data);
            }
            return Json(ModelState);
        }
        [HttpPatch]
        [Route("DeactivateVacancy/{Id}")]
        public IActionResult DeactivateVacancy([FromRoute] int Id)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.DeactivateVacancy(Id);
                return Json(data);
            }
            return Json(ModelState);
        }

        [HttpPatch]
        [Route("PostVacancy/{Id}")]
        public IActionResult PostVacancy([FromRoute] int Id)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.PostVacancy(Id);
                return Json(data);
            }
            return Json(ModelState);
        }
        
        [HttpGet]
        [Route("ShowApplicants/{Id}")]
        public IActionResult ShowVacancyApplicants([FromRoute] int Id)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.GetVacancyApplicants(Id);
                return Json(data);
            }
            return Json(ModelState);
        }
    }
}
