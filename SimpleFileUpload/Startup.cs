using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleFileUpload.AppLayer;
using SimpleFileUpload.DataAccess;
using System;

namespace SimpleFileUpload
{
	public class Startup
	{
		private IConfiguration Configuration;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddScoped<UserAppLayer>();
			services.AddScoped<UserElasticSearch>(GetUserElasticSearch);
			services.AddScoped<UserFileOperations>();
		}

		private UserElasticSearch GetUserElasticSearch(IServiceProvider arg)
		{
			return new UserElasticSearch(Configuration.GetValue<string>("ElasticSearchBaseAddress"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//app.UseFileServer(new FileServerOptions()
			//{
			//	FileProvider = new PhysicalFileProvider(
			//	Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
			//	RequestPath = new PathString("/node_modules"),
			//	EnableDirectoryBrowsing = true
			//});

			app.UseStaticFiles();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
