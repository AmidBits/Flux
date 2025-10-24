namespace Flux.Transcode
{
  /// <summary>Base64 encoding.</summary>
  public class Base64
    : IStringTranscodable
  {
    public static IStringTranscodable Default { get; } = new Base64();

    public byte[] DecodeString(string input)
      => System.Convert.FromBase64String(input);

    public string EncodeString(byte[] input)
      => System.Convert.ToBase64String(input);
  }
}
