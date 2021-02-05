using System.Text.Json.Serialization;
using HightechAngular.Admin;
using HightechAngular.Admin.Features.OrderManagement;
using HightechAngular.Data;
using HightechAngular.Identity.Entities;
using HightechAngular.Identity.Services;
using HightechAngular.Orders;
using HightechAngular.Shop;
using HightechAngular.Shop.Features.Catalog;
using HightechAngular.Web.Features.Admin;
using HightechAngular.Web.Features.Catalog;
using HightechAngular.Web.Filters;
using Infrastructure;
using Infrastructure.Extensions;
//using Infrastructure.Extensions;
using Infrastructure.SwaggerSchema.Dropdowns.Providers;
using Infrastructure.SwaggerSchema.TypeProvider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HightechAngular.Web
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
            ConfigureDbContext(services);
            ConfigureIdentityAndAuthentication(services);
            ConfigureWeb(services);
            ConfigureInfrastructure(services);
        }

        private static void ConfigureInfrastructure(IServiceCollection services)
        {
            services.AddSingleton<ITypeProvider>(new DefaultTypeProvider(x => x.StartsWith("HightechAngular")));
            services.AddScoped<IDropdownProvider, DefaultDropdownProvider>();
            services.AddScoped<DbContext, ApplicationDbContext>();
            services.AddDistributedMemoryCache();
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(ApplicationContextFactory.SetOptions);
            services.AddAsyncInitializer<ApplicationDbContextInitializer>();
        }

        private static void ConfigureWeb(IServiceCollection services)
        {
            services.AddRazorPages();
            services
                .AddControllersWithViews(options => options.Filters.Add(typeof(ExceptionsFilterAttribute)))
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); })
                .AddModulesWithDbContext<ApplicationDbContext>(
                    typeof(CatalogController).Assembly,
                    typeof(OrderController).Assembly);

            services.RegisterShop();
            services.RegisterAdmin();
            services.RegisterOrder();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.RegisterSwagger();
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddCors();
        }

        private static void ConfigureIdentityAndAuthentication(IServiceCollection services)
        {
            services
                .AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddIdentityServer()
                .AddApiAuthorization<User, ApplicationDbContext>();

            services
                .AddAuthentication()
                .AddIdentityServerJwt();
            
            services.AddScoped<IUserContext, UserContext>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            
            var fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(fordwardedHeaderOptions);

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            
            app.UseSession();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v1/swagger.json", "API");
                options.DocExpansion(DocExpansion.None);
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}