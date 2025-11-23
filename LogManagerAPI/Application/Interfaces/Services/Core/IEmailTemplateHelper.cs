namespace Application.Interfaces.Services.Core;

public interface IEmailTemplateHelper
{
    string GetRegistrationEmail(string token);
    public string GetSubject();
}