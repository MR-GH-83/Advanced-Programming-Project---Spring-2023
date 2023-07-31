using Football_Fantasy.DataAccess;
namespace Football_Fantasy.Business;

public class BusinessOtp
{
    public static void SaveDataToUserTable(string email)
    {
        if(DataOtp.ReturnOtpObjectBySearchEmail(email)==null)
            new Exception("This Email is not exist.");
        
        OTP temp = DataOtp.ReturnOtpObjectBySearchEmail(email);
        User saveUser = new User();
        saveUser.name = temp.name;
        saveUser.email = temp.email;
        saveUser.username = temp.username;
        saveUser.password = temp.password;
        DataOtp.SaveDataToUser(saveUser);
    }
    public static string SecurityCode()
    {
        Random rnd = new Random();
        int code = rnd.Next(1000,10000);
        return Convert.ToString(code);
    }
    
    public static bool ThisEmailIsExistInOtpAndTheLastOne(string email)
    {

        if (DataOtp.TheLastEmailInOtp() == email && DataOtp.ThisEmailIsExist(email))
            return true;
        return false;
    }
    
}