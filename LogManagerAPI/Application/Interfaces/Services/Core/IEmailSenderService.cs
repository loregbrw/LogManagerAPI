namespace Application.Interfaces.Services.Core;

public interface IEmailSenderService
{
    void SendEmail(string recipient, string subject, string body);
}