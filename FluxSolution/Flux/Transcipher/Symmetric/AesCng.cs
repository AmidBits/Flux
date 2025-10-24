namespace Flux.Transcipher.Symmetric
{
  public sealed class AesCng
    : ISymmetricTranscipherable
  {
    public static ISymmetricTranscipherable Default { get; } = new AesCng();

    public void Decrypt(System.IO.Stream source, byte[] key, byte[] iv, System.IO.Stream target)
    {
      using var algorithm = System.Security.Cryptography.AesCng.Create();

      /*
        using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

        algorithm.Key = pdb.GetBytes(algorithm.KeySize / 8);
        algorithm.IV = pdb.GetBytes(algorithm.BlockSize / 8);
      */

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
      using var algorithm = System.Security.Cryptography.AesCng.Create();

      /*
        using var pdb = new System.Security.Cryptography.PasswordDeriveBytes(key, System.Text.UnicodeEncoding.Unicode.GetBytes(salt));

        algorithm.Key = pdb.GetBytes(algorithm.KeySize / 8);
        algorithm.IV = pdb.GetBytes(algorithm.BlockSize / 8);
      */

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

/*
  var (key, iv) = Flux.Transcipher.Helper.GetBytes("Robs Key", "Robs Salt", 32, 16, HashAlgorithmName.SHA3_512, 9973);

  var sourceString = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure."; //"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
  var sourceLength = sourceString.Length;

  var uncryptedBytes = Flux.Transcode.Utf8.Default.DecodeString(sourceString);

  var encryptedBytes = Flux.Transcipher.Symmetric.AesCng.Default.Encrypt(uncryptedBytes, key, iv);

  var decryptedBytes = Flux.Transcipher.Symmetric.AesCng.Default.Decrypt(encryptedBytes, key, iv);

  var targetString = Flux.Transcode.Utf8.Default.EncodeString(decryptedBytes);
  var targetLength = targetString.Length;
 */

/*
  var (key, iv) = Flux.Transcipher.Helper.GetBytes("Robs Key", "Robs Salt", 16, 8, HashAlgorithmName.SHA3_512, 1000);

  var isymmetrictranscipherable = Flux.Transcipher.Symmetric.TripleDesCng.Default;
  var itranscodable = Flux.Transcode.Utf8.Default;
  var itransflexable = Flux.Transflex.Brotli.Max;

  var sourceString = "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure."; //"Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
  var sourceLength = sourceString.Length;
  var sourceBytes = itranscodable.DecodeString(sourceString);

  //System.Text.Encoding.UTF8.GetBytes(sourceString);
  //var base85 = Flux.Transcode.Base85.Default.EncodeString(sourceBytes);
  //sourceBytes = Flux.Transcode.Utf8.Default.DecodeString(base85);

  var compressedBytes = itransflexable.Compress(sourceBytes);
  var encryptedBytes = isymmetrictranscipherable.Encrypt(compressedBytes, key, iv);
  var decryptedBytes = isymmetrictranscipherable.Decrypt(encryptedBytes, key, iv);
  var decompressedBytes = itransflexable.Decompress(decryptedBytes);

  var targetString = itranscodable.EncodeString(decompressedBytes);
  var targetLength = targetString.Length;
  //var unbase85 = Flux.Transcode.Base85.Default.DecodeString(targetString);
  //var unbaseString = Flux.Transcode.Utf8.Default.EncodeString(unbase85);

  //var processed = isymmetrictranscipherable.Encrypt(itransflexable.Compress(itranscodable.DecodeString(sourceString)), key, iv); // Same as above.
 */