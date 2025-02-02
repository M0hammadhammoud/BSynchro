using BSynchro.RJP.Accounts.Application.Contracts;
using BSynchro.RJP.Accounts.Application.Services;
using Common.MessageQueueSender.Contracts;
using Common.MessageQueueSender.Models.DTOs;
using Common.MessageQueueSender.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BSynchro.RJP.Accounts.Application
{
    public static class DependencyInjection
    {
        public static void InjectServices(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddSingleton(sp => configurationManager.GetSection("MessageTypeQueues").Get<List<MessageConfigurationDTO>>());
            services.AddTransient(sp => configurationManager.GetSection("RabbitMq").Get<RabbitMqConfigurationDTO>());
            services.AddSingleton<IMessageQueueSender, MessageQueueSender>();

            services.AddScoped<IRequestInfoService, RequestInfoService>();
            services.AddScoped<IAccountService, AccountService>();

            //registered this service as a transient since user info could be updated several times due to account containing calculated field(s)
            services.AddTransient<ICustomerService, CustomerService>();
        }   
    }
}
