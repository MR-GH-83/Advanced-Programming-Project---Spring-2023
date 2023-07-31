using Football_Fantasy.Presentation;
namespace Football_Fantasy.Urls;

public class LoginApis
{
    public static void GetApis(WebApplication app)
    {
        app.MapGet("/authentication/signin",async context =>
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/sign-in.html");
            var fileStream = File.OpenRead(filePath);
            await fileStream.CopyToAsync(context.Response.Body);
        });
        app.MapPost("/authentication/signin-request", PresentationLogin.Login);
        app.MapPost("/authentication/generate-token", PresentationLogin.CreateToken);
        app.MapPost("/authentication/read-token", PresentationLogin.ReadToken);
    }
}