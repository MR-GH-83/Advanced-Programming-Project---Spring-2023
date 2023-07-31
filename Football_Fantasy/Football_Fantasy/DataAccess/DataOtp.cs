namespace Football_Fantasy.DataAccess;

public class DataOtp
{
    public static string TheLastEmailInOtp()
    {
        using (var db = new Database())
        {
            int maxId = 0;
            foreach (var dbOtp in db.Otps)
            {
                
                if (dbOtp.primaryKey > maxId)
                {
                    maxId = dbOtp.primaryKey;
                }
            }

            foreach (var dbOtp in db.Otps)
            {
                if (maxId == dbOtp.primaryKey)
                    return dbOtp.email;
            }
            
        }

        throw new Exception();
        return "";
    }
    
    public static bool ThisEmailIsExist(string email)
    {
        using (var db = new Database())
        {
            foreach (var dbOtp in db.Otps)
            {
                if (dbOtp.email == email)
                    return true;
            }
            
        }

        return false;
    }

    public static OTP? ReturnOtpObjectBySearchEmail(string email)
    {
        using (var db = new Database())
        {
            foreach (var dbOtp in db.Otps)
            {
                if (dbOtp.email == email)
                    return dbOtp;
            }
            
        }
        return null;
    }

    public static void SaveDataToUser(User user)
    {
        using (var db =new Database())
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
    
    
}