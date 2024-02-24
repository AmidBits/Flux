namespace Flux
{
  public static partial class Em
  {
    public static string GetAcronym(this DmsNotation format)
    => format switch
    {
      DmsNotation.DecimalDegrees => "D",
      DmsNotation.DegreesDecimalMinutes => "DM",
      DmsNotation.DegreesMinutesDecimalSeconds => "DMS",
      _ => throw new System.NotImplementedException(),
    };
  }

  /// <summary>
  /// <para>DMS notation is a way to represent latitude or longitude.</para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Degree_(angle)#Subdivisions"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Decimal_degrees"/></para>
  /// </summary>
  public enum DmsNotation
  {
    /// <summary>A.k.a. "D" notation.</summary>
    DecimalDegrees,
    /// <summary>A.k.a. "DM" notation.</summary>
    DegreesDecimalMinutes,
    /// <summary>A.k.a. "DMS" notation.</summary>
    DegreesMinutesDecimalSeconds
  }
}
