namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static string GetAcronym(this Units.AngleDmsNotation format)
    => format switch
    {
      Units.AngleDmsNotation.DecimalDegrees => "D",
      Units.AngleDmsNotation.DegreesDecimalMinutes => "DM",
      Units.AngleDmsNotation.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }
}
