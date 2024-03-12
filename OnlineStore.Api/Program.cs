using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Models.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// добавляем swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// отображение даты и времени в PSQL
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 

// регистрация БД
builder.Services.AddDbContext<OnlineStoreContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));


}

);

var app = builder.Build();

// добавим midleware for swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
