//namespace Flux
//{
//  public static partial class Maths
//  {
//    // Haversed functions (half of the versed versions above):

//    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hvsin(double value)
//      => (1 - System.Math.Cos(value)) / 2;
//    /// <summary>Returns the haversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hvcos(double value)
//      => (1 + System.Math.Cos(value)) / 2;
//    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hcvsin(double value)
//      => (1 - System.Math.Sin(value)) / 2;
//    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hcvcos(double value)
//      => (1 + System.Math.Sin(value)) / 2;

//    // Inversed haversed functions:

//    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahvsin(double y)
//      => System.Math.Acos(1 - 2 * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));
//    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahvcos(double y)
//      => System.Math.Acos(2 * y - 1); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));
//    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahcvsin(double y)
//      => System.Math.Asin(1 - 2 * y);
//    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahcvcos(double y)
//      => System.Math.Asin(2 * y - 1);
//  }
//}
