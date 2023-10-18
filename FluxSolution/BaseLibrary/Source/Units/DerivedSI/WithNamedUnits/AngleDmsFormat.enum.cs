namespace Flux
{
  public static partial class UnitsExtensionMethods
  {
    public static string GetAcronymString(this Units.AngleDmsFormat format)
    => format switch
    {
      Units.AngleDmsFormat.DecimalDegrees => "D",
      Units.AngleDmsFormat.DegreesDecimalMinutes => "DM",
      Units.AngleDmsFormat.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }

  namespace Units
  {
    public enum AngleDmsFormat
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
