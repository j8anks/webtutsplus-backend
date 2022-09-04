using DapperASPNetCore.Context;
using DapperASPNetCore.Contracts;
using DapperASPNetCore.Repository;
using DapperASPNetCore.Services;
using Stripe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//https://simplecoding-ecommerce.netlify.app/

/// <summary>
///  None of this fires.
/// </summary>

namespace DapperASPNetCore
{
	public class Startup
	{
		
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;			

		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddSingleton<DapperContext>();
			services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<IAccountService, DapperASPNetCore.Services.AccountService>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();			
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<IFileUploadRepository, FileUploadRepository>();
			services.AddScoped<IFileUploadService, FileUploadService>();
			services.AddScoped<IOrderService, DapperASPNetCore.Services.OrderService>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IWishListRepository, WishListRepository>();
			services.AddScoped<IWishListService, WishListService>();

			//services.AddCors(options =>
			//{
			//	options.AddPolicy("MyAllowAllHeadersPolicy",
			//		policy =>
			//		{
			//			policy.WithOrigins("https://localhost:8080")
			//				   .WithMethods("PUT", "DELETE", "GET");
			//		});
			//});

			//services.AddCors(options =>
			//{
			//	options.AddPolicy("CorsApi",
			//		builder => builder.WithOrigins("https://localhost:8080")
			//			.AllowAnyHeader()
			//			.AllowAnyMethod());
			//});

			services.AddControllers();

			StripeConfiguration.ApiKey = System.Environment.GetEnvironmentVariable("tGN0bIwXnHdwOa85VABjPdSn8nWY7G7I");



			//services.AddCors(options =>
			//{
			//	// Define one or more CORS policies
			//	options.AddPolicy("AllowAll", builder =>
			//		builder.AllowAnyOrigin()
			//			.AllowAnyMethod()
			//			.AllowAnyHeader());
			//});

		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			env.EnvironmentName = Microsoft.Extensions.Hosting.Environments.Development;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStatusCodePages();

			app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			// app.UseCors("CorsApi");
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//app.UseRouting();
			//app.UseCors("CorsApi");
			//app.UseAuthorization();

			////app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

			//         app.UseEndpoints(endpoints =>
			//         {
			//             endpoints.MapControllers();
			//         });

		}

	}
}
