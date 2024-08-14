using System.Security.Cryptography;
using System.Text;

namespace SSO.Services;

public static class HashHelper
{
    public static async Task<byte[]> GetByteHashAsync(string str, byte[] salt, CancellationToken cancellationToken = default)
    {
        var strBytes = Encoding.UTF8.GetBytes(str);
        var saltedStr = new byte[str.Length + salt.Length];
        Buffer.BlockCopy(saltedStr, 0, strBytes, 0,str.Length);
        Buffer.BlockCopy(saltedStr, 0, salt, str.Length,salt.Length);
        
        using var stream = new MemoryStream(saltedStr);
        var hash = await SHA256.HashDataAsync(stream, cancellationToken);
        return hash;
    }

    public static async Task<string> GetStringHashAsync(string str, byte[] salt, CancellationToken cancellationToken = default)
    {
        var bytes = await GetByteHashAsync(str, salt, cancellationToken);
        return Encoding.UTF8.GetString(bytes);
    }
    public static async Task<string> GetStringHashAsync(string str, string salt, CancellationToken cancellationToken = default)
    {
        var byteSalt = Encoding.UTF8.GetBytes(salt);
        var bytes = await GetByteHashAsync(str, byteSalt, cancellationToken);
        return Encoding.UTF8.GetString(bytes);
    }
}