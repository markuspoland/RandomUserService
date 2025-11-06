using Microsoft.Extensions.Hosting;

namespace RandomUserService.Infrastructure.Schedulers.Interfaces
{
    public interface IRandomUserServiceScheduler 
    {
        bool IsRunning { get; }
        void Pause();
        void Resume();
        string GetStatus();
    }
}
