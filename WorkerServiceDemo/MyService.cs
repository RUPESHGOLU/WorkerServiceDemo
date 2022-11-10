using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkerServiceDemo.Models;

namespace WorkerServiceDemo
{
    internal class MyService : IHostedService
    {
        private Timer? _timer = null;
        private readonly MongoDBConfig _config;
        public MyService(MongoDBConfig mongoDBConfig)
        {
            _config = mongoDBConfig;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CopyCollection, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async void CopyCollection(object? state)
        {
            try
            {
                var client = new MongoClient(_config.ConnectionString);
                var filter = Builders<UserDetail>.Filter.Lte(PurgeOldDataConstants.CreatedTimeStamp, DateTime.UtcNow.AddDays(-90));
                var db = client.GetDatabase(_config.ClientDatabase);
                var liveCollection = db.GetCollection<UserDetail>(_config.LiveCollection);
                var filteredQuery = await liveCollection.FindAsync(filter).ConfigureAwait(false);
                var result = filteredQuery.ToList();
                if (result.Count() > 0)
                {
                    var historyCollection = db.GetCollection<HistUserDetail>(_config.HistoryCollection);
                    historyCollection.InsertMany(result.Select(s => new HistUserDetail { Name = s.Name, City = s.City, CreatedTimeStamp = s.CreatedTimeStamp }));
                    await liveCollection.DeleteManyAsync(filter);

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        Console.WriteLine($"My Service is running at {DateTime.Now}");
        //        await Task.Delay(2000, stoppingToken);
        //    }
        //}
    }
}
