namespace Flux.Transcode
{
  /// <summary>Utf32 encoding.</summary>
  public class Utf32
    : IStringTranscoder
  {
    public static IStringTranscoder Default { get; } = new Utf32();

    public byte[] DecodeString(string input)
      => System.Text.Encoding.UTF32.GetBytes(input);

    public string EncodeString(byte[] input)
      => System.Text.Encoding.UTF32.GetString(input);
  }
}
