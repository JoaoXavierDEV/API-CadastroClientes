using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace XPTO.Presentation.API.Configuration
{
    public static class ApiConfig
    {
        public static WebApplicationBuilder AddWebApiConfig(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (builder.Environment.IsDevelopment())
                builder.Configuration.AddUserSecrets<Program>();

            return builder;
        }

        public static IServiceCollection AddWebApiConfig(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                ////using Microsoft.AspNetCore.Mvc.Formatters;
                //options.RespectBrowserAcceptHeader = true;
                //options.OutputFormatters.RemoveType<StringOutputFormatter>();
                //options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            }).AddJsonOptions(op =>
            {
                op.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                op.JsonSerializerOptions.WriteIndented = true;
            });


            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("X-Version"));
            })
            .AddMvc(options => { })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });


            services.Configure<ApiBehaviorOptions>(
                op =>
                {
                    op.SuppressModelStateInvalidFilter = true;
                });

            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }
        public static IApplicationBuilder UseApiConfig(this IApplicationBuilder app)
        {
            var Environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            if (Environment.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // funciona em modo dev, refatorar
                //app.UseCors("Production");

                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            //app.UseMiddleware<ExceptionMiddleware>();

            // redirecionar automaticamente para https
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            return app;
        }
    }
}
