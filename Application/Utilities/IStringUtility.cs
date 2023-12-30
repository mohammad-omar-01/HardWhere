namespace Application.Utilities
{
    public interface IStringUtility
    {
        string HashString(string str);
        bool VerifyEquailityForTwoPasswords(string str1, string str2);
        string NicenName(string fName, string lName);
    }
}
