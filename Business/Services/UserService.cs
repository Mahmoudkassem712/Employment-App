using DataLayer;
using DataLayer.Data;
using DataLayer.Repositories;
using EmploymentApp.Application.Business.EnumsAndConst;
using EmploymentApp.Models;
using System.Collections.Generic;

namespace EmploymentApp.Application.Business
{
    public class UserService 
    {
        private readonly UserRepositroy _UserRepositroy;
        private readonly EmploymentSystemDbContext _dbcontext;
        private readonly ConnectionManager connectionManager;

        public UserService(string ConnectionString) {
            connectionManager = new ConnectionManager(ConnectionString);
            _dbcontext = connectionManager.dbContext;

            _UserRepositroy = new UserRepositroy(_dbcontext);
        }

        public List<UserModel> GetAllUsersByType(UserTypeEnum userType)
        {
            var users = new List<UserModel>();
            var result = _UserRepositroy.GetAllUsersByType((int)userType);
            foreach (var user in result) {
                users.Add(MapEntityToModel(user));
                    }
            return users;
        }
        public UserModel GetUser(int id)
        {
            var user = new UserModel();
            var result = _UserRepositroy.Get(id);
            user =  MapEntityToModel(result);
            
            return user;
        }


        private Users MapModelToEntity(UserModel userModel)
        {
            return new Users()
            {
                Email = userModel.Email,
                Password = userModel.Password,
                UserTypeId = userModel.UserTypeId,
                FullName = userModel.FullName,
            };
        }

        private UserModel MapEntityToModel(Users user)
        {
            return new UserModel()
            {
                Email = user.Email,
                Password = user.Password,
                UserTypeId = user.UserTypeId,
                FullName = user.FullName,
            };
        }


    }
}
