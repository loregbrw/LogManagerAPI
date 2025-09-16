
/*
    TecnoToolingIO API - Inventory Management Software with incoming and outgoing stock control.
    Copyright (C) 2025 Lorena Gobara Falci

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    Contact: loregobara@gmail.com
*/

namespace Infrastructure.Services;

using System.Net;
using System.Net.Mail;
using Application.Interfaces.Services.Core;
using Application.Models.Options;
using Microsoft.Extensions.Options;

public class EmailSenderService(IOptions<EmailSenderOptions> options) : IEmailSenderService
{
    private readonly EmailSenderOptions _options = options.Value;
    private const string displayName = "Equipe TecnoTooling";

    public void SendEmail(string recipient, string subject, string body)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(_options.Email, displayName),
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