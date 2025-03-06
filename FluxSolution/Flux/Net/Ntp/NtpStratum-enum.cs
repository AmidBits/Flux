namespace Flux.Net.Ntp
{
  /// <summary>
  /// <para><see href="https://en.wikipedia.org/wiki/Network_Time_Protocol"/></para>
  /// </summary>
  public enum NtpStratum
  {
    UnspecifiedOrInvalid = 0,
    PrimaryServer = 1,
    SecondaryServer = 2, // to 15
    Unsynchronized = 16,
    Reserved = 17, // to 255
  }
}
