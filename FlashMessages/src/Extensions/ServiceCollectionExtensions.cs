using System;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FlashMessages.Options;

namespace SharpGrip.FlashMessages.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlashMessages(this IServiceCollection services, Action<FlashMessagesOptions>? setupOptions = null)
        {
            services.AddOptions();
            services.AddHttpContextAccessor();

            services.AddScoped<IFlasher, Flasher>();

            if (setupOptions != null)
            {
                services.Configure(setupOptions);
            }

            return services;
        }
    }
}