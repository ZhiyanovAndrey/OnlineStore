using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// ����������� ���� � ������� � PSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 

// ����������� ��
builder.Services.AddDbContext<OnlineStoreContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));


}

);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
