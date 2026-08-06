namespace Book_A_Doc.Application.Services;

public interface IEmailTemplateService
{
    string GenerateEmailConfirmationTemplate(
        string userName,
        string confirmationLink);

    string GenerateForgotPasswordTemplate(
       string userName,
       string otp);
}