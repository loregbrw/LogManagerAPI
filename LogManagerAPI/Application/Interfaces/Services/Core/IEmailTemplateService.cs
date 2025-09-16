namespace Application.Interfaces.Services.Core;

public interface IEmailTemplateService
{
    string GetRegistrationEmail(string token);
}