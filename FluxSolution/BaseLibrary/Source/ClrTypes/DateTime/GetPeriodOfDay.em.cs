namespace Flux
{
  public static partial class XtensionsDateTime
  {
    public static string GetPeriodOfDay(this System.DateTime source)
      => source.Hour switch
      {
        int hour when (hour == 23 && source.Minute > 53) || (hour == 00 && source.Minute < 07) => @"Midnight",
        int hour when (hour == 11 && source.Minute > 53) || (hour == 12 && source.Minute < 07) => @"Noon",
        int hour when hour >= 21 && source.Hour < 05 => @"Night",
        int hour when hour >= 17 => @"Evening",
        int hour when hour > 12 => @"Afternoon",
        int hour when hour < 12 => @"Morning",
        _ => string.Empty,
      };
  }
}
