namespace Flux.Transcode
{
  /// <summary>Utf8 encoding.</summary>
  public class Utf8
    : IStringTranscoder
  {
    public static IStringTranscoder Default { get; } = new Utf8();

    public byte[] DecodeString(string input)
      => System.Text.Encoding.UTF8.GetBytes(input);

    public string EncodeString(byte[] input)
      => System.Text.Encoding.UTF8.GetString(input);
  }
}
