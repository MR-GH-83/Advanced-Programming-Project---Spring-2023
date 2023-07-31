using Football_Fantasy.Business;

namespace Football_Fantasy.Presentation;

public class Signup
{
    public static object signup(OTP user)
    {
        if (BusinessSignUp.CheckUser(user.email, user.username) == "email")
        {
            return new
            {
                status = "Fail",
                massage = "This email has already been selected"
            };
        }
        if (BusinessSignUp.CheckUser(user.email, user.username) == "username")
        {
            return new
            {
                status = "Fail",
                massage = "This username has already been selected"
            };
        }
        if (BusinessSignUp.CheckUser(user.email, user.username) == "null")
        {
            BusinessSignUp.SaveData(user);
            return new
            {
                status = "OK",
                massage = "Please verfiy your account.",
                token = BusinessSignUp.generatetokenforotp(user.email)
            };
        }

        return new
        {
            status = "Error",
            massage = "There is a server side error"
        };
    }

    public static object readtoken(string token)
    {
        if (BusinessSignUp.IsTokenOK(token))
        {
            return new
            {
                status = "OK",
                massage = BusinessSignUp.ReadToken(token),
                code = BusinessOtp.SecurityCode()
            };
        }
        else
        {
            return new
            {
                status = "Fail"
            };
        }
            
    }
    
}