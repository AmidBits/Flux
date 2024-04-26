namespace Flux
{
  public static partial class Fx
  {
    public static PeriodOfDay GetPeriodOfDay(this System.DateTime source)
      => ((source.Hour == 23 && source.Minute > 47) || (source.Hour == 00 && source.Minute < 13))
      ? PeriodOfDay.Midnight
      : ((source.Hour == 00 && source.Minute > 47) || (source.Hour == 12 && source.Minute < 13))
      ? PeriodOfDay.Noon
      : (source.Hour >= 3 && source.Hour < 12)
      ? PeriodOfDay.Morning
      : (source.Hour >= 12 && source.Hour < 18)
      ? PeriodOfDay.Afternoon
      : (source.Hour >= 18 && source.Hour < 20)
      ? PeriodOfDay.Evening
      : (source.Hour >= 18 || source.Hour < 6)
      ? PeriodOfDay.Night
      : (source.Hour >= 6 || source.Hour < 18)
      ? PeriodOfDay.Day
      : throw new System.NotImplementedException();
  }
}
