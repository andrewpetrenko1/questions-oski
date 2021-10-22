using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using questions_data;
using questions_data.Repositories;
using questions_view.Domains;
using questions_view;
using Microsoft.IdentityModel.Tokens;

namespace questions_api
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
      services.AddControllers();

      services.AddDbContext<IquestionsDbContext, questionsDbContext>(builder =>
        builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition")
            .WithOrigins("http://localhost:4200");
        });
      });

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.RequireHttpsMetadata = true;
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,

            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,

            ValidateLifetime = true
          };
        });

      services.AddTransient<IUserRepository, UserRepository>();
      services.AddTransient<IUserDomain, UserDomain>();

      var mapConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperPr()));
      services.AddSingleton(mapConfig.CreateMapper());
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
