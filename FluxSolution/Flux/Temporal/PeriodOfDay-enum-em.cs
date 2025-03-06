namespace Flux
{
  public static partial class Fx
  {
    public static Temporal.PeriodOfDay GetPeriodOfDay(this System.TimeOnly source)
      => ((source.Hour == 23 && source.Minute > 47) || (source.Hour == 00 && source.Minute < 13))
      ? Temporal.PeriodOfDay.Midnight
      : ((source.Hour == 00 && source.Minute > 47) || (source.Hour == 12 && source.Minute < 13))
      ? Temporal.PeriodOfDay.Noon
      : (source.Hour >= 3 && source.Hour < 12)
      ? Temporal.PeriodOfDay.Morning
      : (source.Hour >= 12 && source.Hour < 17)
      ? Temporal.PeriodOfDay.Afternoon
      : (source.Hour >= 17 && source.Hour < 21)
      ? Temporal.PeriodOfDay.Evening
      : (source.Hour >= 21 || source.Hour < 6)
      ? Temporal.PeriodOfDay.Night
      : (source.Hour >= 6 || source.Hour < 18)
      ? Temporal.PeriodOfDay.Day
      : throw new System.NotImplementedException();

    public static Temporal.PeriodOfDay GetPeriodOfDay(this System.DateTime source)
      => System.TimeOnly.FromDateTime(source).GetPeriodOfDay();
  }
}
