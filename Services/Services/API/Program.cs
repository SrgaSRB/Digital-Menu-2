using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Aplication.Exceptions;
using Services.Aplication.Interfaces.Repositories;
using Services.Aplication.Interfaces.Security;
using Services.Aplication.Interfaces.Services;
using Services.Aplication.Services;
using Services.Infrastructure.Data;
using Services.Infrastructure.Repositories;
using Services.Infrastructure.Security;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Hellang.Middleware.ProblemDetails;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubcategoryService, SubcategoryService>();
builder.Services.AddScoped<ILocalRepository, LocalRepository>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISubcaegoryProductRepository, SubcategoryProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) //need to check is jwtkey nullOrEmpty
      };
  });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdminOnly", policy =>
        policy.RequireClaim("IsGlobalAdmin", "true"));
});

builder.Services.AddProblemDetails(options =>
{
    options.Map<ValidationException>(ex =>
        new StatusCodeProblemDetails(400) { Title = ex.Message });

    options.Map<Services.Aplication.Exceptions.UnauthorizedAccessException>(ex =>
        new StatusCodeProblemDetails(401) { Title = ex.Message });

    options.Map<NotFoundException>(ex =>
        new StatusCodeProblemDetails(404) { Title = ex.Message });

    options.Map<ConflictException>(ex =>
        new StatusCodeProblemDetails(409) { Title = ex.Message });

    options.Map<Exception>(ex =>
        new StatusCodeProblemDetails(500) { Title = ex.Message });

});

var app = builder.Build();

app.UseProblemDetails();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
