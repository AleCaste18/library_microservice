using customersService.Data;
using customersService.Data.Interfaces;
using customersService.GraphQL;
using customersService.Repository;
using customersService.Repository.Interfaces;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args); //Iniezione di dipendenze (servizi) - Middleware


var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment}.json", optional: true)
               .Build();

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                })
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddLogging(x => { x.ClearProviders(); x.AddSerilog(dispose: true); });


// Add services to the container  ->  codice per configurare o registrare i servizi (WebApplication.Services)

builder.Services.AddScoped<ICustomersContext, CustomersContext>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.
    AddGraphQLServer().
    AddQueryType<Query>().
    AddType<CustomerType>();


var app = builder.Build();

// Configure the HTTP request pipeline 


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL("/api/graphql");
});

app.UseSerilogRequestLogging();

app.Run();
