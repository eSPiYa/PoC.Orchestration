using PoC.Orchestration.Api.Hubs;
using PoC.Orchestration.Api.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR()
                .AddStackExchangeRedis("redis", options =>
                {
                    options.Configuration.ChannelPrefix = RedisChannel.Literal("signalr");
                });

builder.Services.AddTransient<WebApiService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapHub<ShowsHub>("/hubs/showshub");

app.Run();
