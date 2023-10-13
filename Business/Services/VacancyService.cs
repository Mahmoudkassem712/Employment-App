using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentApp.Models;
using Business.Models;
using Microsoft.Extensions.Logging;
using Business.EnumsAndConst;

namespace EmploymentApp.Application.Business
{
    public class VacancyService
    {
        private readonly VacancyRepository _VacancyRepositroy;
        private readonly EmploymentSystemDbContext _dbcontext;
        private readonly ILogger _logger;
        private readonly ConnectionManager connectionManager;
        private  string _connectionString;

        public VacancyService(ILoggerFactory logger , string ConnectionString)
        {
            _logger = logger.CreateLogger("VacancyService");
            connectionManager = new ConnectionManager(ConnectionString);
            _dbcontext = connectionManager.dbContext;
            _connectionString = ConnectionString;
            _VacancyRepositroy = new VacancyRepository(_dbcontext, logger);
        }

        public VacancyModel GetVacancy(int Id)
        {
            var result = new VacancyModel();
            try
            {
               var vacancy = _VacancyRepositroy.Get(Id);
               result = MapEntityToModel(vacancy);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Retrieve Vacancy Id {Id} : ", ex.Message);
            }
            return result;
        }

        public List<VacancyModel> SearchForVacancy(string Term)
        {
            var result = new List<VacancyModel>();
            try
            {
                var vacancies = _VacancyRepositroy.Search(Term);
                foreach(var entity in vacancies)
                {
                   result.Add(MapEntityToModel(entity));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Search for Vacancies : ", ex.Message);
            }
            return result;
        }

        public List<VacancyApplicantsModel> GetVacancyApplicants(int VacancyId)
        {
            var result = new List<VacancyApplicantsModel>();
            try
            {
                var vacancyApplicants = _VacancyRepositroy.GetAllVacancyApplicatants(VacancyId);
                foreach (var entity in vacancyApplicants)
                {
                    result.Add(MapApplicantVacancyEntityToModel(entity));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Retrieve Vacancy  : ", ex.Message);
            }
            return result;
        }



        public ValidationResult CreateVacancy(VacancyModel VacancyModel)
        {
            var result = new ValidationResult();
            try
            {
                result = ValidateVacancy(VacancyModel);
                if (result.IsValid)
                {
                    var vacancy = _VacancyRepositroy.Insert(MapModelToEntity(VacancyModel));
                    if (vacancy != null)
                    {
                        result.IsValid = true;
                        result.Status = StatusEnum.Success;
                        result.Message = "The Vacancy Created successfully.";
                        _logger.LogInformation($"The Vacancy Created For Employer Id {VacancyModel.EmployerId}  ");
                    }
                    else
                    {
                        result.IsValid = false;
                        result.Status = StatusEnum.Error;
                        result.Message = "Failed to Create Vacancy.";

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Create Vacancy For Employer Id {VacancyModel.EmployerId} : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Create Vacancy.";
            }
            return result;
        }
        public ValidationResult UpdateVacancy(VacancyModel VacancyModel)
        {
            var result = new ValidationResult();
            try
            {
                result = ValidateVacancy(VacancyModel);
                if (result.IsValid)
                {
                    var vacancy = _VacancyRepositroy.Update(MapModelToEntity(VacancyModel));
                    if (vacancy != null)
                    {
                        result.IsValid = true;
                        result.Status = StatusEnum.Success;
                        result.Message = "The Vacancy Updated successfully.";
                        _logger.LogInformation($"The Vacancy Updated For Employer Id {VacancyModel.EmployerId}  ");
                    }
                    else
                    {
                        result.IsValid = false;
                        result.Status = StatusEnum.Error;
                        result.Message = "Failed to Updated Vacancy.";

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Updated Vacancy For Employer Id {VacancyModel.EmployerId} : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Updated Vacancy.";
            }
            return result;
        }
        public ValidationResult DeleteVacancy(int VacancyId)
        {
            var result = new ValidationResult();
            try
            {
                result = ValidateDelete(VacancyId);
                if (result.IsValid)
                {
                    var Dbstatus = _VacancyRepositroy.Delete(VacancyId);
                    if (Dbstatus)
                    {
                        result.IsValid = true;
                        result.Status = StatusEnum.Success;
                        result.Message = "The Vacancy Deleted successfully.";
                        _logger.LogInformation($"The Vacancy Deleted For Id {VacancyId} ");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Delete Vacancy For Id {VacancyId} : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Delete Vacancy.";
            }
            return result;
        }

        public ValidationResult DeactivateVacancy(int VacancyId)
        {
            var result = new ValidationResult();
            try
            {
                result = ValidateDeactivate(VacancyId);
                if (result.IsValid)
                {
                    var Dbstatus = _VacancyRepositroy.Deactivate(VacancyId);
                    if (Dbstatus)
                    {
                        result.IsValid = true;
                        result.Status = StatusEnum.Success;
                        result.Message = "The Vacancy Deactivated successfully.";
                        _logger.LogInformation($"The Vacancy Deactivated For Id {VacancyId} ");
                    }
                    else
                    {
                        result.IsValid= false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Deactivated Vacancy For Id {VacancyId} : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Deactivated Vacancy.";
            }
            return result;
        }

        public ValidationResult PostVacancy(int VacancyId)
        {
            var result = new ValidationResult();
            try
            {
                result = ValidatePost(VacancyId);
                if (result.IsValid)
                {
                    var Dbstatus = _VacancyRepositroy.PostVacancy(VacancyId);
                    if (Dbstatus)
                    {
                        result.IsValid = true;
                        result.Status = StatusEnum.Success;
                        result.Message = "The Vacancy Posted successfully.";
                        _logger.LogInformation($"The Vacancy Posted For Id {VacancyId} ");
                    }
                    else
                    {
                        result.IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Posted Vacancy For Id {VacancyId} : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Posted Vacancy.";
            }
            return result;
        }

        public ValidationResult ApplyApplicantOnVacancy(VacancyApplicantsModel ApplicantsVacancyModel)
        {
            var result = new ValidationResult();
            try
            {
                ApplicantsVacancyModel.ApplyingDate = DateTime.Now;
                result = ValidateApplyingOnVacancy(ApplicantsVacancyModel);
                if (result.IsValid)
                {
                    var vacancy = _VacancyRepositroy.InsertApplicantVacancy(MapApplicantVacancyModelToEntity(ApplicantsVacancyModel));
                    if (vacancy != null)
                    {
                        result.IsValid = true;
                        result.Status = StatusEnum.Success;
                        result.Message = "The Applicant Applying on Vacancy successfully.";
                        _logger.LogInformation($"The Vacancy Applied by Applicant Id {ApplicantsVacancyModel.ApplicantId}  ");
                    }
                    else
                    {
                        result.IsValid = false;
                        result.Status = StatusEnum.Error;
                        result.Message = "Failed to Apply on Vacancy.";

                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Apply Vacancy For Applicant Id {ApplicantsVacancyModel.ApplicantId} : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Apply on Vacancy.";
            }
            return result;
        }


        private ValidationResult ValidateApplyingOnVacancy(VacancyApplicantsModel ApplicantsVacancyModel)
        {
            var result = new ValidationResult();

            if (_VacancyRepositroy.IsVacancyReachMaximumNumber(ApplicantsVacancyModel.VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't apply , The Vacancy is Reached the Maximum Number Of Applicants.");
            } 
            if (_VacancyRepositroy.IsApplicantHasApplyingVacancyToday(ApplicantsVacancyModel.ApplicantId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't apply , The Applicant is Reached the Maximum Number Of applying.");
            } 
            if (_VacancyRepositroy.IsAppliedBefore(ApplicantsVacancyModel.VacancyId , ApplicantsVacancyModel.ApplicantId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't apply , You applied before for this Vacancy");
            }
            return result;
        }
        private ValidationResult ValidateVacancy(VacancyModel VacancyModel)
        {
            var result = new ValidationResult();

            if (_VacancyRepositroy.VacancyTitleIsExist(VacancyModel.VacancyTitle, VacancyModel.EmployerId, VacancyModel.Id))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"The Title {VacancyModel.VacancyTitle} is Inserted before");
            }
            if(VacancyModel.MaximumNumberForApplicants <1)
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Please Insert a valid MaximumNumberForApplicants ");
            }
            if (VacancyModel.ExpiryDate < DateTime.Today.Date)
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Please Insert a valid ExpiryDate ");
            }
            return result;
        }

        private ValidationResult ValidateDelete(int VacancyId)
        {
            var result = new ValidationResult();

            if (_VacancyRepositroy.VacancyHasApplicants(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Delete Vacancy has applicants");
            }
            if (_VacancyRepositroy.IsVacancyActive(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Delete the Vacancy is Active ");
            }   
            if (_VacancyRepositroy.IsVacancyPosted(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Delete the Vacancy is Posted ");
            }

            return result;
        }

        private ValidationResult ValidateDeactivate(int VacancyId)
        {
            var result = new ValidationResult();

            if (!_VacancyRepositroy.IsVacancyExist(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Deactivate, the Vacancy Not Exist");
            }

            if (_VacancyRepositroy.VacancyHasApplicants(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Deactivate, the Vacancy has applicants");
            }
            if (_VacancyRepositroy.IsVacancyPosted(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Deactivate, the Vacancy is Posted ");
            }

            return result;
        }

        private ValidationResult ValidatePost(int VacancyId)
        {
            var result = new ValidationResult();

            if (!_VacancyRepositroy.IsVacancyExist(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Post, the Vacancy Not Exist");
            }

            if (!_VacancyRepositroy.VacancyHasApplicants(VacancyId))
            {
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Errors.Add($"Can't Post, the Vacancy doesnt have any applicants");
            }

            return result;
        }

        public async Task<ValidationResult> ArchiveVacancy()
        {
            var result = new ValidationResult();
            try
            {
                var vacancies = _VacancyRepositroy.GetAllNotExpiredVacancies();
                foreach(var entity in vacancies)
                {
                    if (CheckVacancyIsExpired(entity))
                        result.IsValid =  _VacancyRepositroy.Archive(entity.Id);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Archive Vacancies  : ", ex.Message);
                result.IsValid = false;
                result.Status = StatusEnum.Error;
                result.Message = "Failed to Archive Vacancy.";
            }
            return result;
        }

        private bool CheckVacancyIsExpired(Vacancy vacancy)
        {
            if(vacancy.ExpiryDate < DateTime.Now)
                return true;

            return false;
        }





        private Vacancy MapModelToEntity(VacancyModel VacancyModel)
        {
            return new Vacancy()
            {
                Id = VacancyModel.Id,
                Title = VacancyModel.VacancyTitle,
                ExpiryDate = VacancyModel.ExpiryDate,
                EmployerId = VacancyModel.EmployerId,
                VacancyDescription = VacancyModel.VacancyDescription,
                IsActive = VacancyModel.IsActive,
                IsPost = VacancyModel.IsPost,
                IsExpired = VacancyModel.IsExpired,
                MaximumNumberForApplicants = VacancyModel.MaximumNumberForApplicants,
            };
        }

        private ApplicantVacancy MapApplicantVacancyModelToEntity(VacancyApplicantsModel Model)
        {
            var entity =  new ApplicantVacancy()
            {
                Id = Model.Id,
                ApplicantId = Model.ApplicantId,
                VacancyId = Model.VacancyId,
                ApplyingDate = Model.ApplyingDate,
            };
            return entity;
        }

        private VacancyModel MapEntityToModel(Vacancy Vacancy)
        {
            return new VacancyModel()
            {
                Id = Vacancy.Id,
                ExpiryDate = Vacancy.ExpiryDate,
                VacancyTitle = Vacancy.Title,
                EmployerId = Vacancy.EmployerId,
                VacancyDescription = Vacancy.VacancyDescription,
                IsActive = Vacancy.IsActive,
                IsPost = Vacancy.IsPost,
                MaximumNumberForApplicants = Vacancy.MaximumNumberForApplicants,
                IsExpired = Vacancy.IsExpired,

            };
        }
        private VacancyApplicantsModel MapApplicantVacancyEntityToModel(ApplicantVacancy entity)
        {
            var model = new VacancyApplicantsModel()
            {
                Id = entity.Id,
                ApplicantId = entity.ApplicantId,
                VacancyId = entity.VacancyId,
                ApplyingDate = entity.ApplyingDate,
            };
            model.Applicant = new UserService(_connectionString).GetUser(entity.ApplicantId);
            model.Vacancy = GetVacancy(entity.VacancyId);
            return model;
        }



    }
}
