using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Timer_Rubik.WebApp.Authorize.Admin.Interfaces;
using Timer_Rubik.WebApp.Authorize.Admin.Services;
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

builder.Services.AddScoped<IAccountService_Admin, AccountService_Admin>();
builder.Services.AddScoped<ICategoryService_Admin, CategoryService_Admin>();
builder.Services.AddScoped<IScrambleService_Admin, ScrambleService_Admin>();
builder.Services.AddScoped<ISolveService_Admin, SolveService_Admin>();
builder.Services.AddScoped<IFavoriteService_Admin, FavoriteService_Admin>();
builder.Services.AddScoped<IRuleService_Admin, RuleService_Admin>();

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
