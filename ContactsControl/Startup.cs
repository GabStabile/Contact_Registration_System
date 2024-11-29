using ContactsControl.Data;
using ContactsControl.Helper;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ISession = ContactsControl.Helper.ISession;

namespace ContactsControl
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString(
                        "DataBase"
                    )
                )
            );

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IContactRepositorie, ContactRepositorie>();
            services.AddScoped<IUserRepositorie, UserRepositorie>();
            services.AddScoped<ISession, Session>();
            // adiciona o suporte � funcionalidade de sess�o no ASP.NET Core. Sess�es permitem armazenar dados tempor�rios por usu�rio entre diferentes solicita��es HTTP
            services.AddSession(o =>
                {
                // Configura o cookie para ser acess�vel apenas pelo servidor, prevenindo que scripts maliciosos (por exemplo, XSS - Cross-Site Scripting) acessem o cookie
                o.Cookie.HttpOnly = true;
                // Marca o cookie como essencial, o que significa que ele ser� enviado mesmo que a aplica��o esteja configurada para respeitar as prefer�ncias de consentimento de cookies do usu�rio
                o.Cookie.IsEssential = true;
                }
            );
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}