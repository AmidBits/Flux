namespace Flux.Transcipher
{
  public static partial class Helper
  {
    /// <summary>
    /// <para>Sets the <see cref="System.Security.Cryptography.SymmetricAlgorithm.Key"/> property by <paramref name="encoding"/> the <paramref name="key"/> string into bytes.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static System.Security.Cryptography.SymmetricAlgorithm SetKey(this System.Security.Cryptography.SymmetricAlgorithm source, string key, System.Text.Encoding? encoding = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(key);

      encoding ??= System.Text.Encoding.UTF8;

      var bytes = encoding.GetBytes(key);

      var buffer = new byte[source.KeySize / 8];

      System.Array.Clear(buffer);
      System.Array.Copy(bytes, buffer, key.Length); // Copy to buffer, e.g. "ABCDEF---"
      System.Array.Reverse(buffer); // Reverse the buffer, e.g. "---FEDCBA"
      System.Array.Copy(bytes, buffer, key.Length); // Copy to buffer, e.g. "ABCDEFCBA"

      source.Key = buffer; // Set the actual Key property.

      return source;
    }

    /// <summary>
    /// <para>Sets the <see cref="System.Security.Cryptography.SymmetricAlgorithm.IV"/> property by <paramref name="encoding"/> the <paramref name="iv"/> string into bytes.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="iv"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static System.Security.Cryptography.SymmetricAlgorithm SetIV(this System.Security.Cryptography.SymmetricAlgorithm source, string iv, System.Text.Encoding? encoding = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(iv);

      encoding ??= System.Text.Encoding.UTF8;

      var bytes = encoding.GetBytes(iv);

      var buffer = new byte[source.KeySize / 8];

      System.Array.Clear(buffer);
      System.Array.Copy(bytes, buffer, iv.Length); // Copy to buffer, e.g. "ABCDEF---"
      System.Array.Reverse(buffer); // Reverse the buffer, e.g. "---FEDCBA"
      System.Array.Copy(bytes, buffer, iv.Length); // Copy to buffer, e.g. "ABCDEFCBA"

      source.IV = buffer; // Set the actual IV property.

      return source;
    }

    /// <summary>Decrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
    public static void Decrypt(this System.Security.Cryptography.SymmetricAlgorithm source, System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var cs = new System.Security.Cryptography.CryptoStream(output, source.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      input?.CopyTo(cs);
    }

    /// <summary>Encrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
    public static void Encrypt(this System.Security.Cryptography.SymmetricAlgorithm source, System.IO.Stream input, System.IO.Stream output)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var cs = new System.Security.Cryptography.CryptoStream(output, source.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      input?.CopyTo(cs);
    }

    /// <summary>Attempts to decrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
    public static bool TryDecrypt(this System.Security.Cryptography.SymmetricAlgorithm source, byte[] data, out byte[] result)
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        Decrypt(source, input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Attempts to encrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
    public static bool TryEncrypt(this System.Security.Cryptography.SymmetricAlgorithm source, byte[] data, out byte[] result)
    {
      try
      {
        using var input = new System.IO.MemoryStream(data);
        using var output = new System.IO.MemoryStream();
        Encrypt(source, input, output);
        result = output.ToArray();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Attempts to decrypt the source string (base64) to the output string with the specified key, salt and symmetric algorithm.</summary>
    public static bool TryDecrypt(this System.Security.Cryptography.SymmetricAlgorithm source, string base64, out string text)
    {
      try
      {
        if (TryDecrypt(source, System.Convert.FromBase64String(base64), out var output))
        {
          text = System.Text.Encoding.UTF8.GetString(output);
          return true;
        }
      }
      catch { }

      text = default!;
      return false;
    }

    /// <summary>Attempts to encrypt the source string to the output string (base64) with the specified key, salt and symmetric algorithm.</summary>
    public static bool TryEncrypt(this System.Security.Cryptography.SymmetricAlgorithm source, string text, out string base64)
    {
      try
      {
        if (TryEncrypt(source, System.Text.Encoding.UTF8.GetBytes(text), out var output))
        {
          base64 = System.Convert.ToBase64String(output);
          return true;
        }
      }
      catch { }

      base64 = default!;
      return false;
    }
  }
}
