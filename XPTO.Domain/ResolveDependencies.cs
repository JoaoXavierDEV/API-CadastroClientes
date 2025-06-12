using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace XPTO.Domain
{
    public static class ResolveDependencies
    {
        public static void AddLayerDomain(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ResolveDependencies).Assembly, includeInternalTypes: true);
        }
    }
}
