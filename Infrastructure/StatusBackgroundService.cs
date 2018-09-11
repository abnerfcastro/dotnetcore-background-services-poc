using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Future.Api.Models;
using Future.Api.Repository;

namespace Future.Api.Infrastructure
{
    public class StatusBackgroundService : BackgroundService
    {
        private readonly IRepository _repository;

        public StatusBackgroundService(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var pendingRequests = (await _repository.GetAll()).Where(s => s.Status == Status.Pending);

                foreach (var pendingRequest in pendingRequests)
                {
                    await _repository.SetStatusAsync(pendingRequest.Id, Status.Completed);
                    await Task.Delay(1000, stoppingToken);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                Console.WriteLine("Status Background Service executed.");
            }
        }
    }
}