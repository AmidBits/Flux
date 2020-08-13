namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hcvsin(double value)
      => (1 - System.Math.Sin(value)) / 2;
    /// <summary>Returns the hacoversed sine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hcvsin(double value, out double sin)
      => (1 - (sin = System.Math.Sin(value))) / 2;
    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hcvcos(double value)
      => (1 + System.Math.Sin(value)) / 2;
    /// <summary>Returns the hacoversed cosine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hcvcos(double value, out double sin)
      => (1 + (sin = System.Math.Sin(value))) / 2;

    // Inverse hacoversed functions are missing.
  }
}
