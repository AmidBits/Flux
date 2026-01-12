namespace Flux.Cipher.Symmetric
{
  public sealed class Rc2
    : ISymmetricCipher
  {
    public static ISymmetricCipher Default { get; } = new Rc2();

    public Rc2()
    {
      using var algorithm = System.Security.Cryptography.RC2.Create();

      DefaultLegalBlockSize = algorithm.BlockSize / 8;
      DefaultLegalKeySize = algorithm.KeySize / 8;

      MaxLegalBlockSize = algorithm.LegalBlockSizes.Max(s => s.MaxSize) / 8;
      MaxLegalKeySize = algorithm.LegalKeySizes.Max(s => s.MaxSize) / 8;
    }

    public int DefaultLegalBlockSize { get; }
    public int DefaultLegalKeySize { get; }

    public int MaxLegalBlockSize { get; }
    public int MaxLegalKeySize { get; }

    public void Decrypt(System.IO.Stream source, byte[] key, byte[] iv, System.IO.Stream target)
    {
      using var algorithm = System.Security.Cryptography.RC2.Create();

      algorithm.KeySize = key.Length * 8;
      algorithm.Key = key;

      algorithm.BlockSize = iv.Length * 8;
      algorithm.IV = iv;

      using var cs = new System.Security.Cryptography.CryptoStream(target, algorithm.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      source?.CopyTo(cs);

      algorithm.Clear();
    }

    public void Encrypt(System.IO.Stream source, byte[] key, byte[] iv, System.IO.Stream target)
    {
      using var algorithm = System.Security.Cryptography.RC2.Create();

      algorithm.KeySize = key.Length * 8;
      algorithm.Key = key;

      algorithm.BlockSize = iv.Length * 8;
      algorithm.IV = iv;

      using var cs = new System.Security.Cryptography.CryptoStream(target, algorithm.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

      source?.CopyTo(cs);

      algorithm.Clear();
    }
  }
}
