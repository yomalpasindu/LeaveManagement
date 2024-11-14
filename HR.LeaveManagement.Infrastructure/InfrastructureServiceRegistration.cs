using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Modles.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Infrastructure.Logging;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdapter<>));
            return services;
        }
    }
}
