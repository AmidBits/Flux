namespace Flux.Cipher
{
  public static partial class Helper
  {
    public const int DefaultIterations = 9973;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">The key string used to derive bytes for Key and IV.</param>
    /// <param name="salt">The salt string used to derive bytes for Key and IV.</param>
    /// <param name="keyLength">
    /// <para>This must be a size accepted by the intended cipher.</para>
    /// <para>E.g. <c><see cref="Symmetric.AesCng"/> = 32</c>, <c><see cref="Symmetric.Rc2"/> = 16</c>, <c><see cref="Symmetric.TripleDesCng"/> = 24</c>.</para>
    /// </param>
    /// <param name="blockLength">
    /// <para>This must be a size accepted by the intended cipher.</para>
    /// <para>E.g. <c><see cref="Symmetric.AesCng"/> = 16</c>, <c><see cref="Symmetric.Rc2"/> = 8</c>, <c><see cref="Symmetric.TripleDesCng"/> = 8</c>.</para>
    /// </param>
    /// <param name="hashAlgorithm">The algorithm used to derive bytes for the Key and IV. E.g. <c><see cref="System.Security.Cryptography.HashAlgorithmName.SHA3_512"/></c></param>
    /// <param name="iterations">The number of iteration for the operation. E.g. <c>9973</c>.</param>
    /// <returns></returns>
    public static (byte[] Key, byte[] Iv) GenerateKeyAndIv(string key, string salt, int iterations, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, int keyLength, int blockLength)
    {
      return (
        GenerateKey(key, salt, iterations, hashAlgorithm, keyLength),
        GenerateKey(string.Concat(salt.Reverse()), string.Concat(key.Reverse()), iterations >> 1, hashAlgorithm, blockLength)
      );
    }

    public static byte[] GenerateKey(string password, string salt, int iterations, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, int keyLength)
    {
      System.ArgumentNullException.ThrowIfNullOrEmpty(password);
      System.ArgumentNullException.ThrowIfNullOrEmpty(salt);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(iterations);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);

      var saltKey = Transcode.Utf8.Default.DecodeString(password);
      var saltBytes = Transcode.Utf8.Default.DecodeString(salt);

      var keyBytes = System.Security.Cryptography.Rfc2898DeriveBytes.Pbkdf2(saltKey, saltBytes, iterations, hashAlgorithm, keyLength);

      return keyBytes;
    }
  }
}
