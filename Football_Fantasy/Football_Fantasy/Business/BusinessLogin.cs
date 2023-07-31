using Football_Fantasy.DataAccess;
namespace Football_Fantasy.Business;

public class BusinessLogin
{
    public static bool Login(string email, string password)
    {
        return DataLogin.IsThisEmailAndPasswordForAUser(email, password);

    }

    public static bool EmailExistButPasswordNotMatch(string email, string password)
    {
        if (DataLogin.IsThisEmailExist(email))
        {
            if (DataLogin.IsThisEmailAndPasswordForAUser(email, password))
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public static bool IsThisEmailExist(string email)
    {
        return DataLogin.IsThisEmailExist(email);
    }
    
}