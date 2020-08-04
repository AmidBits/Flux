namespace Flux
{
  public static partial class XtensionsByte
  {
    /// <summary>Attempts to decrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
    public static bool TryDecrypt(this byte[] data, out byte[] result, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        input.Decrypt(output, key, salt, algorithm);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
    /// <summary>Attempts to encrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
    public static bool TryEncrypt(this byte[] data, out byte[] result, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        input.Encrypt(output, key, salt, algorithm);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
