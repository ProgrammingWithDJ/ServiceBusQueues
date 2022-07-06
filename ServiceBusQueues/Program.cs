using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var final = builder.Configuration.GetSection("ConnectionDomain");
builder.Services.AddAzureClients(builder =>
{
    

    builder.AddClient<ServiceBusClient, ServiceBusClientOptions>((_, _, _) =>
    {
        return new ServiceBusClient(final.Value, new DefaultAzureCredential());
    });
   //builder.AddServiceBusClient(connectionString);  // Removed this since it is not needed for managed Identitu
});


var app = builder.Build();

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
