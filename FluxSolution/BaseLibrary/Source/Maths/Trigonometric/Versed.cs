namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Vsin(double value)
      => 1 - System.Math.Cos(value);
    /// <summary>Returns the versed sine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Vsin(double value, out double cos)
      => 1 - (cos = System.Math.Cos(value));
    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Vcos(double value)
      => 1 + System.Math.Cos(value);
    /// <summary>Returns the versed cosine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Vcos(double value, out double cos)
      => 1 + (cos = System.Math.Cos(value));

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Avsin(double value)
      => System.Math.Acos(1 - value);
    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Avcos(double value)
      => System.Math.Acos(1 + value);
  }
}
