using WorkerServiceDemo;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddHostedService<Worker>();
        services.AddHostedService<MyService>();
    })
    .Build();

await host.RunAsync();
