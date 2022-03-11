//namespace Flux
//{
//  public static partial class Maths
//  {
//    // Versed functions.

//    /// <summary>Returns the versed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Vsin(double value)
//      => 1 - System.Math.Cos(value);
//    /// <summary>Returns the versed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Vcos(double value)
//      => 1 + System.Math.Cos(value);
//    /// <summary>Returns the coversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Cvsin(double value)
//      => 1 - System.Math.Sin(value);
//    /// <summary>Returns the coversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Cvcos(double value)
//      => 1 + System.Math.Sin(value);

//    // Inverse versed functions:

//    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Avsin(double y)
//      => System.Math.Acos(1 - y);
//    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Avcos(double y)
//      => System.Math.Acos(y - 1);
//    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Acvsin(double y)
//      => System.Math.Asin(1 - y);
//    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Acvcos(double y)
//      => System.Math.Asin(y - 1);
//  }
//}
