using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using votacao_backend.Api.DbContexts;
using votacao_backend.Api.Models;
using votacao_backend.Api.Repositories;

namespace Api
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
             services
              .AddControllers()
              .AddXmlDataContractSerializerFormatters()
              .ConfigureApiBehaviorOptions(setupAction =>
               {
                   setupAction.InvalidModelStateResponseFactory = context =>
                   {
                        // create a problem details object
                        var problemDetailsFactory = context.HttpContext.RequestServices
                           .GetRequiredService<ProblemDetailsFactory>();

                       var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                               context.HttpContext,
                               context.ModelState);

                       if (context.ModelState.ErrorCount > 0 && problemDetails.Status.Equals(StatusCodes.Status400BadRequest))
                       {
                           return new BadRequestObjectResult(new ErroRetorno(string.Join("; ", context.ModelState.Values
                                      .SelectMany(x => x.Errors)
                                      .Select(x => x.ErrorMessage))));
                       }

                       return new StatusCodeResult(500);
                   };
               });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
           {

               c.SwaggerDoc("v1",
                   new OpenApiInfo
                   {
                       Title = "Sistema COBRIC",
                       Version = "v1",
                       Description = "Serviço criado para processos de Votacoes"
                   });
           });

            services.AddScoped<IVotacaoRepository, VotacaoRepository>();
            services.AddScoped<IVotosRepository, VotosRepository>();

            services.AddDbContext<VotacaoContext>(options =>
           {
               options.UseNpgsql("User ID=sadbcobric@dbcobric ;Password=147258369Co#;Server=dbcobric.postgres.database.azure.com;Port=5432;Database=dbcobric; Integrated Security=true;Pooling=true;");
           });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serviço de Votacao");
            });
        }
    }
}
