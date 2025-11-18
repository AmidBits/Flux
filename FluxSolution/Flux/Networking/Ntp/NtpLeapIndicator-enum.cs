namespace Flux.Net.Ntp
{
  /// <summary>
  /// <para><see href="https://en.wikipedia.org/wiki/Network_Time_Protocol"/></para>
  /// </summary>
  public enum NtpLeapIndicator
  {
    NoWarning = 0b00,
    LastMinuteHas61Seconds = 0b01,
    LastMinuteHas59Seconds = 0b10,
    UnknownOrClockUnsynchronized = 0x11,
  }
}
