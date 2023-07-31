using Football_Fantasy.Presentation;
namespace Football_Fantasy.Urls;

public static partial class SignUpApi
{
    public static void GetApis(WebApplication app)
    {

        app.MapGet("/authentication/signup", async context =>
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "HtmlFiles/sign-up.html");
            var fileStream = File.OpenRead(filePath);
            await fileStream.CopyToAsync(context.Response.Body);
        });
        app.MapPost("/authentication/signup-request", Signup.signup);
        app.MapGet("/readtokenotp", Signup.readtoken);
    }
}