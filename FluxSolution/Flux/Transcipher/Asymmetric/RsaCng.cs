namespace Flux.Transcipher.Asymmetric
{
  public sealed class RsaCng
    : IAsymmetricTranscipherable
  {
    public static IAsymmetricTranscipherable Default { get; } = new RsaCng();

    public byte[] Decrypt(byte[] sourceBytes, byte[] rsaPrivateKey)
    {
      using var algorithm = System.Security.Cryptography.RSACng.Create();

      algorithm.ImportRSAPrivateKey(rsaPrivateKey, out var bytesRead);

      var targetBytes = algorithm.Decrypt(sourceBytes, System.Security.Cryptography.RSAEncryptionPadding.OaepSHA3_512);

      algorithm.Clear();

      return targetBytes;
    }

    public byte[] Encrypt(byte[] sourceBytes, byte[] rsaPublicKey)
    {
      using var algorithm = System.Security.Cryptography.RSACng.Create();

      algorithm.ImportRSAPublicKey(rsaPublicKey, out var bytesRead);

      var targetBytes = algorithm.Encrypt(sourceBytes, System.Security.Cryptography.RSAEncryptionPadding.OaepSHA3_512);

      algorithm.Clear();

      return targetBytes;
    }

    #region Static methods

    public static (byte[] PrivateKey, string PrivateText, byte[] PublicKey, string PublicText) CreateRsaKeyPair(int keySizeInBits, int splitLineLength = 64)
    {
      using var rsa = System.Security.Cryptography.RSA.Create(keySizeInBits);

      var privateKey = rsa.ExportRSAPrivateKey();

      var privateB64 = Flux.Transcode.Base64.Default.EncodeString(privateKey);

      var privateLines = privateB64.Length.CreateSubRanges(splitLineLength).Select(r => privateB64[r]).ToList();

      privateLines.Insert(0, "-----BEGIN RSA PRIVATE KEY-----");
      privateLines.Add("-----END RSA PRIVATE KEY-----");

      var privateText = string.Join('\n', privateLines);

      var publicKey = rsa.ExportRSAPublicKey();

      var publicB64 = Flux.Transcode.Base64.Default.EncodeString(publicKey);

      var publicLines = publicB64.Length.CreateSubRanges(splitLineLength).Select(r => publicB64[r]).ToList();

      publicLines.Insert(0, "-----BEGIN RSA PUBLIC KEY-----");
      publicLines.Add("-----END RSA PUBLIC KEY-----");

      var publicText = string.Join('\n', publicLines);

      return (privateKey, privateText, publicKey, publicText);
    }

    #endregion
  }
}

/*
  var rsaBitSize = 4096;

  var rsa = Flux.Transcipher.Asymmetric.RsaCng.CreateRsaKeyPair(rsaBitSize);

  //System.IO.File.WriteAllText($"../../../../private-{rsaBitSize}.pem", rsa.PrivateText);
  //System.IO.File.WriteAllText($"../../../../public-{rsaBitSize}.pem", rsa.PublicText);

  var sourceText = "Hello";
  var sourceLength = sourceText.Length;

  var uncryptedBytes = Flux.Transcode.Utf8.Default.DecodeString(sourceText);

  var encryptedData = Flux.Transcipher.Asymmetric.RsaCng.Default.Encrypt(uncryptedBytes, rsa.PublicKey);

  var decrypedData = Flux.Transcipher.Asymmetric.RsaCng.Default.Decrypt(encryptedData, rsa.PrivateKey);

  var targetText = Flux.Transcode.Utf8.Default.EncodeString(decrypedData);
  var targetLength = targetString.Length;
 */