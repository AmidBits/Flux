namespace Flux
{
  public static partial class Em
  {
    public static string GetAcronym(this AngleDmsNotation format)
    => format switch
    {
      AngleDmsNotation.DecimalDegrees => "D",
      AngleDmsNotation.DegreesDecimalMinutes => "DM",
      AngleDmsNotation.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }

  /// <summary>
  /// <para>Angle DMS notation is a way to represent latitude or longitude.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_degrees"/></para>
  /// </summary>
  public enum AngleDmsNotation
  {
    /// <summary>A.k.a. "D" notation.</summary>
    DecimalDegrees,
    /// <summary>A.k.a. "DM" notation.</summary>
    DegreesDecimalMinutes,
    /// <summary>A.k.a. "DMS" notation.</summary>
    DegreesMinutesDecimalSeconds
  }
}
