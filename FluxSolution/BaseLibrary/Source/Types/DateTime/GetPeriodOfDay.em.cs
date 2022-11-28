namespace Flux
{
  public enum PeriodOfDay
  {
    Unknown,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Afternoon"/>
    /// </summary>
    Afternoon,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Day#Daytime"/>
    /// </summary>
    Day,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Evening"/>
    /// </summary>
    Evening,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Midnight"/>
    /// </summary>
    Midnight,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Morning"/>
    /// </summary>
    Morning,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Night"/>
    /// </summary>
    Night,
    /// <summary>
    /// <para></para>
    /// <see href="https://en.wikipedia.org/wiki/Noon"/>
    /// </summary>
    Noon,
  }

  public static partial class DateTimeEm
  {
    public static PeriodOfDay GetPeriodOfDay(this System.DateTime source)
      => IsMidnight(source) ? PeriodOfDay.Midnight
      : IsNoon(source) ? PeriodOfDay.Noon
      : IsMorning(source) ? PeriodOfDay.Morning
      : IsAfternoon(source) ? PeriodOfDay.Afternoon
      : IsEvening(source) ? PeriodOfDay.Evening
      : IsNightTime(source) ? PeriodOfDay.Night
      : IsDaytime(source) ? PeriodOfDay.Day
      : throw new System.NotImplementedException();

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Afternoon"/>
    public static bool IsAfternoon(this System.DateTime source)
      => source.Hour >= 12 && source.Hour < 18;

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Day#Daytime"/>
    public static bool IsDaytime(this System.DateTime source)
      => source.Hour >= 6 || source.Hour < 18;

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Evening"/>
    public static bool IsEvening(this System.DateTime source)
      => source.Hour >= 18 && source.Hour < 20;

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Midnight"/>
    public static bool IsMidnight(this System.DateTime source)
      => (source.Hour == 23 && source.Minute > 47) || (source.Hour == 00 && source.Minute < 13);

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Morning"/>
    public static bool IsMorning(this System.DateTime source)
      => source.Hour >= 3 && source.Hour < 12;

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Night"/>
    public static bool IsNightTime(this System.DateTime source)
      => source.Hour >= 18 || source.Hour < 6;

    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/Noon"/>
    public static bool IsNoon(this System.DateTime source)
      => (source.Hour == 00 && source.Minute > 47) || (source.Hour == 12 && source.Minute < 13);
  }
}
