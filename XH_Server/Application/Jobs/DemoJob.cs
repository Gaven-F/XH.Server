using Furion.Schedule;

namespace XH_Server.Applications.Jobs;

[Cron("* * * * * ?", Furion.TimeCrontab.CronStringFormat.WithSeconds)]
public class DemoJob : IJob
{
    private readonly ILogger _logger;

    public DemoJob(ILogger<DemoJob> logger) => _logger = logger;

    public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        _logger.LogInformation("Demo Job");
        return Task.CompletedTask;
    }
}
