using EmergencyManagementSystem.Service.Interfaces;
using EmergencyManagementSystem.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmergencyManagementSystem.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IUserRest, UserRest>();
            services.AddScoped<IEmployeeRest, EmployeeRest>();
            services.AddScoped<IVehicleRest, VehicleRest>();
            services.AddScoped<IAddressRest, AddressRest>();
            services.AddScoped<IMemberRest, MemberRest>();
            services.AddScoped<IRequesterService, RequesterRest>();
            services.AddScoped<IEmergencyRest, EmergencyRest>();
            services.AddScoped<IEmergencyHistoryRest, EmergencyHistoryRest>();
            services.AddScoped<IMedicalEvaluationRest, MedicalEvaluationRest>();
            services.AddScoped<UserService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IEmergencyRequiredVehicleRest, EmergencyRequiredVehicleRest>();
            services.AddScoped<IMedicalDecisionHistoryRest, MedicalDecisionHistoryRest>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => options.LoginPath = "/Login/Index");

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
