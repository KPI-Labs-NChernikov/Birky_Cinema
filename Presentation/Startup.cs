using Business;
using Business.Interfaces;
using Business.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;

namespace Presentation
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
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IScenarioWriterService, ScenarioWriterService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<CustomLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Seed data
            using var serviceScope = app.ApplicationServices.CreateScope();
            var seeder = new DataSeeder()
            {
                RoleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>(),
            };
            seeder.SeedRoles().Wait();
        }
    }
}
