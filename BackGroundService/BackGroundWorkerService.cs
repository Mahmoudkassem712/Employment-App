using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackGroundService
{
    public class BackGroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackGroundWorkerService> _logger;

        public BackGroundWorkerService(ILogger<BackGroundWorkerService> logger)
        {
            _logger = logger;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Start Task of Archiving Vacancy");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Service Running at time : {DateTimeOffset.Now} ");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}