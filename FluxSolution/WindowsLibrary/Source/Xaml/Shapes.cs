namespace Flux.Wpf
{
  public static partial class Media
  {
    public static partial class Shapes
    {
      /// <summary>Creates a circular polygon with a specified number of sides, radius and an optional offset (in radians).</summary>
      public static System.Collections.Generic.IEnumerable<System.Windows.Point> CreateCircular(double numberOfSides, double radius, double offsetRadians = 0f)
      {
        var step = GenericMath.PiX2 / numberOfSides;

        for (var angle = 0.0; angle < GenericMath.PiX2; angle += step)
          yield return (angle + offsetRadians).AngularRotationToPoint().Multiply(radius);
      }

      public static System.Collections.Generic.IEnumerable<System.Windows.Point> CreateHexagonHorizontal(double radius)
      {
        return CreateCircular(6, radius, GenericMath.PiOver2);
      }
      public static System.Collections.Generic.IEnumerable<System.Windows.Point> CreateHexagonVertical(double radius)
      {
        return CreateCircular(6, radius, 0);
      }

      /// <summary>Creates a square starting at point X and Y.</summary>
      public static System.Collections.Generic.IEnumerable<System.Windows.Point> CreateSquare(double startingX, double startingY)
      {
        var point = new System.Windows.Point(startingX, startingY);
        yield return point;
        point = point.GetPerpendicularCw();
        yield return point;
        point = point.GetPerpendicularCw();
        yield return point;
        point = point.GetPerpendicularCw();
        yield return point;
      }
      /// <summary>Creates a triangle starting at point X and Y.</summary>
      public static System.Collections.Generic.IEnumerable<System.Windows.Point> CreateTriangle(double startingX, double startingY)
      {
        var point1 = new System.Windows.Point(startingX, startingY);
        yield return point1;
        var point2 = point1.Divide(2.0).Negate();
        yield return point1.GetPerpendicularCw().Subtract(point2);
        yield return point1.GetPerpendicularCcw().Subtract(point2);
      }

      public static System.Windows.Point PatternCircle(double unitRatio)
      {
        unitRatio %= 1.0;

        return Quantities.Angle.ConvertDegreeToRadian(unitRatio * 360.0).AngularRotationToPoint();
      }
      public static System.Windows.Point PatternSquare(double unitRatio)
      {
        unitRatio %= 1.0;

        if (unitRatio >= 0 && unitRatio <= 0.25)
          return new System.Windows.Point(((unitRatio - 0.125) * 4), 0);
        else if (unitRatio <= 0.5)
          return new System.Windows.Point(0.5, (((unitRatio - 0.25) - 0.125) * 4));
        else if (unitRatio <= 0.75)
          return new System.Windows.Point((((unitRatio - 0.5) - 0.125) * -4), 0.5);
        else if (unitRatio <= 1.0)
          return new System.Windows.Point(0, (((unitRatio - 0.75) - .125) * -4));

        return new System.Windows.Point();
      }
    }
  }
}
