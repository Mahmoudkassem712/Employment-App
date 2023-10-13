using DataLayer.Data;
using EmploymentApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class VacancyApplicantsModel
    {
        public int Id { get; set; }

        [Required]
        public int VacancyId { get; set; }
        [Required]

        public int ApplicantId { get; set; }
        [Required]

        public DateTime ApplyingDate { get; set; }

        public virtual UserModel Applicant { get; set; }

        public virtual VacancyModel Vacancy { get; set; }
    }
}
