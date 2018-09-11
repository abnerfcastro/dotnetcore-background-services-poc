using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Future.Api.Infrastructure
{
    public interface IBackgroundService : IHostedService
    {
        Task ExecuteAsync(CancellationToken stoppingToken);
    }
}