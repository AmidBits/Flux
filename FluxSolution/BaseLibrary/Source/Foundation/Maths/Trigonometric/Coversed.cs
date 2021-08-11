namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Cvsin(double value)
      => 1 - System.Math.Sin(value);
    /// <summary>Returns the coversed sine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Cvsin(double value, out double sin)
      => 1 - (sin = System.Math.Sin(value));
    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Cvcos(double value)
      => 1 + System.Math.Sin(value);
    /// <summary>Returns the coversed cosine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Cvcos(double value, out double sin)
      => 1 + (sin = System.Math.Sin(value));

    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Acvsin(double value)
      => System.Math.Asin(1 - value);
    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Acvcos(double value)
      => System.Math.Acos(1 + value);
  }
}
