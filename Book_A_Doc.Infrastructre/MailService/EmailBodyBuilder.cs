using System.Text;

namespace Book_A_Doc.Infrastructre.MailService;

public static class EmailBodyBuilder
{
    public static string GenerateEmailBody(
        string templateName,
        Dictionary<string, string> templateModel)
    {
        var currentDirectory = Directory.GetCurrentDirectory();

        var templatePath = Path.Combine(
            currentDirectory,
            "Templates",
            $"{templateName}.html");

        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException(
                $"Email template '{templateName}' was not found.",
                templatePath);
        }

        var body = File.ReadAllText(templatePath, Encoding.UTF8);

        foreach (var item in templateModel)
        {
            body = body.Replace($"{{{{{item.Key}}}}}", item.Value);
        }

        return body;
    }
}