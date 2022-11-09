using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorkerServiceDemo
{
    internal class MyService : IHostedService
    {
        private Timer _timer = null;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(CopyCollection, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async void CopyCollection(object? state)
        {
            var connectionString = "mongodb://localhost:27017"; 
            var client = new MongoClient(connectionString);
            var filter = Builders<BsonDocument>.Filter.Eq("prep_time", 10);
            var db = client.GetDatabase("cooker");
            var liveCollection = db.GetCollection<BsonDocument>("recipes"); 
            var filteredQuery = await liveCollection.FindAsync(filter).ConfigureAwait(false);
            var result = filteredQuery.ToList();
            var historyCollection = db.GetCollection<BsonDocument>("recipesOld");
            historyCollection.InsertMany(result);
            //var deleteFilter = Builders<BsonDocument>.Filter.In("_id", result.Select(i => i._id));
            await liveCollection.DeleteManyAsync(filter);
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
