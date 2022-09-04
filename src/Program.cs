using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Repository;
using DapperASPNetCore.Services;
using Microsoft.AspNetCore;
using Microsoft.OpenApi.Models;
using WebApplication.Extensions;
var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// https://www.section.io/engineering-education/building-a-crud-api-dapper-aspnet-core/
// https://www.dominikzoechbauer.com/blog/2021-02-20/combining-and-cookie-jwt-bearer-authentication/
// https://stackoverflow.com/questions/57487107/how-to-implement-session-cookie-based-authentication-in-vue-js
// https://github.com/stripe/stripe-dotnet/tree/master/src/Stripe.net/Services

// Add services to the container.

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFileUploadRepository, FileUploadRepository>();
builder.Services.AddScoped<IFileUploadService, FileUploadService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();
builder.Services.AddScoped<IWishListService, WishListService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    //.WithMethods("PUT", "DELETE", "GET");
                });


    //options.AddPolicy(name: "MyPolicy",
    //            policy =>
    //            {
    //                policy.WithOrigins("https://localhost:8080/")
    //                .AllowAnyHeader()
    //                .AllowAnyMethod();
    //                   //.WithMethods("PUT", "DELETE", "GET");
    //            });
});

builder.Services.AddControllers();

builder.Services.AddJWTTokenServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();

CreateHostBuilder(args).Build().Run();

IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<DapperASPNetCore.Startup>();
                });



