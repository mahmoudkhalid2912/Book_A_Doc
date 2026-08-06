using Book_A_Doc.Application.Services;
using Book_A_Doc.Infrastructre.Services.Mail.Builder;

namespace Book_A_Doc.Infrastructre.Services.Mail.Service;

public class EmailTemplateService : IEmailTemplateService
{
    public string GenerateEmailConfirmationTemplate(
        string userName,
        string confirmationLink)
    {
        return EmailBodyBuilder.GenerateEmailBody(
            "EmailConfirmation",
            new Dictionary<string, string>
            {
                { "UserName", userName },
                { "ConfirmationLink", confirmationLink }
            });
    }

    public string GenerateForgotPasswordTemplate(
    string userName,
    string otp)
    {
        return EmailBodyBuilder.GenerateEmailBody(
            "ForgetPassword",
            new Dictionary<string, string>
            {
            { "UserName", userName },
            { "OTP", otp }
            });
    }
}
