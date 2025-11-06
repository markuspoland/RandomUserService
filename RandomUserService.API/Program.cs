using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RandomUserService.API.Configuration;
using RandomUserService.Infrastructure.Configuration.Providers;
using RandomUserService.Infrastructure.Configuration.Providers.Interfaces;
using RandomUserService.Infrastructure.Extensions;
using RandomUserService.Infrastructure.Persistence;
using RandomUserService.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHandlers();

builder.Services.Configure<RandomUserServiceConfiguration>(builder.Configuration.GetSection(nameof(RandomUserServiceConfiguration)));

builder.Services.AddScoped<ISchedulerConfigurationProvider>(sp =>
{
    var options = sp.GetRequiredService<IOptions<RandomUserServiceConfiguration>>();
    var config = options.Value;
    return new SchedulerConfigurationProvider(config.InfrastructureConfiguration);
});

var app = builder.Build();

app.MigrateDatabase<RandomUserServiceDbContext>();

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
