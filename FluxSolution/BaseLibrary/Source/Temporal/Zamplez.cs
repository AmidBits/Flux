#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the coordinate systems zample.</summary>
    public static void RunTemporal()
    {
      System.Console.WriteLine();
      System.Console.WriteLine(nameof(RunTemporal));
      System.Console.WriteLine();

      var dt = new Flux.MomentUtc(1100, 04, 28, 13, 30, 31);
      System.Console.WriteLine($"{dt}");

      var jdgc = dt.ToJulianDate(ConversionCalendar.GregorianCalendar);
      System.Console.WriteLine($"{jdgc.ToTimeString()}");
      var jdngc = jdgc.ToJulianDayNumber();
      System.Console.WriteLine($"{jdngc.ToDateString(ConversionCalendar.GregorianCalendar)}");
      var mugc = jdgc.ToMomentUtc(ConversionCalendar.GregorianCalendar);
      //System.Console.WriteLine($"{mugc}, {mugc.ToDateOnly()}, {mugc.ToDateTime()}, {mugc.ToTimeOnly()}, {mugc.ToTimeSpan()}");
      System.Console.WriteLine($"{mugc}, {mugc.ToDateTime()}, {mugc.ToTimeSpan()}");

      //var mugc1 = mugc with { Year = Year + 1 };

      var jdjc = dt.ToJulianDate(ConversionCalendar.JulianCalendar);
      System.Console.WriteLine($"{jdjc.ToTimeString()}");
      var jdnjc = jdjc.ToJulianDayNumber();
      System.Console.WriteLine($"{jdnjc.ToDateString(ConversionCalendar.JulianCalendar)}");
      var mujc = jdjc.ToMomentUtc(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{mujc}, {mujc.ToDateOnly()}, {mujc.ToDateTime()}, {mujc.ToTimeOnly()}, {mujc.ToTimeSpan()}");
      System.Console.WriteLine($"{mujc}, {mujc.ToDateTime()}, {mujc.ToTimeSpan()}");

      //return;

      //var jdn = dt.ToJulianDayNumber(ConversionCalendar.GregorianCalendar);
      //System.Console.WriteLine($"{jdn}");
      //var jd1 = jdn.ToJulianDate();
      //System.Console.WriteLine($"{jd1}");
      //var dt1 = jdn.ToMomentUtc(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{dt1} = {dt1.ToDateTime()}");

      //var jd = dt.ToJulianDate(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{jd}");
      //var dt2 = jd.ToMomentUtc(ConversionCalendar.JulianCalendar);
      //System.Console.WriteLine($"{dt2} = {dt2.ToDateTime()}");
    }
  }
}
#endif
