using System.Reflection.Metadata;

namespace Book_A_Doc.Infrastructre.Services.Mail.Options;

public class MailOptions
{

    public static string SectionName => "MailSettings";
    public string Mail { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; }
}
