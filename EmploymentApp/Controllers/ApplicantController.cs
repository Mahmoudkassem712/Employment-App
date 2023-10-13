
using Business.Models;
using EmploymentApp.Application.Business;
using EmploymentApp.Application.Business.EnumsAndConst;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace EmploymentApp.Controllers
{
    [Route("api/Applicant")]
    public class ApplicantController : Controller
    {
        protected readonly VacancyService _VacancyService;
        protected readonly UserService _userService;
        private readonly ILogger _logger;

        public ApplicantController(ILoggerFactory logger, IConfiguration configuration)
        {
            _logger = logger.CreateLogger("ApplicantController");
            _VacancyService = new VacancyService(logger, configuration.GetConnectionString("DefaultConnection"));
            _userService = new UserService(configuration.GetConnectionString("DefaultConnection"));
        }
        [Route("GetApplicants")]
        public IActionResult ApplicantIndex()
        {
            var data = _userService.GetAllUsersByType(UserTypeEnum.Applicant);
            return Json(data);
        }

        [HttpGet]
        [Route("Search-Vacancy")]
        public IActionResult SearchForVacancies([FromQuery]string Term)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.SearchForVacancy(Term);
                return Json(data);
            }
            return Json(ModelState);
        }

        [HttpPost]
        [Route("ApplyVacancy")]
        public IActionResult ApplyVacancy([FromBody] VacancyApplicantsModel Model)
        {
            if (ModelState.IsValid)
            {
                var data = _VacancyService.ApplyApplicantOnVacancy(Model);
                return Json(data);
            }
            return Json(ModelState);
        }
    }
}
