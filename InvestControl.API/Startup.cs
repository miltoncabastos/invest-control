using InvestControl.Application.Services;
using InvestControl.Application.Services.Interfaces;
using InvestControl.Domain.Repository;
using InvestControl.Infra.Context;
using InvestControl.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace InvestControl.API
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
            
            // var connectionString = Configuration.GetConnectionString("InvestControlCn");
            // services.AddDbContext<InvestControlContext>(option => option.UseSqlServer(connectionString));
            services.AddDbContext<InvestControlContext>(option =>
                option.UseInMemoryDatabase("InvestControl"));

            services.AddScoped<IImpostoDeRendaService, ImpostoDeRendaService>();
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();
            services.AddScoped<IEventoRepository, EventoRepository>();
            services.AddScoped<IUploadInformationsService, UploadInformationsService>();
            services.AddScoped<IUploadB3MovimentacaoService, UploadB3MovimentacaoService>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InvestControl.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvestControl.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
