namespace Flux.Transcipher.Symmetric
{
  public sealed class Rc2
    : ISymmetricTranscipherable
  {
    public static ISymmetricTranscipherable Default { get; } = new Rc2();

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
