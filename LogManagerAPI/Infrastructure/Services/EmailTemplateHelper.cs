using Application.Interfaces.Services.Core;
using Application.Models.Options;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class EmailTemplateHelper(
    IOptions<RegistrationEmailOptions> emailOptions, IOptions<FrontendOptions> frontendOptions
) : IEmailTemplateHelper
{
    private readonly RegistrationEmailOptions _emailOptions = emailOptions.Value;
    private readonly FrontendOptions _frontendOptions = frontendOptions.Value;

    public string GetRegistrationEmail(string token)
    {
        return _emailOptions.Template
            .Replace("{{token}}", token)
            .Replace("{{url}}", _frontendOptions.Url);
    }

    public string GetSubject() => _emailOptions.Subject;
}
