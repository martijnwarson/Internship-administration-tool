namespace WebApplication.Helpers
{
    public interface IPasswordGenerator
    {
        /// <summary>
        ///     Generate A random password with the specified Length
        /// </summary>
        /// <returns>a string containing the password</returns>
        string CreateRandomPassword();
    }
}