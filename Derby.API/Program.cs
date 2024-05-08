using Derby.API.Config;
using Derby.API.Services;
using Derby.Domain.Models;
using Derby.Domain.Profiles;
using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDerebitService, DerebitService>();
builder.Services.AddSingleton<ITradeService, TradeService>();
builder.Services.AddSingleton<IInstrumentService, InstrumentService>();
builder.Services.AddSingleton<SeedService>();
builder.Services.AddAutoMapper(typeof(InstrumentProfile));
builder.Services.AddTransient<ICronService, CronService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DerebitDb")
    );

var dbConnection = builder.Configuration.GetSection("DerebitDb");
var mongoClientName = dbConnection.GetSection("ConnectionString").Value;
var mongoDbName = dbConnection.GetSection("DatabaseName").Value;
builder.Services.AddHangfire(configuration => configuration
.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseMongoStorage(mongoClientName, mongoDbName, new MongoStorageOptions
{
    CheckQueuedJobsStrategy = CheckQueuedJobsStrategy.TailNotificationsCollection,
    MigrationOptions = new MongoMigrationOptions
    {
        MigrationStrategy = new MigrateMongoMigrationStrategy(),
        BackupStrategy = new CollectionMongoBackupStrategy()
    },
    Prefix = "hangfire.mongo",
    CheckConnection = true
})
);

builder.Services.AddHangfireServer(serverOptions =>
{
    serverOptions.ServerName = "Hangfire.Mongo server 1";
    serverOptions.Queues = new[] { "trade_queue", "test_queue", "default" };
});


var app = builder.Build();
using ( var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard()
    .UseHangfireServer();

HangfireJobConfiguration.ConfigureJobs(app);

app.Run();

