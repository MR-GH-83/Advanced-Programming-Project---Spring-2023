namespace Football_Fantasy.DataAccess;

public class DataSignUp
{
    public static bool IsUserExist(string email, string username)
    {
        using (var db = new Database())
        {
            foreach (var dbUser in db.Users)
            {
                if (dbUser.email == email || dbUser.username == username)
                {
                    return true;
                }

            }

            return false;
        }
    }

    public static bool IsEmialHasBeenTaken(string email)
    {
        using (var db= new Database())
        {
            foreach (var dbUser in db.Users)
            {
                if (dbUser.email == email)
                    return true;
            }

            return false;
        }
    }
    public static bool IsUsernameHasBeenTaken(string username)
    {
        using (var db= new Database())
        {
            foreach (var dbUser in db.Users)
            {
                if (dbUser.email == username)
                    return true;
            }

            return false;
        }
    }
    public static void SaveDataToOtp(OTP user)
    {
        using (var db = new Database())
        {
            db.Otps.Add(user);
            db.SaveChanges();
        }
    }

     
    

}