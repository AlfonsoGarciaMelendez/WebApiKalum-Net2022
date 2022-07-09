using Microsoft.EntityFrameworkCore;
using WebApiKalum.Utilities;
namespace WebApiKalum
{
    public class Startup
    {
        public IConfiguration Configuration { get;}
        public Startup(IConfiguration _Configuration)
        {
            this.Configuration = _Configuration;
        }

        public void ConfigureServices(IServiceCollection _services)
        {
            _services.AddTransient<ActionFilter>();
            _services.AddControllers(options => options.Filters.Add(typeof(ErrorFilterException)));
            _services.AddAutoMapper(typeof(Startup));
            _services.AddControllers();
            _services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            _services.AddDbContext<KalumDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            _services.AddEndpointsApiExplorer();
            _services.AddSwaggerGen();   
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

        }

    }
}