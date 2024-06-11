using System.Security.Cryptography;
using System.Text;

namespace LogApi.Functions;

public static class EncryptPasswordFunction
{
    public static string Encrypt(this string value)
    {
        var data = Encoding.Unicode.GetBytes(value);

        var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

        return Convert.ToBase64String(encrypted);
    }

    public static string Decrypt(this string value)
    {
        var data = Convert.FromBase64String(value);
        var decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
        return Encoding.Unicode.GetString(decrypted);
    }
}