namespace Flux.Transcode
{
  /// <summary>Utf32 encoding.</summary>
  public class Utf32
    : IStringTranscodable
  {
    public static IStringTranscodable Default { get; } = new Utf32();

    public byte[] DecodeString(string input)
      => System.Text.Encoding.UTF32.GetBytes(input);

    public bool TryDecodeString(string input, byte[] output)
      => System.Text.Encoding.UTF32.TryGetBytes(input, output, out var bytesWritten);

    public string EncodeString(byte[] input)
      => System.Text.Encoding.UTF32.GetString(input);
  }
}
