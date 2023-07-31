using Football_Fantasy.DataAccess;
namespace Football_Fantasy.Business;

public class BusinessSignUp
{
    public static string CheckUser(string email, string username)
    {
        if (DataSignUp.IsUserExist(email, username))
        {
            if (DataSignUp.IsEmialHasBeenTaken(email))
                return "email";
            if (DataSignUp.IsUsernameHasBeenTaken(username))
                return "username";
        }

        return "null";

    }

    public static string generatetokenforotp(string email)
    {
       string result = SubClasses.Token.GenerateToken("otp", email);
       return result;
    }

    public static void SaveData(OTP user)
    {
        DataSignUp.SaveDataToOtp(user);
    }

    public static bool IsTokenOK(string token)
    {
        string email = SubClasses.Token.ReadToken("otp", token);
        return BusinessOtp.ThisEmailIsExistInOtpAndTheLastOne(email);
    }

    public static string ReadToken(string token)
    {
        return SubClasses.Token.ReadToken("otp", token);
    }
    
}