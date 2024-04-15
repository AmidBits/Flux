//namespace Flux.Geometry
//{
//  public static partial class Tools
//  {
//    /// <summary>Computes the perimeter of the specified circle.</summary>
//    /// <param name="radius"></param>
//    /// <returns></returns>
//    public static double PerimeterOfCircle(double radius) => 2 * System.Math.PI * radius;

//    /// <summary>Computes the perimeter of the specified ellipse.</summary>
//    /// <param name="semiMajorAxis">The longer radius.</param>
//    /// <param name="semiMinorAxis">The shorter radius.</param>
//    public static double PerimeterOfEllipse(double semiMajorAxis, double semiMinorAxis)
//    {
//      var circle = System.Math.PI * (semiMajorAxis + semiMinorAxis);

//      if (semiMajorAxis == semiMinorAxis) // For a circle, use (PI * diameter);
//        return circle;

//      var h3 = 3 * System.Math.Pow(System.Math.Abs(semiMajorAxis - semiMinorAxis), 2) / System.Math.Pow(semiMajorAxis + semiMinorAxis, 2);

//      var ellipse = circle * (1 + h3 / (10 + System.Math.Sqrt(4 - h3)));

//      return ellipse;
//    }

//    /// <summary>Computes the perimeter of the specified rectangle.</summary>
//    /// <param name="circumradius">The radius or length of one side of a hexagon are both the same.</param>
//    public static double PerimeterOfHexagon(double circumradius) => 6 * circumradius;

//    /// <summary>Computes the perimeter of the specified rectangle.</summary>
//    /// <param name="length">The length of a rectangle.</param>
//    /// <param name="width">The width of a rectangle.</param>
//    public static double PerimeterOfRectangle(double length, double width) => 2 * length + 2 * width;
//  }
//}
