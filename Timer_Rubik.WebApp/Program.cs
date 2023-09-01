using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Timer_Rubik.WebApp.Data;
using Timer_Rubik.WebApp.Interfaces.Repository;
using Timer_Rubik.WebApp.Interfaces.Services.Client;
using Timer_Rubik.WebApp.Interfaces.Utils;
using Timer_Rubik.WebApp.Middlewares;
using Timer_Rubik.WebApp.Services;
using Timer_Rubik.WebApp.Services.Client;
using Timer_Rubik.WebApp.Utilities;
using IAdmin = Timer_Rubik.WebApp.Interfaces.Services.Admin;
using SAdmin = Timer_Rubik.WebApp.Services.Admin;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connect Db
builder.Services.AddDbContext<DataContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 4, 25))));

//Register Interface
builder.Services.AddScoped<IEmailUtils, EmailUtils>();
builder.Services.AddScoped<IJWTUtils, JWTUtils>();
builder.Services.AddScoped<IPasswordUtils, PasswordUtils>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IcategoryService, CategoryService>();
builder.Services.AddScoped<IScrambleService, ScrambleService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

builder.Services.AddScoped<IAdmin.IAccountService, SAdmin.AccountService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IScrambleRepository, ScrambleRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IRuleRepository, RuleRepository>();

// Register auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Ignore Cycles
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("timer-rubik-secret-access-token"))
                };
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseHttpMethodOverride();

app.UseMiddleware<AdminTokenMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.Run();
