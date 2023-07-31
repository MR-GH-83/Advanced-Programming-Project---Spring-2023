using Football_Fantasy.Business;
using Football_Fantasy.SubClasses;
namespace Football_Fantasy.Presentation;

public class GetValueForLogin
{
    public string email { get; set; }
    public string password { get; set; }
    
}

public class GetValueForToken
{
    public string email { get; set; }
}

public class GetValueForReadToken
{
    public string token { get; set; }
}
public class PresentationLogin
{
    public static object CreateToken(GetValueForToken user)
    {
        if (BusinessLogin.IsThisEmailExist(user.email))
        {
            string ReToken= SubClasses.Token.GenerateToken("email", user.email);
            return new
            {
                massage = ReToken
            };
        }

        return new
        {
            massage = "This Email does not exist."
        };
        
    }
    public static object ReadToken(GetValueForReadToken token)
    {
        string value = SubClasses.Token.ReadToken("email", token.token);
        if (BusinessLogin.IsThisEmailExist(value))
        {
            return new
            {
                status="OK",
                massage = value
            };
        }

        return new
        {
            status = "Fail",
            massage = "Please Login"
        };

    }
    
    public static object Login(GetValueForLogin user)
    {
        if (BusinessLogin.Login(user.email, user.password))
        {
            return new
            {
                status = "OK",
                massage = "Login was successful."
            };
        }

        if (BusinessLogin.EmailExistButPasswordNotMatch(user.email, user.password))
        {
            return new
            {
                status = "Fail",
                massage = "Your email or password is wrong"
            };
        }

        return new
        {
            status = "Fail",
            massage = "Your email or password is wrong"
        };


    }
    
}