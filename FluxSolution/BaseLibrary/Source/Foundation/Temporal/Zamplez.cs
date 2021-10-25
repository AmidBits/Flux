#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunTemporal()
    {
      var dt = new Flux.MomentUtc(1100, 04, 28, 13, 30, 30);
      System.Console.WriteLine($"{dt}");

      var jdn = dt.ToJulianDayNumber();
      System.Console.WriteLine($"{jdn}");
      var jd1 = jdn.ToJulianDate();
      System.Console.WriteLine($"{jd1}, {jd1.GetConversionCalendar()}");
      var dt1 = jdn.ToMomentUtc(ConversionCalendar.JulianCalendar);
      System.Console.WriteLine($"{dt1} = {dt1.ToDateTime()}");

      var jd = dt.ToJulianDate(ConversionCalendar.JulianCalendar);
      System.Console.WriteLine($"{jd}, {jd.GetConversionCalendar()}");
      var dt2 = jd.ToMomentUtc(ConversionCalendar.JulianCalendar);
      System.Console.WriteLine($"{dt2} = {dt2.ToDateTime()}");
    }
  }
}
#endif
