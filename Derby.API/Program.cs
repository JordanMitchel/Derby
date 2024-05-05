using Derby.API.Services;
using Derby.Domain.Models;
using Derby.Domain.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDerebitService, DerebitService>();
builder.Services.AddSingleton<ITradeService, TradeService>();
builder.Services.AddSingleton<IInstrumentService, InstrumentService>();
builder.Services.AddSingleton<SeedService>();
builder.Services.AddAutoMapper(typeof(InstrumentProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DerebitDb")
    );

var app = builder.Build();
using ( var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedService.Seed(services);
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

app.Run();

