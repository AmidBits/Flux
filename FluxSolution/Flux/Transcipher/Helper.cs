namespace Flux.Transcipher
{
  public static class Helper
  {
    public const int DefaultIterations = 9973;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">The key string used to derive bytes for Key and IV.</param>
    /// <param name="salt">The salt string used to derive bytes for Key and IV.</param>
    /// <param name="requestSizeKey">
    /// <para>This must be a size accepted by the intended cipher.</para>
    /// <para>E.g. <c><see cref="Symmetric.AesCng"/> = 32</c>, <c><see cref="Symmetric.Rc2"/> = 16</c>, <c><see cref="Symmetric.TripleDesCng"/> = 24</c>.</para>
    /// </param>
    /// <param name="requestSizeIV">
    /// <para>This must be a size accepted by the intended cipher.</para>
    /// <para>E.g. <c><see cref="Symmetric.AesCng"/> = 16</c>, <c><see cref="Symmetric.Rc2"/> = 8</c>, <c><see cref="Symmetric.TripleDesCng"/> = 8</c>.</para>
    /// </param>
    /// <param name="hashAlgorithm">The algorithm used to derive bytes for the Key and IV. E.g. <c><see cref="System.Security.Cryptography.HashAlgorithmName.SHA3_512"/></c></param>
    /// <param name="iterations">The number of iteration for the operation. E.g. <c>9973</c>.</param>
    /// <returns></returns>
    public static (byte[] Key, byte[] IV) GetBytes(string key, string salt, int requestSizeKey, int requestSizeIV, System.Security.Cryptography.HashAlgorithmName hashAlgorithm, int iterations)
    {
      var saltBytes = Transcode.Utf8.Default.DecodeString(salt);

      //using (var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, saltBytes, "SHA1", 1000))
      //  return (pdb.GetBytes(requestSizeKey), pdb.GetBytes(requestSizeIV));

      using (var db = new System.Security.Cryptography.Rfc2898DeriveBytes(key, saltBytes, iterations, hashAlgorithm))
        return (db.GetBytes(requestSizeKey), db.GetBytes(requestSizeIV));
    }
  }
}
