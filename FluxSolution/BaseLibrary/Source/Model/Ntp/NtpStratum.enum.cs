namespace Flux.Net
{
  public static partial class Ntp
  {
    public static Net.NtpStratum GetStratum(int value)
    {
      return value switch
      {
        0 => Net.NtpStratum.UnspecifiedOrInvalid,
        1 => Net.NtpStratum.PrimaryServer,
        >= 2 and <= 15 => Net.NtpStratum.SecondaryServer,
        16 => Net.NtpStratum.Unsynchronized,
        >= 17 => Net.NtpStratum.Reserved,
        _ => throw new System.ArgumentOutOfRangeException(nameof(value))
      };
    }
  }

  /// <see href="https://en.wikipedia.org/wiki/Network_Time_Protocol"/>
  public enum NtpStratum
  {
    UnspecifiedOrInvalid = 0,
    PrimaryServer = 1,
    SecondaryServer = 2, // to 15
    Unsynchronized = 16,
    Reserved = 17, // to 255
  }
}
