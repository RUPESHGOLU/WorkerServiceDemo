using System.Configuration;
using WorkerServiceDemo;
using WorkerServiceDemo.Models;
 
var configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddJsonFile("appsettings.json")
        .Build();
var mongoConfig = new MongoDBConfig();
configuration.GetSection("MongoDBConfig").Bind(mongoConfig);
var builder = Host.CreateDefaultBuilder(args);
IHost host = builder
    .ConfigureAppConfiguration(builder =>
    {
        builder.Sources.Clear();
        builder.AddConfiguration(configuration);
        
    }).ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        services.AddHostedService<MyService>();
        services.AddSingleton(mongoConfig);
    })
    .Build();

await host.RunAsync();
