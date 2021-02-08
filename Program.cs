using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendingEmailwFluentEmail
{
    class Program
    {
        // use "papercut" application to listen to emails being sent
        static async Task Main(string[] args)
        {
            // settings
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                // send via email server not by file directory
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
                //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation = @"C:\Users\mackn\Documents\C#\TimCory Email Demo"
            });

            // default server uses sender settings
            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            // email template with stringbuilder
            StringBuilder template = new();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it.</p>");
            template.AppendLine("- The MackCo Team");

            // send an email
            var email = await Email
                .From("mack@weaver.com")
                .To("mw@mailinator.com", "Matchew")
                .Subject("Thanks")
                .UsingTemplate(template.ToString(), new { FirstName = "Mack", ProductName = "Baconator" })
                //.Body("You're the best.")
                .SendAsync();
        }
    }
}
