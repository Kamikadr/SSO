using System.Security.Cryptography;

namespace SSO.Helpers;

public static class SaltGenerator
{
    public static byte[] GenerateSalt(int length = default)
    {
        if (length == default)
        {
            length = RandomNumberGenerator.GetInt32(5, 20);
        }
        
        var salt = new byte[length];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }
}