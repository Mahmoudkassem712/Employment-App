using DataLayer.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class VacancyRepository
    {
        private readonly EmploymentSystemDbContext _dbcontext;
        private readonly ILogger _logger;

        public VacancyRepository(EmploymentSystemDbContext dbContext , ILoggerFactory logger)
        {
            _logger = logger.CreateLogger("VacancyRepository");
            _dbcontext = dbContext;
        }

       

        public Vacancy Get(int VacancyId)
        {
            var Vacancy = _dbcontext.Vacancies.FirstOrDefault(x => x.Id == VacancyId);
            return Vacancy;
        }
        public List<ApplicantVacancy> GetAllVacancyApplicatants(int VacancyId)
        {
            var ApplicantVacancies = _dbcontext.ApplicantVacancies.Where(x => x.VacancyId == VacancyId).ToList();
            return ApplicantVacancies;
        }
        public List<Vacancy> GetAllVacanciesByEmployer(int EmployerId)
        {
            var Vacancies = _dbcontext.Vacancies.Where(x => x.EmployerId == EmployerId).ToList();
            return Vacancies;
        }

        public List<Vacancy> GetAllActiveVacancies()
        {
            var Vacancies = _dbcontext.Vacancies.Where(x => !x.IsActive).ToList();
            return Vacancies;
        }
        public List<Vacancy> GetAllNotExpiredVacancies()
        {
            var Vacancies = _dbcontext.Vacancies.Where(x => !x.IsExpired).ToList();
            return Vacancies;
        }
        public Vacancy? Insert(Vacancy vacancy)
        {
            try
            {
              _dbcontext.Vacancies.Add(vacancy);
             _dbcontext.SaveChanges();
              return vacancy;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Saving Vacancy in DB For Employer Id {vacancy.EmployerId} : ", ex.Message);
                return null;
            }
        }

        public Vacancy Update(Vacancy vacancy)
        {
            try
            {
                _dbcontext.Vacancies.Update(vacancy);
                _dbcontext.SaveChanges();
                return vacancy;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Updating Vacancy in DB For Employer Id {vacancy.EmployerId} : ", ex.Message);
                return null;
            }
        }
        public bool Delete(int VacancyId)
        {
            try
            {
                var Vacancy = _dbcontext.Vacancies.FirstOrDefault(x => x.Id == VacancyId);
                _dbcontext.Vacancies.Remove(Vacancy);
                _dbcontext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Delete Vacancy From DB For Id {VacancyId} : ", ex.Message);
                return false;
            }
        }

        public bool Deactivate(int VacancyId)
        {
            try
            {
                var Vacancy = _dbcontext.Vacancies.FirstOrDefault(x => x.Id == VacancyId);
                Vacancy.IsActive = false;
                _dbcontext.Vacancies.Update(Vacancy);
                _dbcontext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Deactivate Vacancy From DB For Id {VacancyId} : ", ex.Message);
                return false;
            }
        }
        public bool Archive(int VacancyId)
        {
            try
            {
                var Vacancy = _dbcontext.Vacancies.FirstOrDefault(x => x.Id == VacancyId);
                Vacancy.IsExpired = true;
                _dbcontext.Vacancies.Update(Vacancy);
                _dbcontext.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Deactivate Vacancy From DB For Id {VacancyId} : ", ex.Message);
                return false;
            }
        }
        public bool PostVacancy(int VacancyId)
        {
            try
            {
                var Vacancy = _dbcontext.Vacancies.FirstOrDefault(x => x.Id == VacancyId);
                Vacancy.IsPost = true;
                _dbcontext.Vacancies.Update(Vacancy);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Post Vacancy From DB For Id {VacancyId} : ", ex.Message);
                return false;
            }
        }

        public bool VacancyTitleIsExist(string Title, int EmployerId, int VacancyId)
        {
            var IsExist = _dbcontext.Vacancies.Any(x => x.Title == Title && x.IsActive && !x.IsPost && !x.IsExpired && x.EmployerId == EmployerId && x.Id != VacancyId);
            return IsExist;
        }

        public bool IsVacancyReachMaximumNumber(int VacancyId)
        {
            var count = _dbcontext.ApplicantVacancies.Count(x => x.VacancyId == VacancyId);
            var entity = _dbcontext.Vacancies.FirstOrDefault(x => x.Id == VacancyId);
            if (entity.MaximumNumberForApplicants < count+1)  return true; 
            
            return false;
        }

        public bool IsAppliedBefore(int VacancyId ,  int ApplicantId)
        {
            return _dbcontext.ApplicantVacancies.Any(x => x.VacancyId == VacancyId && x.ApplicantId == ApplicantId);

        }
        public bool IsApplicantHasApplyingVacancyToday(int ApplicantId)
        {
            var LastApplying = _dbcontext.ApplicantVacancies.OrderByDescending(x=>x.ApplyingDate).FirstOrDefault(x => x.ApplicantId == ApplicantId);
           
            if(LastApplying !=null)
                if ((DateTime.UtcNow - LastApplying.ApplyingDate).TotalHours < 24 )
                     return true;

            return false;
        }

        public bool VacancyHasApplicants(int VacancyId)
        {
            var IsExist = _dbcontext.ApplicantVacancies.Any(x => x.VacancyId == VacancyId);
            return IsExist;
        }

        public bool IsVacancyActive(int VacancyId)
        {
            var IsExist = _dbcontext.Vacancies.Any(x => x.Id == VacancyId && x.IsActive);
            return IsExist;
        }

        public bool IsVacancyExist(int VacancyId)
        {
            var IsExist = _dbcontext.Vacancies.Any(x => x.Id == VacancyId);
            return IsExist;
        }


        public bool IsVacancyPosted(int VacancyId)
        {
            var IsExist = _dbcontext.Vacancies.Any(x => x.Id == VacancyId && x.IsPost);
            return IsExist;
        }

        public List<Vacancy> Search(string Term)
        {
            List<Vacancy> Vacancies = new List<Vacancy>();
            Vacancies = _dbcontext.Vacancies.Where(x => x.Title.Contains(Term) && x.IsActive && !x.IsPost && !x.IsExpired).ToList();
            return Vacancies;
        }

        public ApplicantVacancy? InsertApplicantVacancy(ApplicantVacancy entity)
        {
            try
            {
                _dbcontext.ApplicantVacancies.Add(entity);
                _dbcontext.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed During Saving Applicant Vacancy in DB For Applicant Id {entity.ApplicantId} : ", ex.Message);
                return null;
            }
        }
    }
}
