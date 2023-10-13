using System;
using System.Collections.Generic;

namespace DataLayer.Data;

public partial class ApplicantVacancy
{
    public int Id { get; set; }

    public int VacancyId { get; set; }

    public int ApplicantId { get; set; }

    public DateTime ApplyingDate { get; set; }

    public virtual Users Applicant { get; set; } 

    public virtual Vacancy Vacancy { get; set; } 
}
