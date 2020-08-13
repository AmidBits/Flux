namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hvsin(double value)
      => (1 - System.Math.Cos(value)) / 2;
    /// <summary>Returns the haversed sine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hvsin(double value, out double cos)
      => (1 - (cos = System.Math.Cos(value))) / 2;
    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hvcos(double value)
      => (1 + System.Math.Cos(value)) / 2;
    /// <summary>Returns the haversed cosine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hvcos(double value, out double cos)
      => (1 + (cos = System.Math.Cos(value))) / 2;

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahvsin(double value)
      => 2 * System.Math.Asin(System.Math.Sqrt(value));
    /// <summary>Returns the inverse of haversed sine of the specified angle. Bonus result from sqrt(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahvsin(double value, out double sqrt)
      => 2 * System.Math.Asin(sqrt = System.Math.Sqrt(value));
    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahvcos(double value)
      => 2 * System.Math.Acos(System.Math.Sqrt(value));
    /// <summary>Returns the inverse of haversed cosine of the specified angle. Bonus result from sqrt(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahvcos(double value, out double sqrt)
      => 2 * System.Math.Acos(sqrt = System.Math.Sqrt(value));
  }
}
