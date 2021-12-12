using IesSchool.Core;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RealEstate.Context;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddPersistenceServices(connString);
builder.Services.AddApplicationServices();
builder.Services.ServiceInjection();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Iespolice",
                      builder =>
                      {
                          builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                      });
});
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(options =>
    {
        options.DocExpansion(DocExpansion.None);
    }); ;
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors("Iespolice");

app.UseAuthorization();

app.MapControllers();

app.Run();
