using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ConnectionManager
    {
        private EmploymentSystemDbContext _instance;
        private string _ConnectionString { get; set; }

        public ConnectionManager(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public  EmploymentSystemDbContext dbContext
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmploymentSystemDbContext(_ConnectionString);
                }

                return _instance;
            }
        }



    }
}
