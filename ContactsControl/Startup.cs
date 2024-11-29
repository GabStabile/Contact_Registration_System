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
            // adiciona o suporte à funcionalidade de sessão no ASP.NET Core. Sessões permitem armazenar dados temporários por usuário entre diferentes solicitações HTTP
            services.AddSession(o =>
                {
                // Configura o cookie para ser acessível apenas pelo servidor, prevenindo que scripts maliciosos (por exemplo, XSS - Cross-Site Scripting) acessem o cookie
                o.Cookie.HttpOnly = true;
                // Marca o cookie como essencial, o que significa que ele será enviado mesmo que a aplicação esteja configurada para respeitar as preferências de consentimento de cookies do usuário
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