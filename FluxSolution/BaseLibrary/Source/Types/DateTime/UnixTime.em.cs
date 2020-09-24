namespace Flux
{
  public static partial class Xtensions
  {
    public static readonly System.DateTime UnixEpoch = new System.DateTime(1970, 1, 1, 0, 0, 0);

    public static System.DateTime FromUnixTimestamp(this long unixTimestamp)
      => UnixEpoch.AddSeconds(unixTimestamp);
    public static long ToUnixTimestamp(this System.DateTime source)
      => (long)(source - UnixEpoch).TotalSeconds;

    public static System.DateTime FromUnixUltraTimestamp(this long unixUltraTimestamp)
      => UnixEpoch.AddMilliseconds(unixUltraTimestamp);
    public static long ToUnixUltraTimestamp(this System.DateTime source)
      => (long)(source - UnixEpoch).TotalMilliseconds;
  }
}
