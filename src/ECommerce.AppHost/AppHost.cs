using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
            .AddPostgres("postgres")
            .WithImageTag("16.2")
            .WithPgAdmin(pgAdmin =>
            {
                pgAdmin.WithHostPort(5050);
            })
            .WithDataVolume("postgres-data");

var catalogDb = postgres.AddDatabase("catalogdb");

var rabbitMq = builder.AddRabbitMQ("messaging")
                      .WithImageTag("3.13-management")
                      .WithManagementPlugin()
                      .WithDataVolume("rabbitmq-data");

builder.AddProject<Catalog_API>("catalog-service")
    .WithReference(catalogDb)
    .WaitFor(catalogDb)
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq);

builder.Build().Run();
