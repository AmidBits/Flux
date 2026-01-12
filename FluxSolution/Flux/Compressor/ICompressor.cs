namespace Flux
{
  public interface ICompressor
    : ICompressable, IDecompressable
  {
    string Compress(string input)
    {
      var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);

      var outputBytes = Compress(inputBytes);

      var outputString = Transcode.Base64.Default.EncodeString(outputBytes);

      return outputString;
    }

    string Decompress(string input)
    {
      var inputBytes = Transcode.Base64.Default.DecodeString(input);

      var outputBytes = Decompress(inputBytes);

      var outputString = System.Text.Encoding.UTF8.GetString(outputBytes);

      return outputString;
    }
  }
}
