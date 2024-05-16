using SendGrid;
using SendGrid.Helpers.Mail;

namespace digitalguestbook.Services;

public class EmailSender
{
    public async Task SendEmail(string subject, string toEmail, string username, string message)
    {
        var apiKey = "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";
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("vbaudisch@student.tgm.ac.at", "Digital Guestbook");
        var to = new EmailAddress(toEmail, username);
        var plainTextContent = message;
        var htmlContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}