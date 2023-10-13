using DataLayer.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class UserRepositroy
    {
        private readonly EmploymentSystemDbContext _dbcontext;
       // private readonly ILogger _logger;

        public UserRepositroy(EmploymentSystemDbContext dbContext /*, ILoggerFactory logger*/)
        {
            _dbcontext = dbContext;
            //_logger = logger.CreateLogger("UserRepositroy");
        }


        public Users Get(int Id)
        {
            var user = _dbcontext.Users.FirstOrDefault(x => x.Id == Id);
            return user;
        }


        public List<Users> GetAllUsersByType(int UserTypeId)
        {
            var users = _dbcontext.Users.Where(x=>x.UserTypeId == UserTypeId).ToList();
            return users;
        }

        public Users Insert(Users user)
        {
            _dbcontext.Users.Add(user);
            _dbcontext.SaveChanges();
            return user;
        }

        public Users Update(Users user)
        {
            _dbcontext.Users.Update(user);
            _dbcontext.SaveChanges();
            return user;
        }
        public Users Delete(Users user)
        {
            _dbcontext.Users.Remove(user);
            _dbcontext.SaveChanges();
            return user;
        }
    }
}
