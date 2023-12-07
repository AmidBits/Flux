namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Decrypt the <paramref name="source"/> stream to the specified stream using the specified <paramref name="key"/>, <paramref name="salt"/> and symmetric <paramref name="algorithm"/> into the target <paramref name="target"/> stream. If <paramref name="algorithm"/> is null then <see cref="System.Security.Cryptography.Aes"/> is used.</summary>
    public static void Decrypt(this System.IO.Stream source, System.IO.Stream target, string key, string salt, System.Security.Cryptography.SymmetricAlgorithm algorithm)
    {
      System.ArgumentNullException.ThrowIfNull(algorithm);

      using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

      algorithm.Key = pdb.GetBytes(algorithm.KeySize / 8);
      algorithm.IV = pdb.GetBytes(algorithm.BlockSize / 8);

      using var cs = new System.Security.Cryptography.CryptoStream(target, algorithm.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      source?.CopyTo(cs);
    }

    /// <summary>Encrypt the <paramref name="source"/> stream to the specified stream using the specified <paramref name="key"/>, <paramref name="salt"/> and symmetric <paramref name="algorithm"/> into the target <paramref name="target"/> stream. If <paramref name="algorithm"/> is null then <see cref="System.Security.Cryptography.Aes"/> is used.</summary>
    public static void Encrypt(this System.IO.Stream source, System.IO.Stream target, string key, string salt, System.Security.Cryptography.SymmetricAlgorithm algorithm)
    {
      System.ArgumentNullException.ThrowIfNull(algorithm);

      using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

      algorithm.Key = pdb.GetBytes(algorithm.KeySize / 8);
      algorithm.IV = pdb.GetBytes(algorithm.BlockSize / 8);

      using var cs = new System.Security.Cryptography.CryptoStream(target, algorithm.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      source?.CopyTo(cs);
    }
  }
}
