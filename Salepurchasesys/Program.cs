using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SalePurchasesys.Models;
using SalePurchasesys.Services;
using SalePurchasesys.Data;
using AutoMapper;
using SalePurchasesys.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ISaleService, SaleService>();

// ? Add AutoMapper BEFORE builder.Build()
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add Swagger for API documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SalePurchasesys API",
        Version = "3.0.1",
        Description = "API for managing sale and purchase orders"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalePurchasesys API v1");
        c.RoutePrefix = "swagger"; // Swagger loads at "/swagger"
    });
}

// CORS
app.UseCors("AllowAngularClient");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
