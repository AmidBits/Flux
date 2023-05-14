namespace Flux2
{
  //public static partial class MomentUtcExtensionMethods
  //{
  //  /// <summary>Returns the approximate number of computed seconds for the instance pro-rata rate. This is by not an exact measurement and used only to compare two instances.</summary>
  //  public static double GetTotalApproximateSeconds(this MomentTime source)
  //    => System.Math.CopySign(System.Math.Abs(source.Year) * 31536000L + source.Month * 2628000L + source.Day * 86400L + source.Hour * 3600L + source.Minute * 60L + source.Second + source.Millisecond / 1000.0, source.Year);

  //  public static MomentTime ToMomentUtc(this System.DateTime source)
  //    => new(source.Year, source.Month, source.Day, source.Hour, source.Minute, source.Second, source.Millisecond);
  //}

  /// <summary>A moment is a specific point in time down to the millisecond.</summary>
  public readonly record struct MomentTime
    : System.IComparable<MomentTime>
  {
    private readonly byte m_hour;
    private readonly byte m_minute;
    private readonly byte m_second;
    private readonly int m_millisecond;

    public MomentTime(int hour, int minute, int second, int millisecond)
    {
      m_hour = hour >= 0 && hour < 24 ? (byte)hour : throw new System.ArgumentOutOfRangeException(nameof(hour));
      m_minute = minute >= 0 && minute <= 59 ? (byte)minute : throw new System.ArgumentOutOfRangeException(nameof(minute));
      m_second = second >= 0 && second <= 59 ? (byte)second : throw new System.ArgumentOutOfRangeException(nameof(second));
      m_millisecond = millisecond >= 0 && millisecond <= 999 ? millisecond : throw new System.ArgumentOutOfRangeException(nameof(millisecond));
    }

    public int Hour => m_hour;
    public int Minute => m_minute;
    public int Second => m_second;
    public int Millisecond => m_millisecond;

    #region Overloaded operators
    public static bool operator <(MomentTime a, MomentTime b) => a.CompareTo(b) < 0;
    public static bool operator <=(MomentTime a, MomentTime b) => a.CompareTo(b) <= 0;
    public static bool operator >(MomentTime a, MomentTime b) => a.CompareTo(b) > 0;
    public static bool operator >=(MomentTime a, MomentTime b) => a.CompareTo(b) >= 0;
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable<>
    public int CompareTo(MomentTime other)
      => m_hour < other.m_hour ? -1 : m_hour > other.m_hour ? 1
      : m_minute < other.m_minute ? -1 : m_minute > other.m_minute ? 1
      : m_second < other.m_second ? -1 : m_second > other.m_second ? 1
      : m_millisecond < other.m_millisecond ? -1 : m_millisecond > other.m_millisecond ? 1
      : 0; // This means this instance is equal to the other.

    #endregion Implemented interfaces
  }
}
