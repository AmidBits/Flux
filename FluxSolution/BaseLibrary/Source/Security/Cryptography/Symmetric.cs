namespace Flux.Cryptography
{
  public static class Symmetric
  {
    /// <summary>Decrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
    //public static void Decrypt(this System.IO.Stream input, System.IO.Stream output, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    //{
    //  using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

    //  using var algo = System.Security.Cryptography.SymmetricAlgorithm.Create(algorithm);

    //  algo.Key = pdb.GetBytes(algo.KeySize / 8);
    //  algo.IV = pdb.GetBytes(algo.BlockSize / 8);

    //  using var cs = new System.Security.Cryptography.CryptoStream(output, algo.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

    //  input?.CopyTo(cs); // input.WriteTo(cs);
    //}
    /// <summary>Encrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
    //public static void Encrypt(this System.IO.Stream input, System.IO.Stream output, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    //{
    //  using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

    //  using var algo = System.Security.Cryptography.SymmetricAlgorithm.Create(algorithm);

    //  algo.Key = pdb.GetBytes(algo.KeySize / 8);
    //  algo.IV = pdb.GetBytes(algo.BlockSize / 8);

    //  using var cs = new System.Security.Cryptography.CryptoStream(output, algo.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

    //  input?.CopyTo(cs);
    //}

    /// <summary>Attempts to decrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
    //public static bool TryDecrypt(this byte[] data, out byte[] result, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    //{
    //  try
    //  {
    //    using var input = new System.IO.MemoryStream(data);
    //    using var output = new System.IO.MemoryStream();
    //    input.Decrypt(output, key, salt, algorithm);
    //    result = output.ToArray();
    //    return true;
    //  }
    //  catch { }

    //  result = default!;
    //  return false;
    //}
    /// <summary>Attempts to encrypt the source byte array to the output byte array with the specified key, salt and symmetric algorithm.</summary>
    //public static bool TryEncrypt(this byte[] data, out byte[] result, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    //{
    //  try
    //  {
    //    using var input = new System.IO.MemoryStream(data);
    //    using var output = new System.IO.MemoryStream();
    //    input.Encrypt(output, key, salt, algorithm);
    //    result = output.ToArray();
    //    return true;
    //  }
    //  catch { }

    //  result = default!;
    //  return false;
    //}

    /// <summary>Attempts to decrypt the source string (base64) to the output string with the specified key, salt and symmetric algorithm.</summary>
    //public static bool TryDecrypt(this string data, out string result, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    //{
    //  try
    //  {
    //    if (System.Convert.FromBase64String(data).TryDecrypt(out var bytes, key, salt, algorithm))
    //    {
    //      result = System.Text.Encoding.UTF8.GetString(bytes);
    //      return true;
    //    }
    //  }
    //  catch { }

    //  result = default!;
    //  return false;
    //}
    /// <summary>Attempts to encrypt the source string to the output string (base64) with the specified key, salt and symmetric algorithm.</summary>
    //public static bool TryEncrypt(this string data, out string result, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    //{
    //  try
    //  {
    //    if (System.Text.Encoding.UTF8.GetBytes(data).TryEncrypt(out var bytes, key, salt, algorithm))
    //    {
    //      result = System.Convert.ToBase64String(bytes);
    //      return true;
    //    }
    //  }
    //  catch { }

    //  result = default!;
    //  return false;
    //}
  }
}
