using PoC.Orchestration.Orchestrator.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddWorkflow(cfg =>
{
    var redisHost = builder.Configuration.GetValue<string>("redis:server:host")!;

    cfg.UseRedisPersistence(redisHost, "orchestrator");
    cfg.UseRedisLocking(redisHost);
    cfg.UseRedisQueues(redisHost, "orchestrator");
    cfg.UseRedisEventHub(redisHost, "orchestration");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Services.RegisterWorkFlows();

app.Run();
