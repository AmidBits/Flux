namespace Flux
{
  public static partial class Fx
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

  namespace Quantities
  {
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
}
