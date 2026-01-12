namespace Flux
{
  public interface ISymmetricCipher
    : ISymmetricDecryptable, ISymmetricEncryptable
  {
    int DefaultLegalBlockSize { get; }
    int DefaultLegalKeySize { get; }

    int MaxLegalBlockSize { get; }
    int MaxLegalKeySize { get; }

    string Encrypt(string input, string password, string salt)
    {
      var (key, iv) = Cipher.Helper.GenerateKeyAndIv(password, salt, Cipher.Helper.DefaultIterations, System.Security.Cryptography.HashAlgorithmName.SHA3_512, MaxLegalKeySize, MaxLegalBlockSize);

      return Transcode.Base64.Default.EncodeString(Encrypt(System.Text.Encoding.UTF8.GetBytes(input), key, iv));
    }

    string Decrypt(string input, string password, string salt)
    {
      var (key, iv) = Cipher.Helper.GenerateKeyAndIv(password, salt, Cipher.Helper.DefaultIterations, System.Security.Cryptography.HashAlgorithmName.SHA3_512, MaxLegalKeySize, MaxLegalBlockSize);

      return System.Text.Encoding.UTF8.GetString(Decrypt(Transcode.Base64.Default.DecodeString(input), key, iv));
    }
  }
}
