using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure.Configuration;
using Infrastructure.Repository.Repositories;
using Infrastructure.Repository.Generics;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceProduct;
using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Domain.Interfaces.InterfaceCompraUsuario;
using Infra.Repository.Repositories;
using Domain.Interfaces.InterfaceCompra;
using Application.Interfaces;
using Application.OpenApp;
using Domain.Interfaces.InterfaceLogSistema;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UI.Token;
using System;
using System.Threading.Tasks;

namespace Web_ECommerce
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
            services.AddDbContext<ContextBase>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContextBase>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            // INTERFACE E REPOSITORIO
            services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
            services.AddSingleton<IProduct, RepositoryProduct>();
            services.AddSingleton<ICompraUsuario, RepositoryCompraUsuario>();
            services.AddSingleton<ICompra, RepositoryCompra>();
            services.AddSingleton<ILogSistema, RepositoryLogSistema>();


            // INTERFACE APLICA��O
            services.AddSingleton<InterfaceProductApp, AppProduct>();
            services.AddSingleton<InterfaceCompraUsuarioApp, AppCompraUsuario>();
            services.AddSingleton<InterfaceCompraApp, AppCompra>();
            services.AddSingleton<InterfaceLogSistemaApp, AppLogSistema>();

            // SERVI�O DOMINIO
            services.AddSingleton<IServiceProduct, ServiceProduct>();
            services.AddSingleton<IServiceCompraUsuario, ServiceCompraUsuario>();


            //JSONWEBTOKEN
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "MrReacher.Security.Bearer",
                    ValidAudience = "MrReacher.Security.Bearer",
                    IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                };

                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        return Task.CompletedTask;
                    },
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UsuarioAPI",
                    policy => policy.RequireClaim("UsuarioAPINumero"));
            });

            //JSONWEBTOKEN

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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
                endpoints.MapRazorPages();
            });
        }
    }
}
