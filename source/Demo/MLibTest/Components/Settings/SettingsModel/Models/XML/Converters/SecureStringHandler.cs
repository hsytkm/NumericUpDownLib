using System.Runtime.InteropServices;
using System.Security;

namespace SettingsModel.Models.XML.Converters;

/// <summary>
/// Source of string encryption and decryption:
/// http://weblogs.asp.net/jongalloway//encrypting-passwords-in-a-net-app-config-file
/// </summary>
internal class SecureStringHandler : IAlternativeDataTypeHandler
{
    private static readonly byte[] _entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Usually Not A Password");

    /// <summary>
    /// Gets the type of the original data type that is to be replaced
    /// with an alternative (typed) representation.
    /// </summary>
    public Type SourceDataType => typeof(SecureString);

    /// <summary>
    /// Gets the type of the target data type that is to be used
    /// instead of the original (typed) representation.
    /// </summary>
    public Type TargetDataType => typeof(string);

    /// <summary>
    /// Converts from the source datatype into the target data type representation.
    /// </summary>
    /// <param name="objectInput"></param>
    /// <returns></returns>
    public object? Convert(object? objectInput)
    {
        if (objectInput is not SecureString input)
            return null;

        byte[]? encryptedData = null;
        try
        {
            encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                _entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);

            return System.Convert.ToBase64String(encryptedData);
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (encryptedData != null)
            {
                for (int i = 0; i < encryptedData.Length; i++)
                {
                    encryptedData[i] = 0;
                }
            }
        }
    }

    /// <summary>
    /// Converts from the target datatype into the source data type representation.
    /// </summary>
    /// <param name="objectEncryptedData"></param>
    /// <returns></returns>
    public object? ConvertBack(object? objectEncryptedData)
    {
        if (objectEncryptedData is not string encryptedData)
            return null;

        byte[]? decryptedData = null;
        try
        {
            decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                System.Convert.FromBase64String(encryptedData),
                _entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);

            return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
        }
        catch
        {
            return new SecureString();
        }
        finally
        {
            if (decryptedData != null)
            {
                for (int i = 0; i < decryptedData.Length; i++)
                {
                    decryptedData[i] = 0;
                }
            }
        }
    }

    private SecureString ToSecureString(string input)
    {
        var secure = new SecureString();
        foreach (char c in input)
        {
            secure.AppendChar(c);
        }
        secure.MakeReadOnly();
        return secure;
    }

    private string ToInsecureString(SecureString input)
    {
        string returnValue = "";
        IntPtr ptr = Marshal.SecureStringToBSTR(input);
        try
        {
            returnValue = Marshal.PtrToStringBSTR(ptr);
        }
        finally
        {
            Marshal.ZeroFreeBSTR(ptr);
        }
        return returnValue;
    }
}
