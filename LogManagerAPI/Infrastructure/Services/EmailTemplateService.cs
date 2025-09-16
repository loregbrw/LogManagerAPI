namespace Infrastructure.Services;

using Application.Interfaces.Services.Core;
using Application.Models.Options;

public class EmailTemplateService(FrontendOptions frontendOptions) : IEmailTemplateService
{
    private readonly FrontendOptions _frontendOptions = frontendOptions;

    public string GetRegistrationEmail(string token) =>
$"""
Olá,

Você está recebendo este e-mail porque alguém cadastrou o seu endereço de e-mail na plataforma LogManager.

Para concluir seu cadastro, utilize o seguinte código de verificação (válido por 30 dias):

{token}

Ou, se preferir, clique diretamente no link abaixo para completar seu cadastro:
{_frontendOptions.Url}/complete-registration?token={token}

Este código expira em 30 dias. Após esse período, será necessário solicitar um novo convite de cadastro. ⚠️

Se você não esperava este e-mail, pode ignorá-lo com segurança.

Atenciosamente,  
Equipe LogManager.
""";

}