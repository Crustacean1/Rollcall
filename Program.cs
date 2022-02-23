using Rollcall.Repositories;
using Rollcall.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
class Program
{
    static private WebApplicationBuilder? builder;
    static private void ConfigureRepositories()
    {
        if(builder == null){return;}
        var sqlConnectionString = builder.Configuration.GetConnectionString("sqlConnection");
        builder.Services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseMySql(sqlConnectionString, ServerVersion.AutoDetect(sqlConnectionString));
        });
        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<IGroupRepository,GroupRepository>();
        builder.Services.AddScoped<IChildRepository,ChildRepository>();
    }
    static private void ConfigureAuthentication()
    {
        if(builder == null){return;}
        // Note: there is a bug with newest version of Microsoft.IdentityModel assembly, should have used the other one from asp net but idk
        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer((options) =>
            {
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                var issuer = jwtSettings.GetValue<string>("Issuer");
                var audience = jwtSettings.GetValue<string>("Issuer");
                var secret = jwtSettings.GetValue<string>("Secret");

                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,

                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
                };
            });
        builder.Services.AddTransient<JwtIssuerService>();
        builder.Services.AddTransient<AuthorizationService>();

    }
    static public void ConfigureServices(){
        if(builder == null){return;}
        builder.Services.AddScoped<AttendanceHandlerService>();
        builder.Services.AddScoped<ChildHandlerService>();
        builder.Services.AddScoped<SchemaService>();
    }
    static public void Main(String[] args)
    {
        builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureRepositories();
        ConfigureAuthentication();
        ConfigureServices();

        builder.Services.AddControllers();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
        }

        app.UseRouting();
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}