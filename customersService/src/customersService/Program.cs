using customersService.Data;
using customersService.Data.Interfaces;
using customersService.GraphQL;
using customersService.Repository;
using customersService.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args); //Iniezione di dipendenze (servizi) - Middleware

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

app.Run();
