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
    public class VacancyModel
    {
        public int Id { get; set; }

        [Required]
        public string VacancyDescription { get; set; } = null!;


        [Required]
        public string VacancyTitle { get; set; } 

        [Required]
        public int EmployerId { get; set; }
        [Required]
        public int MaximumNumberForApplicants { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsPost { get; set; }

        public bool IsActive { get; set; }
        public bool IsExpired { get; set; }

        public  ICollection<ApplicantVacancy> ApplicantVacancies { get; set; } = new List<ApplicantVacancy>();

        public  UserModel Employer { get; set; }
    }
}
