using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Authorize.Admin.Repository;
using Timer_Rubik.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connect Db
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 25))));

//Register Interface

builder.Services.AddScoped<IAccountRepository_Admin, AccountRepository_Admin>();
builder.Services.AddScoped<ICategoryRepository_Admin, CategoryRepository_Admin>();
builder.Services.AddScoped<IScrambleRepository_Admin, ScrambleRepository_Admin>();
builder.Services.AddScoped<ISolveRepository_Admin, SolveRepository_Admin>();

// Register auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Ignore Cycles
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
