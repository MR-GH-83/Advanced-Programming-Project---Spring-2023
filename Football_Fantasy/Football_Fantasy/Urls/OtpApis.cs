using Football_Fantasy.Business;
using Football_Fantasy.Presentation;
namespace Football_Fantasy.Urls;

public class OtpApis
{
    public static void GetOtpApis(WebApplication app)
    {
        
       
        app.MapGet("/verfiyation/sendcode",async context =>
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/Otp.html");
            var fileStream = File.OpenRead(filePath);
            PresentationOtp.SendVerfiyCode();
            await fileStream.CopyToAsync(context.Response.Body);
        });
        app.MapGet("/sendcode", PresentationOtp.SendCode);
        app.MapGet("/verfiyation/set", PresentationOtp.SetEmailAndCode);
        app.MapGet("/verfiyation/emailcheck",PresentationOtp.ReturnEmailForShow);
        app.MapGet("/verfiyation/codecheck", PresentationOtp.ReturnCodeForCheck);
        app.MapGet("/verfiyation/saveDataToUser", PresentationOtp.SaveToUserTable);
        
    }
}