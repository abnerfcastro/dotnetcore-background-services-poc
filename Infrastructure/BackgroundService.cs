using System;
using System.Threading;
using System.Threading.Tasks;

namespace Future.Api.Infrastructure
{
    public abstract class BackgroundService : IBackgroundService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCancellationTokenSource = new CancellationTokenSource();

        public abstract Task ExecuteAsync(CancellationToken stoppingToken);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_stoppingCancellationTokenSource.Token);
            return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
                return;

            try
            {
                _stoppingCancellationTokenSource.Cancel();
            }
            finally
            {
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        public void Dispose()
        {
            _stoppingCancellationTokenSource?.Cancel();
        }
    }
}