using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Data;
using WebApplication.Dtos;
using WebApplication.EmailService;
using WebApplication.Helpers;
using WebApplication.Managers;
using WebApplication.Managers.Interfaces;
using WebApplication.Mappers;
using WebApplication.Mappers.Interfaces;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Repositories.Interfaces;

namespace WebApplication
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
                    });
            });
            
            // register Managers
            services.AddTransient<IAddressManager, AddressManager>();
            services.AddTransient<ICompanyManager, CompanyManager>();
            services.AddTransient<ICourseManager, CourseManager>();
            services.AddTransient<ICoördinatorManager, CoördinatorManager>();
            services.AddTransient<IInternshipManager, InternshipManager>();
            services.AddTransient<ILectorManager<Lector>, LectorManager<Lector>>();
            services.AddTransient<IPeriodManager, PeriodManager>();
            services.AddTransient<IPersonManager<Person>, PersonManager<Person>>();
            services.AddTransient<IStudentManager, StudentManager>();
            services.AddTransient<ITechnologyManager, TechnologyManager>();
            services.AddTransient<IValidationManager, ValidationManager>();
            services.AddTransient<ILoginManager, LoginManager>();
            services.AddTransient<IEmailManager, EmailManager>();

            // Register mappers
            services.AddTransient<IInternshipMapper, InternshipMapper>();
            services.AddTransient<ICompanyMapper, CompanyMapper>();
            services.AddTransient<ICompanyCreateMapper, CompanyCreateMapper>();
            services.AddTransient<IAddressMapper, AddressMapper>();
            services.AddTransient<IPersonMapper<Person, PersonDto>, PersonMapper<Person, PersonDto>>();
            services.AddTransient<IInternshipOverviewMapper, InternshipOverviewMapper>();
            services.AddTransient<IInternshipListMapper, InternshipListMapper>();
            services.AddTransient<IInternshipListStudentMapper, InternshipListStudentMapper>();
            services.AddTransient<IValidationOverviewMapper, ValidationOverviewMapper>();
            services.AddTransient<IValidationUpdateMapper, ValidationUpdateMapper>();
            services.AddTransient<ICourseMapper, CourseMapper>();
            services.AddTransient<IPeriodMapper, PeriodMapper>();
            services.AddTransient<ITechnologyMapper, TechnologyMapper>();
            services.AddTransient<IValidationUpdateMapper, ValidationUpdateMapper>();
            services.AddTransient<IStudentMapper, StudentMapper>();
            services.AddTransient<IStudentOverviewMapper, StudentOverviewMapper>();
            services.AddTransient<IStudentListMapper, StudentListMapper>();
            services.AddTransient<IFeedBackMapper, FeedBackMapper>();
            services.AddTransient<ILectorMapper<Lector, LectorDto>, LectorMapper<Lector, LectorDto>>();
            services.AddTransient<IUpdateStateCompanyMapper, UpdateStateCompanyMapper>();

            // Register Repositories
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();
            services.AddTransient<ICoördinatorRepository, CoördinatorRepository>();
            services.AddTransient<IInternshipRepository, InternshipRepository>();
            services.AddTransient<ILectorRepository<Lector>, LectorRepository<Lector>>();
            services.AddTransient<IPeriodRepository, PeriodRepository>();
            services.AddTransient<IPersonRepository<Person>, PersonRepository<Person>>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ITechnologyRepository, TechnologyRepository>();
            services.AddTransient<IValidationRepository, ValidationRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            
            // Register CrudRepositories
            services.AddTransient<ICrudRepository<BaseModel>, CrudRepository<BaseModel>>();
            services.AddTransient<ICrudRepository<Address>, AddressRepository>();
            services.AddTransient<ICrudRepository<Company>, CompanyRepository>();
            services.AddTransient<ICrudRepository<Course>, CourseRepository>();
            services.AddTransient<ICrudRepository<Coördinator>, CoördinatorRepository>();
            services.AddTransient<ICrudRepository<Models.Internship>, InternshipRepository>();
            services.AddTransient<ICrudRepository<Lector>, LectorRepository<Lector>>();
            services.AddTransient<ICrudRepository<Period>, PeriodRepository>();
            services.AddTransient<ICrudRepository<Person>, PersonRepository<Person>>();
            services.AddTransient<ICrudRepository<Student>, StudentRepository>();
            services.AddTransient<ICrudRepository<Technology>, TechnologyRepository>();
            services.AddTransient<ICrudRepository<Validation>, ValidationRepository>();

            // Register EmailService
            services.AddSingleton<ISendGridService, SendGridService>();
            
            // Register UserVerifier
            services.AddTransient<IUserVerifier, UserVerifier>();
            
            // Register PasswordGenerator
            services.AddSingleton<IPasswordGenerator, PasswordGenerator>();
            
            // Register PdfGenerator
            services.AddSingleton<IPdfGenerator, PdfGenerator>();
            
            // Register Context
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddControllers();
            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SqlConnection")));
            
            // Add authentication
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["AppSettings:Token"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/error");
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler("/error");
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.Run(context => context.Response.WriteAsync("Hello from ASP.NET Core!"));
        }
    }
}