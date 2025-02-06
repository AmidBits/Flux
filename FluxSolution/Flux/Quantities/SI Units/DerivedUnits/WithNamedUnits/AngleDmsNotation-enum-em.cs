namespace Flux
{
  public static partial class Em
  {
    public static string GetAcronym(this Quantities.AngleDmsNotation format)
    => format switch
    {
      Quantities.AngleDmsNotation.DecimalDegrees => "D",
      Quantities.AngleDmsNotation.DegreesDecimalMinutes => "DM",
      Quantities.AngleDmsNotation.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }
}
