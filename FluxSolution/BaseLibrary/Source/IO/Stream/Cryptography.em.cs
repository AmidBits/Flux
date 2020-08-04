namespace Flux
{
  public static partial class XtensionsStream
  {
    /// <summary>Decrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
    public static void Decrypt(this System.IO.Stream input, System.IO.Stream output, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    {
      using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

      using var algo = System.Security.Cryptography.SymmetricAlgorithm.Create(algorithm);

      algo.Key = pdb.GetBytes(algo.KeySize / 8);
      algo.IV = pdb.GetBytes(algo.BlockSize / 8);

      using var cs = new System.Security.Cryptography.CryptoStream(output, algo.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      input?.CopyTo(cs); // input.WriteTo(cs);
    }
    /// <summary>Encrypt the source stream to the specified stream using the specified key, salt and symmetric algorithm.</summary>
    public static void Encrypt(this System.IO.Stream input, System.IO.Stream output, string key, string salt, string algorithm = nameof(System.Security.Cryptography.Rijndael))
    {
      using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

      using var algo = System.Security.Cryptography.SymmetricAlgorithm.Create(algorithm);

      algo.Key = pdb.GetBytes(algo.KeySize / 8);
      algo.IV = pdb.GetBytes(algo.BlockSize / 8);

      using var cs = new System.Security.Cryptography.CryptoStream(output, algo.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      input?.CopyTo(cs);
    }
  }
}
