//namespace Flux
//{
//  public static partial class Geometric
//  {
//    /// <summary>
//    /// <para>The eccentricity of a conic section is a non-negative real number that uniquely characterizes its shape. One can think of the eccentricity as a measure of how much a conic section deviates from being circular. The eccentricity of an ellipse which is not a circle is greater than zero but less than 1.</para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Eccentricity_(mathematics)"/></para>
//    /// <para><see href="https://en.wikipedia.org/wiki/Focus_(geometry)"/></para>
//    /// </summary>
//    public static (double A, double B, double LinearEccentricity, double FirstEccentricity, double SecondEccentricity, double ThirdEccentricity, double FirstFlattening, double SecondFlattening, double ThirdFlattening) EllipseEccentricities(this System.Numerics.Vector2 SemiAxes)
//    {
//      var a = System.Math.Max(SemiAxes.X, SemiAxes.Y);
//      var b = System.Math.Min(SemiAxes.X, SemiAxes.Y);

//      var a2 = System.Math.Pow(a, 2);
//      var b2 = System.Math.Pow(b, 2);

//      return (
//        a,
//        b,
//        System.Math.Sqrt(a2 - b2), // Linear Eccentricity.
//        System.Math.Sqrt(1 - b2 / a2), // (First) Eccentricity.
//        System.Math.Sqrt(a2 / b2 - 1), // Second Eccentricity.
//        System.Math.Sqrt(a2 - b2) / System.Math.Sqrt(a2 + b2), // Third Eccentricity.
//        (a - b) / a, // (First) Flattening.
//        (a - b) / b, // Second Flattening.
//        (a - b) / (a + b) // Third Flattening.
//      );
//    }
//  }
//}
