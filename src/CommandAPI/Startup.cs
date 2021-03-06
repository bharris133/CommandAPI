using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient;
using AutoMapper;

namespace CommandAPI
{
	public class Startup
	{
		//In order to supply our connection string from appsettings.json to our
		//DbContext class, we have to update our Startup class to provide a “Configuration” object
		//we use this configuration object to access the connection string.
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddAutoMapper(typeof(Startup));
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommandAPI", Version = "v1" });
			});

			/* var builder = new SqlConnectionStringBuilder();
				builder.ConnectionString = Configuration.GetConnectionString("DefaultSqlConnection");
				builder.UserID = Configuration["User ID"];
				builder.Password = Configuration["Password"]; */

			// services.AddScoped<ICommandAPIRepo, MockCommandAPIRepo>();
			services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
			services.AddDbContext<CommandContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));

			services.AddCors(options =>
			{
				options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin()
						 .AllowAnyMethod()
						 .AllowAnyHeader());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommandAPI v1"));
			}

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseCors("AllowOrigin");

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
