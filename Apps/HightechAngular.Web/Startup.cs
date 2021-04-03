using HightechAngular.Admin;
using HightechAngular.Data;
using HightechAngular.Identity.Entities;
using HightechAngular.Identity.Services;
using HightechAngular.Orders;
using HightechAngular.Shop;
using HightechAngular.Web.Filters;
using Infrastructure.Extensions;
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
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HightechAngular.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
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

        private void ConfigureWeb(IServiceCollection services)
        {
            services.AddRazorPages();
            services
                .AddOpenGenericTypeDefinition(typeof(IDropdownProvider<>))
                .AddControllersWithViews(options =>
                {
                    if (!_env.IsDevelopment())
                    {
                        options.Filters.Add(typeof(ExceptionsFilterAttribute));
                    }
                })
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .AddModulesWithDbContext<ApplicationDbContext>(
                    CoreRegistrations.RegisterCore,
                    ShopRegistrations.RegisterShop,
                    AdminRegistrations.RegisterAdmin);

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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
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
                    "default",
                    "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                options.DocExpansion(DocExpansion.None);
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer("start");
                }
            });
        }
    }
}