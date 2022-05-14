using Rollcall.Repositories;
using Rollcall.Services;
using Rollcall.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
class Program
{
    static private WebApplicationBuilder? builder;
    static private void ConfigureRepositories()
    {
        if (builder == null) { return; }
        var sqlConnectionString = builder.Configuration.GetConnectionString("sqlConnection");
        builder.Services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseMySql(sqlConnectionString, ServerVersion.AutoDetect(sqlConnectionString));
        });
        builder.Services.AddScoped<UserRepository>();

        builder.Services.AddScoped<ChildRepository>();
        builder.Services.AddScoped<GroupRepository>();

        builder.Services.AddScoped<SummaryRepository>();
        builder.Services.AddScoped<MealSchemaRepository>();

        builder.Services.AddScoped<MealRepository>();
        builder.Services.AddScoped<MaskRepository>();
    }
    static private void ConfigureAuthentication()
    {
        if (builder == null) { return; }
        // Note: there is a bug with newest version of Microsoft.IdentityModel assembly
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
    static public void ConfigureServices()
    {
        if (builder == null) { return; }

        builder.Services.AddScoped<DateValidationFilter>();
        builder.Services.AddScoped<MealUpdateDateFilter>();

        builder.Services.AddScoped<ChildExtractorFilter>();
        builder.Services.AddScoped<GroupExtractorFilter>();

        builder.Services.AddScoped<IChildService, ChildService>();
        builder.Services.AddScoped<IGroupService, GroupService>();

        builder.Services.AddScoped<IMealService<Child>, ChildMealService>();
        builder.Services.AddScoped<IGroupMealService, GroupMealService>();

        builder.Services.AddScoped<MealShaper>();

        builder.Services.AddScoped<IEqualityComparer<ChildMeal>, ChildMealComparer>();
        builder.Services.AddScoped<IEqualityComparer<GroupMask>, GroupMealComparer>();
    }
    static public void Main(String[] args)
    {
        builder = WebApplication.CreateBuilder(args);

        ConfigureRepositories();
        ConfigureAuthentication();
        ConfigureServices();

        builder.Services.AddCors();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(options => options.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin());

        app.MapControllers();

        app.Run();
    }
}
