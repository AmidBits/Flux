namespace Flux
{
  public interface ISymmetricTranscipherable
    : ISymmetricDecryptable, ISymmetricEncryptable
  {
    ///// <summary>
    ///// <summary>Attempts to decompress the <paramref name="sourceBytes"/> to <paramref name="resultBytes"/>.</summary>
    ///// </summary>
    ///// <param name="sourceBytes">The bytes to decompress.</param>
    ///// <param name="resultBytes">The decompressed bytes.</param>
    ///// <returns></returns>
    //bool TryDecrypt(byte[] sourceBytes, string key, string salt, out byte[] resultBytes)
    //{
    //  try
    //  {
    //    using var inputStream = new System.IO.MemoryStream(sourceBytes);
    //    using var outputStream = new System.IO.MemoryStream();

    //    Decrypt(inputStream, key, salt, outputStream);

    //    resultBytes = outputStream.ToArray();
    //    return true;
    //  }
    //  catch { }

    //  resultBytes = default!;
    //  return false;
    //}

    ///// <summary>
    ///// <para>Attempts to compress the <paramref name="sourceBytes"/> to the <paramref name="resultBytes"/>.</para>
    ///// </summary>
    ///// <param name="sourceBytes">The bytes to compress.</param>
    ///// <param name="resultBytes">The compressed bytes.</param>
    ///// <returns></returns>
    //bool TryEncrypt(byte[] sourceBytes, string key, string salt, out byte[] resultBytes)
    //{
    //  try
    //  {
    //    using var inputStream = new System.IO.MemoryStream(sourceBytes);
    //    using var outputStream = new System.IO.MemoryStream();

    //    Encrypt(inputStream, key, salt, outputStream);

    //    resultBytes = outputStream.ToArray();
    //    return true;
    //  }
    //  catch { }

    //  resultBytes = default!;
    //  return false;
    //}

    ///// <summary>
    ///// <para>Attempts to decompress <paramref name="sourceString"/> (encoded as Base64) to <paramref name="resultString"/>.</para>
    ///// </summary>
    ///// <param name="sourceString">The characters to decompress (encoded as Base64).</param>
    ///// <param name="resultString">The decompressed characters.</param>
    ///// <returns></returns>
    //bool TryDecrypt(string sourceString, string key, string salt, out string resultString)
    //{
    //  try
    //  {
    //    var compressedBytes = System.Convert.FromBase64String(sourceString);

    //    if (TryDecrypt(compressedBytes, key, salt, out var decompressedBytes))
    //    {
    //      resultString = System.Text.Encoding.UTF8.GetString(decompressedBytes);
    //      return true;
    //    }
    //  }
    //  catch { }

    //  resultString = default!;
    //  return false;
    //}

    ///// <summary>
    ///// <para>Attempts to compress <paramref name="sourceChars"/> to <paramref name="resultString"/> (encoded as Base64).</para>
    ///// </summary>
    ///// <param name="sourceChars">The characters to compress.</param>
    ///// <param name="resultString">The compressed characters (encoded as Base64).</param>
    ///// <returns></returns>
    //bool TryEncrypt(System.ReadOnlySpan<char> sourceChars, out string resultString, string key, string salt)
    //{
    //  try
    //  {
    //    var sourceBytes = new byte[System.Text.Encoding.UTF8.GetByteCount(sourceChars)];

    //    System.Text.Encoding.UTF8.GetBytes(sourceChars, sourceBytes);

    //    if (TryEncrypt(sourceBytes, key, salt, out var compressedBytes))
    //    {
    //      resultString = System.Convert.ToBase64String(compressedBytes);
    //      return true;
    //    }
    //  }
    //  catch { }

    //  resultString = default!;
    //  return false;
    //}
  }
}
