using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Sim.UI.Web.Areas.Identity.IdentityHostingStartup))]
namespace Sim.UI.Web.Areas.Identity
{
    using Sim.Cross.Identity;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
            builder.ConfigureServices((context, services) => {/*
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityContext>();
            */

                //registra dbcontext identity
                services.AddDbContext<IdentityContext>(options => {

                    options.UseSqlServer(context.Configuration.GetConnectionString("IdentityContextConnection"));

                });

                //registra o dbcontext padrão do identity
                services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>();

                services.AddScoped<IAppServiceUser, RepositoryUser>();

                //configura o identity
                services.Configure<IdentityOptions>(options => {

                    //define regras de login
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;

                    //define users
                    options.User.RequireUniqueEmail = true;

                    //define regras senhas
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;

                    //define Lockout
                    options.Lockout.AllowedForNewUsers = false;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(720);
                    options.Lockout.MaxFailedAccessAttempts = 5;

                });

            });
        }
    }
}