namespace Infrastructure.Services;

using System.Net;
using System.Net.Mail;
using Application.Interfaces.Services.Core;
using Application.Models.Options;
using Microsoft.Extensions.Options;

public class EmailSenderService(IOptions<EmailSenderOptions> options) : IEmailSenderService
{
    private readonly EmailSenderOptions _options = options.Value;

    public void SendEmail(string recipient, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_options.Email, _options.DisplayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };

        mail.To.Add(recipient);

        using var smtp = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(_options.Email, _options.Password),
            EnableSsl = true
        };

        smtp.Send(mail);
    }
}