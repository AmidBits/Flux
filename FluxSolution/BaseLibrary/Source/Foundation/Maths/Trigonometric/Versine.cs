namespace Flux
{
  public static partial class Maths
  {
    // Versed functions.

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Vsin(double value)
      => 1 - System.Math.Cos(value);
    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Vcos(double value)
      => 1 + System.Math.Cos(value);
    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Cvsin(double value)
      => 1 - System.Math.Sin(value);
    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Cvcos(double value)
      => 1 + System.Math.Sin(value);
    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hvsin(double value)
      => (1 - System.Math.Cos(value)) / 2;
    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hvcos(double value)
      => (1 + System.Math.Cos(value)) / 2;
    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hcvsin(double value)
      => (1 - System.Math.Sin(value)) / 2;
    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Hcvcos(double value)
      => (1 + System.Math.Sin(value)) / 2;

    // Inverse versed functions:

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Avsin(double y)
      => System.Math.Acos(1 - y);
    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Avcos(double y)
      => System.Math.Acos(y - 1);
    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Acvsin(double y)
      => System.Math.Asin(1 - y);
    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Acvcos(double y)
      => System.Math.Acos(y - 1);
    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahvsin(double y)
      => 2 * System.Math.Asin(System.Math.Sqrt(y));
    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahvcos(double y)
      => 2 * System.Math.Acos(System.Math.Sqrt(y));
    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahcvsin(double y)
      => System.Math.Asin(1 - (2 * y));
    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Ahcvcos(double y)
      => System.Math.Asin((2 * y) - 1);
  }
}