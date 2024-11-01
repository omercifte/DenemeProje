using System.Net.Mail;
using System.Net;
using DenemeProje.Context;
using Microsoft.Extensions.DependencyInjection;
using DenemeProje.Models;

namespace DenemeProje.Services
{
    public  class MailService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public MailService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _context = GetScopedData();
            _serviceProvider = serviceProvider;
        }

        public void SendDailyEmail( string subject, string body)
        {
          

           
        }
        public AppDbContext GetScopedData()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                return scopedService;
            }
        }


    }
}





