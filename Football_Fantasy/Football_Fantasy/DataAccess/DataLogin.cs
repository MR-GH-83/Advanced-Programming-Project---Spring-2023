namespace Football_Fantasy.DataAccess;

public class DataLogin
{
    public static bool IsThisEmailAndPasswordForAUser(string email, string password)
    {
        using (var db = new Database())
        {
            foreach (var dbUser in db.Users)
            {
                if (dbUser.email == email)
                {
                    if (dbUser.password == password)
                        return true;
                }
            }
        }

        return false;
    }

    public static bool IsThisEmailExist(string email)
    {
        using (var db = new Database())
        {
            foreach (var dbUser in db.Users)
            {
                if ((dbUser.email == email))
                    return true;
            }
        }

        return false;
    }
    
    
    
}