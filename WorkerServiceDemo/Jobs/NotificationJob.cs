using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServiceDemo.Jobs
{
    internal class NotificationJob : IJob
    {
        private readonly ILogger _logger;
        public NotificationJob(ILogger<NotificationJob> logger)
        {
            _logger = logger;
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Notify User at {DateTime.Now} and JobType : {context.JobDetail.JobType}");
            return Task.CompletedTask;  
        }
    }
}
