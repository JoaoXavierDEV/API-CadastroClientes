﻿using Microsoft.Extensions.DependencyInjection;
using XPTO.Application.Interfaces;
using XPTO.Application.Services;

namespace XPTO.Application
{
    public static class ResolveDependencies
    {
        public static void AddLayerApplication(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IEnderecoService, EnderecoService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }
    }
}
