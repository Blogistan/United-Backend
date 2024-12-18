var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.UnitedAPI>("unitedapi");

builder.Build().Run();
