using System;
using System.Collections.Generic;

namespace DataLayer.Data;

public partial class Vacancy
{
    public int Id { get; set; }

    public string VacancyDescription { get; set; } = null!;
    public string Title { get; set; } = null!;

    public int EmployerId { get; set; }

    public int MaximumNumberForApplicants { get; set; }

    public DateTime ExpiryDate { get; set; }

    public bool IsPost { get; set; }

    public bool IsActive { get; set; }
    public bool IsExpired { get; set; }

    public virtual ICollection<ApplicantVacancy> ApplicantVacancies { get; set; } = new List<ApplicantVacancy>();

    public virtual Users Employer { get; set; } 
}
