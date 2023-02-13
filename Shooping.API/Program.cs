using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.API.Helpers;
using Shopping.BLL.Interfaces;
using Shopping.BLL.Repository_Classes;
using Shopping.Map;
using Shopping.API.Authorization;
using Shopping.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AccountProfile());
    cfg.AddProfile(new CategoryProfile());
    cfg.AddProfile(new ContactProfile());
    cfg.AddProfile(new ProductProfile());
    cfg.AddProfile(new CountryProfile());
    cfg.AddProfile(new CardProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddDbContext<Shopping.DAL.ShoppingDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
