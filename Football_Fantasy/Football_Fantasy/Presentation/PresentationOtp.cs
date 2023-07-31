using Football_Fantasy.Business;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Runtime.CompilerServices;
using SendGrid.Helpers.Mail;

namespace Football_Fantasy.Presentation;


public class EmailSender
{
    public static void SendEmail(string email,string subject,string massage)
    {
            /*var EmailFrom = "football_fantasy_shahed@outlook.com";*/
            var EmailFrom = "football.fantasy.shahed@gmail.com";
            /*var Password = "Shaheduniversity";*/
            var Password = "ShahedUniversity.CE1401";
// create a new MailMessage object
            MailMessage message = new MailMessage();
            message.From = new MailAddress(EmailFrom);
            message.To.Add(new MailAddress(email));
            message.Subject = subject;
            message.Body = massage;

// create a new SmtpClient object
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            //client.Host = "smtp.office365.com";
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(EmailFrom, Password);
            client.EnableSsl = true;

// send the email
            client.Send(message);
            //ShahedUniversity.CE1401
    }

    public static void Send(string email)
    {
        var apiKey="m";
        var client = new SendGridClient(apiKey);
        
        var from = new EmailAddress("example@example.com", "Example User");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress("recipient@example.com", "Example User");
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = client.SendEmailAsync(msg);
    }
}

public class PresentationOtp
{
    public static string Email;
    public static string Code;
    public static void SendVerfiyCode()
    {
        string subject = "Football fantasy security code";
        Code = BusinessOtp.SecurityCode();
        string massage =
            "Football fantasy account \n Security code \n Please use the following security code for your account. "+Email+" \n Security code: "+Code;
        EmailSender.SendEmail(Email,subject,massage);
    }

    public static object SendCode(string email)
    {
        string subject = "Football fantasy security code";
        Code = BusinessOtp.SecurityCode();
        string massage =
            "Football fantasy account \n Security code \n Please use the following security code for your account. " +
            Email + " \n Security code: " + Code;
        try
        {
            EmailSender.SendEmail(email,subject,massage);
            return new
            {
                status = "OK",
                massage = email,
                code = Code
            };
        }
        catch (Exception e)
        {
            return new
            {
                status = "Fail",
                massage = e,
                code = Code
            };
        }
    }
    

    public static object SaveToUserTable()
    {
        BusinessOtp.SaveDataToUserTable(Email);
        return new
        {
            status = "OK",
            massage= Email
        };
    }
    public static object ReturnCodeForCheck()
    {
        return new
        {
            code = Code
        };
    }
    
    public static object ReturnEmailForShow()
    {
        return new
        {
            massage =Email
        };
    }

    public static object SetEmailAndCode(string email)
    {
        Email = email;
        Code = BusinessOtp.SecurityCode();
        return new
        {
            status = "OK",
            massage = Email,
            code = Code
        };
    }

}