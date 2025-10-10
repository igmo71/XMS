var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.XMS_ApiService>("apiservice")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.XMS_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.AddProject<Projects.WMS_Project>("wms-project");

builder.Build().Run();
