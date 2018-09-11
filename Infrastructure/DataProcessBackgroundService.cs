using Future.Api.Models;
using Future.Api.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Future.Api.Infrastructure
{
    public class DataProcessBackgroundService : BackgroundService
    {
        private readonly IRepository _repository;

        public DataProcessBackgroundService(IRepository repository)
        {
            _repository = repository;
        }

        public override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var pendingRequests = (await _repository.GetAll()).Where(s => s.Status == Status.Completed);

                foreach (var pendingRequest in pendingRequests)
                {
                    pendingRequest.Data = pendingRequest.Data.ToLower();
                }

                await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
            }
        }
    }
}