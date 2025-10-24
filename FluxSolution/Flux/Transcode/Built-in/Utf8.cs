namespace Flux.Transcode
{
  /// <summary>Utf8 encoding.</summary>
  public class Utf8
    : IStringTranscodable
  {
    public static IStringTranscodable Default { get; } = new Utf8();

    public byte[] DecodeString(string input)
      => System.Text.Encoding.UTF8.GetBytes(input);

    public bool TryDecodeString(string input, byte[] output)
      => throw new System.ArithmeticException();// System.Text.Encoding.UTF8.TryGetBytes(input, output, out var bytesWritten);

    public string EncodeString(byte[] input)
      => System.Text.Encoding.UTF8.GetString(input);
  }
}
