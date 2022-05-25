using Lead2Change.Data.Contexts;
using Lead2Change.Services.CareerDeclarationService;
using Lead2Change.Services.Identity;
using Lead2Change.Services.Students;
using Lead2Change.Services.Goals;
using Lead2Change.Services.Answers;
using Lead2Change.Services.Questions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Lead2Change.Domain.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Lead2Change.Services.Interviews;
using Lead2Change.Services.QuestionInInterviews;
using Lead2Change.Services.Coaches;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using Lead2Change.Domain.Constants;
using Lead2Change.Web.Ui.Controllers;

namespace Lead2Change.Web.Ui
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
            configuration.GetSection("SendGrid:SenderName");
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            StudentsController.SetAdminInfo(Configuration.GetValue<string>("SendGrid:AdminEmailToSendTo"),Configuration.GetValue<bool>("SendGrid:SendEmailsToAdmin"));
            EmailSender.setApiKey(Configuration.GetValue<string>("SendGrid:apiKey"));
            EmailSender.setDefaultSenderName(Configuration.GetValue<string>("SendGrid:SenderName"));
            EmailSender.setDefaultSenderEmail(Configuration.GetValue<string>("SendGrid:SenderEmail"));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICareerDeclarationService, CareerDeclarationService>();
            services.AddScoped<IGoalsService, GoalsService>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IInterviewService, InterviewService>();
            services.AddScoped<IQuestionsService, QuestionsService>();
            services.AddScoped<IAnswersService, AnswersService>();
            services.AddScoped<IQuestionInInterviewService, QuestionInInterviewService>();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AspNetUsers, AspNetRoles>(options => {
                //options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddCors(options => {
                options.AddPolicy(name: "AllowSpecificOrigins", builder => {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            SeedTestUsers(app);
        }

        public void SeedTestUsers(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AspNetUsers>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AspNetRoles>>();
                CreateDefaultRoles(roleManager).Wait();
                CreateNewUser(userManager, "admin@lead2changeinc.org", "complex Shed reject lunch sodiUm thread faint glove execution context! add o4ligation", StringConstants.RoleNameAdmin).Wait();
            }
        }

        public static async Task CreateDefaultRoles(RoleManager<AspNetRoles> roleManager)
        {
            var hasAdmin = await roleManager.FindByNameAsync(StringConstants.RoleNameAdmin);
            if (hasAdmin == null)
                await roleManager.CreateAsync(new AspNetRoles()
                {
                    Name = StringConstants.RoleNameAdmin,
                    NormalizedName = StringConstants.RoleNameAdmin
                });

            var hasCoach = await roleManager.FindByNameAsync(StringConstants.RoleNameCoach);
            if (hasCoach == null)
                await roleManager.CreateAsync(new AspNetRoles()
                {
                    Name = StringConstants.RoleNameCoach,
                    NormalizedName = StringConstants.RoleNameCoach
                });

            var hasStudent = await roleManager.FindByNameAsync(StringConstants.RoleNameStudent);
            if (hasStudent == null)
                await roleManager.CreateAsync(new AspNetRoles()
                {
                    Name = StringConstants.RoleNameStudent,
                    NormalizedName = StringConstants.RoleNameStudent
                });
        }

        public static async Task CreateNewUser(UserManager<AspNetUsers> userManager, string email, string password, string roleName, bool confirm = true)
        {
            var identityUser = new AspNetUsers()
            {
                UserName = email,
                Email = email
            };

            var result = await userManager.CreateAsync(identityUser, password);
            if (confirm)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                await userManager.ConfirmEmailAsync(identityUser, token);
            }

            if (result.Succeeded)
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    var role = await userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    //Do something because the user does not exist
                }
            }
            else
            {
                //Do something because the user could not be created
            }
        }
    }
}
