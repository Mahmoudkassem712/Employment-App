using DataLayer.Data;
using EmploymentApp.Application.Business;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmploymentApp.BackGroundService
{
    public class BackGroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackGroundWorkerService> _logger;
        protected readonly VacancyService _VacancyService;


        public BackGroundWorkerService(ILogger<BackGroundWorkerService> logger , ILoggerFactory loggerFactory , IConfiguration configuration)
        {
            _logger = logger;
            _VacancyService = new VacancyService(loggerFactory, configuration.GetConnectionString("DefaultConnection"));
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start Task of Archiving Vacancy");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Service Running at time : {DateTimeOffset.Now} ");

              await  _VacancyService.ArchiveVacancy();

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}