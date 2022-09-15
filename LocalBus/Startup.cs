 using LocalBus.Context;
using LocalBus.Models;
using LocalBus.Repositories;
using LocalBus.Repositories.Interfaces;
using LocalBus.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace LocalBus;
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
        services.AddControllers().AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        //usado devido ao erro System.Text.Json.JsonException:
        //um possível ciclo de objeto foi detectado.
        //Isso pode ser devido a um ciclo ou se a profundidade do
        //objeto for maior que a profundidade máxima permitida de 32.
        //Considere usar ReferenceHandler.Preserve em JsonSerializerOptions
        //para oferecer suporte a ciclos.

        

        services.AddIdentity<MyUser, IdentityRole>()      //Serviço do IdentityUser
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddPasswordValidator<ValidadorDeSenha<MyUser>>();
            

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(1);
        });

        //cofira o formato das senhas do identity

        services.Configure<DataProtectionTokenProviderOptions>(
            options => options.TokenLifespan = TimeSpan.FromMinutes(3)
           );;
    

        services.Configure<IdentityOptions>(options =>
        {
            //configurações senha
         //   options.SignIn.RequireConfirmedAccount = false; //aprovação de conta preciso ver depois
            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            //bloqueio de usuario
         // options.Lockout.MaxFailedAccessAttempts = 3;
        //  options.Lockout.AllowedForNewUsers = true;
            
        });
        //nome da seção
        services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens")); //necessario para imgs

        services.AddControllersWithViews();

        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // Container DI ele cria a instancia
        services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);
        services.AddTransient<IEscolaRepository, EscolaRepository>();
        services.AddTransient<IPontosRepository, PontosRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();


        //necessario usar addPolicy para authenticar o admin
        services.AddAuthorization(options => { options.AddPolicy("Admin", politica => { politica.RequireRole("Admin"); }); });
        services.AddAuthorization(options => { options.AddPolicy("Member", politica => { politica.RequireRole("Member"); }); });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
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
        app.UseSession();
        app.UseRouting();

        //cria os perfis
        seedUserRoleInitial.SeedRoles();
        //cria os usuarios e atribui ao perfil
        seedUserRoleInitial.SeedUsers();

        app.UseAuthentication(); //adicionado para o Identity
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
         );
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

           endpoints.MapControllerRoute(
            name: "Administrador",
            pattern: "Administrador/{controller=Administrador}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "Aluno",
                pattern: "Aluno/{controller=Aluno}/{action=Index}/{id?}"
              );
        
        });




    }
}
