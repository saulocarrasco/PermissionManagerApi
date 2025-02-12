

using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PermissionsManager.Application.Contracts;
using PermissionsManager.Infrastructure.ElasticSearch;
using PermissionsManager.Persistence;
using PermissionsManager.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

const string CORS_CONFIG_NAME = "_myCors";

builder.Services.AddCors(opts =>
{
    opts.AddPolicy(CORS_CONFIG_NAME, p =>
    {
        p.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(PermissionsManager.Application.Permissions.Queries.List)));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(PermissionsManager.Application.Permissions.Queries.List));
builder.Services.AddFluentValidationAutoValidation();

var settings = new ElasticsearchClientSettings(new Uri(builder.Configuration["ElasticSearch:Host"]!))
    .CertificateFingerprint(builder.Configuration["ElasticSearch:CertificateFingerprint"]!)
    .Authentication(new ApiKey(builder.Configuration["ElasticSearch:ApiKey"]!));
builder.Services.AddSingleton(new ElasticsearchClient(settings));

builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
builder.Services.AddTransient<IElasticSearchService, ElasticSearchService>();

builder.Services.AddControllers();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
        Seed.SeedData(context).Wait();
    }
    catch (Exception e)
    {
        Console.WriteLine("There was an error during migrations");
        Console.WriteLine(e.ToString());
        return;
    }
}

app.UseCors(CORS_CONFIG_NAME);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();