using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// ��������� swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// ����������� ���� � ������� � PSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 

// ����������� ��
builder.Services.AddDbContext<OnlineStoreContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));


}

);

var app = builder.Build();

// ������� midleware for swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); 
 // ����� ������������ ���������������� ��������� Swagger � �������� �������� ���������� (https://localhost:<port>/)
        options.RoutePrefix = string.Empty;
        //options.SerializeAsV2 = true;
    }); 
}



// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
