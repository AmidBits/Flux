namespace Flux
{
  public static partial class Fx
  {
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
