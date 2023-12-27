namespace Flux
{
  public static partial class Reflection
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
  }
}
