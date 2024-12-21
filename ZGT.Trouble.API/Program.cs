using ZGT.Trouble.API.Hubs;
using ZGT.Trouble.PL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Ui.Web;
using System.Reflection;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        //Add SignalR Config
        builder.Services.AddSignalR();
            //.AddJsonProtocol(options =>
            //{
            //    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
            //});
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "Trouble API",
                Version = "v1",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Glen",
                    Email = "email",
                    Url = new Uri("https://www.fvtc.edu/")
                }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlpath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlpath);
        });

        builder.Services.AddDbContextPool<TroubleEntities>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("TroubleGameConnection"));
        });

        // configure strongly typed settings object
        //builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        // configure DI for application services
        //builder.Services.AddScoped<IUserService, UserService>();

        string connection = builder.Configuration.GetConnectionString("TroubleGameConnection");

        //var app = builder.Build();
        builder.Services.AddSerilogUi(options =>
        {
            options.UseSqlServer(connection, "Logs");
        });

        Log.Warning("API Program {UserId}", "bfoote");

        // Configure the HTTP request pipeline.

        var configSettings = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();


        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configSettings)
            .CreateLogger();

        builder.Services
            .AddLogging(c => c.AddDebug())
            .AddLogging(c => c.AddSerilog())
            .AddLogging(c => c.AddEventLog())
            .AddLogging(c => c.AddConsole());


        var app = builder.Build();

        app.UseSerilogUi(options =>
        {
            options.RoutePrefix = "logs";
        });


        Log.Warning("API Program {UserId}", "bfoote");

        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        // custom jwt auth middleware
        //app.UseMiddleware<JwtMiddleware>();

        //// Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI();
        //}

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        //app.MapControllers();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHub<UIHub>("/uihub");
        });
  
        app.Run();
    }
}
