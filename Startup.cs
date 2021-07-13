using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using back_end.Filters;
using back_end.Repositories;
using back_end.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace back_end
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"));
            });*/
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<ILocalFileStorage, LocalFileStorage>();
            services.AddHttpContextAccessor();
            services.AddDbContext<MoviesDbContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=MoviesLocalDB;Integrated Security=true;");
               // options.UseSqlServer(
                 //   "Server=(localdb)\\mssqllocaldb;Database=MoviesLocalDB;Trusted_Connection=True;MultipleActiveResultSets=true;AttachDBFilename=|DataDirectory|\\MoviesDB.mdf");
            });
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var frontendUrl = Configuration.GetValue<string>("frontend_url");
                    builder.WithOrigins("http://localhost:4200")
                        .WithExposedHeaders(new string[] {"totalAmountRecords"})
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            //services.AddResponseCaching();
            services.AddTransient<IRepository, RepositoryCache>();
            //services.AddTransient<MyFilterAction>();
            services.AddControllers();/*(options =>
            {
                options.Filters.Add(typeof(MyExceptionFilter));
            });*/
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "back_end", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "back_end v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseCors();

            //app.UseResponseCaching();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
