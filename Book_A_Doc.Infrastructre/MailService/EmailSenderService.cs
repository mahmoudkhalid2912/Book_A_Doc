using Book_A_Doc.Infrastructre.JwtServices.OptionsClass;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Book_A_Doc.Infrastructre.MailService;

public class EmailSender(IOptions<MailOptions> mailOptions) : IEmailSender
{
    private readonly MailOptions mailOptions = mailOptions.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(mailOptions.Mail),
            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();


        smtp.Connect(   mailOptions.Host, mailOptions.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(mailOptions.Mail, mailOptions.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}