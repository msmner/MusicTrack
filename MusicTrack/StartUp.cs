using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicTrack.Infrastructure;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Services;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicTrack
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddEndpointsApiExplorer();
            services.AddDbContext<MusicTrackDbContext>(
                            options => options.UseNpgsql(Configuration.GetConnectionString("MusicTrackDatabase")));

            services.AddSwaggerGen(options =>
            {
                var apiInfo = new OpenApiInfo { Title = "MusicTrack", Version = "v1" };
                options.SwaggerDoc("controllers", apiInfo);

                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Description = @"JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer 12345abcdef'",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    });

                options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
            services.AddScoped<IPlaylistRepository, PlayListRepository>();
            services.AddScoped<ITrackRepository, TrackRepository>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddTransient<ITrackService, TrackService>();
            services.AddTransient<IPlaylistService, PlaylistService>();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/controllers/swagger.json", "REST API");
            });
            app.UseAuthorization();
            app.UseEndpoints(x => x.MapControllers());
            app.UseHttpsRedirection();

        }
    }
}
