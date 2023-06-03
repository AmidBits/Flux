namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    public static string GetAcronymString(this Units.DmsFormat format)
    => format switch
    {
      Units.DmsFormat.DecimalDegrees => "D",
      Units.DmsFormat.DegreesDecimalMinutes => "DM",
      Units.DmsFormat.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }

  namespace Units
  {
    public enum DmsFormat
    {
      /// <summary>A.k.a. "D" notation.</summary>
      DecimalDegrees,
      /// <summary>A.k.a. "DM" notation.</summary>
      DegreesDecimalMinutes,
      /// <summary>A.k.a. "DMS" notation.</summary>
      DegreesMinutesDecimalSeconds
    }
  }
}
