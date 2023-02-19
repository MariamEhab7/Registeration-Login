using BL;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DataBase
var ConnetionString = builder.Configuration.GetConnectionString("RegisterDB");
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(ConnetionString));
#endregion

#region Services
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddScoped<IPasspwordHasher, PasswordServices>();
builder.Services.AddScoped<IRegister, RegisterServices>();
builder.Services.AddScoped<IJwtInitializer, JWTServices>();
builder.Services.AddScoped<IClaimsInitializer, ClaimsServices>();

builder.Services.AddSingleton<HttpService>();
builder.Services.AddScoped<ICountry, CountryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});


#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Default";
    options.DefaultChallengeScheme = "Default";
})
    .AddJwtBearer("Default", options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = UserKey.CreateKey(),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
#endregion

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
