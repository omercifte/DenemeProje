using DenemeProje.Context;
using DenemeProje.Models;
using DenemeProje.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace DenemeProje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(AppDbContext context, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
           
            RecurringJob.AddOrUpdate("Mailgonderimi", () => SendMail(), Cron.Hourly);
            return RedirectToAction("Customers");
        }
        public void SendMail()
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var senderPassword = _configuration["EmailSettings:SenderPassword"];

            using var client = new SmtpClient(smtpServer, smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);
            client.EnableSsl = true;
            
           
            var customers = _context.Customers.ToList();
            foreach (var customer in customers)
            {
                var mailMessage = new MailMessage(senderEmail, customer.Mail, "Saatlik update", "Saatlik mailin");
                client.Send(mailMessage);
            }


        }

        public IActionResult Customers()
        {
            var customers= _context.Customers.ToList();
            return View(customers);
        }
        [HttpDelete]
        public IActionResult DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }

    }
}
