namespace Flux.Geometry
{
  public struct Point2
    : System.IComparable<Point2>, System.IEquatable<Point2>
  {
    /// <summary>Returns the vector (0,0).</summary>
    public static readonly Point2 Zero;

    public int X;
    public int Y;

    public Point2(int value)
      : this(value, value) { }
    public Point2(int x, int y)
    {
      X = x;
      Y = y;
    }
    public Point2(int[] array, int startIndex)
    {
      if (array is null || array.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(array));

      X = array[startIndex++];
      Y = array[startIndex];
    }

    public System.Numerics.Vector2 ToVector2()
      => new System.Numerics.Vector2(X, Y);

    #region Static methods
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(Point2 p1, Point2 p2)
      => System.Math.Max(System.Math.Abs(p2.X - p1.X), System.Math.Abs(p2.Y - p1.Y));
    /// <summary>Computes the closest cartesian coordinate point at the specified angle and distance.</summary>
    public static Point2 ComputePoint(double angle, double distance)
      => new Point2(System.Convert.ToInt32(distance * System.Math.Sin(angle)), System.Convert.ToInt32(distance * System.Math.Cos(angle)));
    /// <summary>Returns the cross product of the two vectors.</summary>
    /// <remarks>This is equivalent to DotProduct(a, CrossProduct(b)), which is consistent with the notion of a "perpendicular dot product", which this is known as.</remarks>
    public static int CrossProduct(Point2 p1, Point2 p2)
      => p1.X * p2.Y - p1.Y * p2.X;
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point2 DivideCeiling(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Ceiling(p.X / value)), System.Convert.ToInt32(System.Math.Ceiling(p.Y / value)));
    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
    public static Point2 DivideFloor(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Floor(p.X / value)), System.Convert.ToInt32(System.Math.Floor(p.Y / value)));
    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
    public static int DotProduct(Point2 p1, Point2 p2)
      => p1.X * p2.X + p1.Y * p2.Y;
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(Point2 p1, Point2 p2)
      => GetLength(p1 - p2);
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquare(Point2 p1, Point2 p2)
      => GetLengthSquared(p1 - p2);
    /// <summary>Compute the length (or magnitude) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLength(Point2 p)
      => System.Math.Sqrt(p.X * p.X + p.Y * p.Y);
    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double GetLengthSquared(Point2 p)
      => p.X * p.X + p.Y * p.Y;
    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static System.Collections.Generic.IEnumerable<Point2> GetQuadrantCenterVectors(Point2 source, Size2 subQuadrant)
    {
      yield return new Point2(source.X + subQuadrant.Width, source.Y + subQuadrant.Height);
      yield return new Point2(source.X - subQuadrant.Width, source.Y + subQuadrant.Height);
      yield return new Point2(source.X - subQuadrant.Width, source.Y - subQuadrant.Height);
      yield return new Point2(source.X + subQuadrant.Width, source.Y - subQuadrant.Height);
    }
    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    public static int GetQuadrantNumber(Point2 source, Point2 center)
      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0);
    public static Point2 InterpolateCosine(Point2 y1, Point2 y2, double mu)
      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
      ? (y1 * (1.0 - mu2) + y2 * mu2)
      : throw new System.ArgumentNullException(nameof(mu));
    public static Point2 InterpolateCubic(Point2 y0, Point2 y1, Point2 y2, Point2 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }
    public static Point2 InterpolateHermite(Point2 y0, Point2 y1, Point2 y2, Point2 y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (y1 - y0) * onePbias * oneMtension / 2;
      m0 += (y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (y2 - y1) * onePbias * oneMtension / 2;
      m1 += (y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return y1 * a0 + m0 * a1 + m1 * a2 + y2 * a3;
    }
    public static Point2 InterpolateLinear(Point2 y1, Point2 y2, double mu)
      => y1 * (1 - mu) + y2 * mu;
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static int ManhattanDistance(Point2 p1, Point2 p2)
      => System.Math.Abs(p2.X - p1.X) + System.Math.Abs(p2.Y - p1.Y);
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point2 MultiplyCeiling(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Ceiling(p.X * value)), System.Convert.ToInt32(System.Math.Ceiling(p.Y * value)));
    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
    public static Point2 MultiplyFloor(Point2 p, double value)
      => new Point2(System.Convert.ToInt32(System.Math.Floor(p.X * value)), System.Convert.ToInt32(System.Math.Floor(p.Y * value)));
    private static readonly System.Text.RegularExpressions.Regex m_regexParse = new System.Text.RegularExpressions.Regex(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]*$");
    public static Point2 Parse(string pointAsString)
      => m_regexParse.Match(pointAsString) is var m && m.Success && m.Groups["X"] is var gX && gX.Success && int.TryParse(gX.Value, out var x) && m.Groups["Y"] is var gY && gY.Success && int.TryParse(gY.Value, out var y)
      ? new Point2(x, y)
      : throw new System.ArgumentOutOfRangeException(nameof(pointAsString));
    public static bool TryParse(string pointAsString, out Point2 point)
    {
      try
      {
        point = Parse(pointAsString);
        return true;
      }
      catch
      {
        point = default!;
        return false;
      }
    }
    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
    public static Point2 PerpendicularCcw(Point2 p)
      => new Point2(-p.Y, p.X);
    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
    public static Point2 PerpendicularCw(Point2 p)
      => new Point2(p.Y, -p.X);
    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
    public static Point2 FromRandom(int toExclusiveX, int toExclusiveY)
      => new Point2(Randomization.NumberGenerator.Crypto.Next(toExclusiveX), Randomization.NumberGenerator.Crypto.Next(toExclusiveY));
    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
    public static Point2 FromRandomZero(int toExclusiveX, int toExclusiveY)
      => new Point2(Randomization.NumberGenerator.Crypto.Next(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.Next(toExclusiveY * 2 - 1) - (toExclusiveY - 1));
    /// <summary>Creates a <see cref='Size2'/> from a <see cref='Point2'/>.</summary>
    public static Size2 ToSize2(Point2 point)
      => new Size2(point.X, point.Y);
    #endregion Static methods

    #region "Unique" index
    /// <summary>Convert an index to a 2D point, based on the specified grid lengths of axes.</summary>
    public static Point2 FromUniqueIndex(long index, Size2 bounds)
      => new Point2((int)(index % bounds.Width), (int)(index / bounds.Width));

    /// <summary>Converts the 2D point to an index, based on the specified grid lengths of axes.</summary>
    public static long ToUniqueIndex(Point2 point, Size2 bounds)
      => point.X + (point.Y * bounds.Width);
    #endregion "Unique" index

    #region Overloaded operators
    public static bool operator ==(Point2 p1, Point2 p2)
      => p1.Equals(p2);
    public static bool operator !=(Point2 p1, Point2 p2)
      => !p1.Equals(p2);

    public static Point2 operator -(Point2 v)
      => new Point2(-v.X, -v.Y);

    public static Point2 operator ~(Point2 v)
      => new Point2(~v.X, ~v.Y);

    public static Point2 operator --(Point2 p)
      => new Point2(p.X - 1, p.Y - 1);
    public static Point2 operator ++(Point2 p)
      => new Point2(p.X + 1, p.Y + 1);

    public static Point2 operator +(Point2 p1, Point2 p2)
      => new Point2(p1.X + p2.X, p1.Y + p2.Y);
    public static Point2 operator +(Point2 p, int v)
      => new Point2(p.X + v, p.Y + v);
    public static Point2 operator +(int v, Point2 p)
      => new Point2(v + p.X, v + p.Y);

    public static Point2 operator -(Point2 p1, Point2 p2)
      => new Point2(p1.X - p2.X, p1.Y - p2.Y);
    public static Point2 operator -(Point2 p, int v)
      => new Point2(p.X - v, p.Y - v);
    public static Point2 operator -(int v, Point2 p)
      => new Point2(v - p.X, v - p.Y);

    public static Point2 operator *(Point2 p1, Point2 p2)
      => new Point2(p1.X * p2.X, p1.Y * p2.Y);
    public static Point2 operator *(Point2 p, int v)
      => new Point2(p.X * v, p.Y * v);
    public static Point2 operator *(Point2 p, double v)
      => new Point2((int)(p.X * v), (int)(p.Y * v));
    public static Point2 operator *(int v, Point2 p)
      => new Point2(v * p.X, v * p.Y);
    public static Point2 operator *(double v, Point2 p)
      => new Point2((int)(v * p.X), (int)(v * p.Y));

    public static Point2 operator /(Point2 p1, Point2 p2)
      => new Point2(p1.X / p2.X, p1.Y / p2.Y);
    public static Point2 operator /(Point2 p, int v)
      => new Point2(p.X / v, p.Y / v);
    public static Point2 operator /(Point2 p, double v)
      => new Point2((int)(p.X / v), (int)(p.Y / v));
    public static Point2 operator /(int v, Point2 p)
      => new Point2(v / p.X, v / p.Y);
    public static Point2 operator /(double v, Point2 p)
      => new Point2((int)(v / p.X), (int)(v / p.Y));

    public static Point2 operator %(Point2 p1, Point2 p2)
      => new Point2(p1.X % p2.X, p1.Y % p2.Y);
    public static Point2 operator %(Point2 p, int v)
      => new Point2(p.X % v, p.Y % v);
    public static Point2 operator %(Point2 p, double v)
      => new Point2((int)(p.X % v), (int)(p.Y % v));
    public static Point2 operator %(int v, Point2 p)
      => new Point2(v % p.X, v % p.Y);
    public static Point2 operator %(double v, Point2 p)
      => new Point2((int)(v % p.X), (int)(v % p.Y));

    public static Point2 operator &(Point2 p1, Point2 p2)
      => new Point2(p1.X & p2.X, p1.Y & p2.Y);
    public static Point2 operator &(Point2 p, int v)
      => new Point2(p.X & v, p.Y & v);
    public static Point2 operator &(int v, Point2 p)
      => new Point2(v & p.X, v & p.Y);

    public static Point2 operator |(Point2 p1, Point2 p2)
      => new Point2(p1.X | p2.X, p1.Y | p2.Y);
    public static Point2 operator |(Point2 p, int v)
      => new Point2(p.X | v, p.Y | v);
    public static Point2 operator |(int v, Point2 p)
      => new Point2(v | p.X, v | p.Y);

    public static Point2 operator ^(Point2 p1, Point2 p2)
      => new Point2(p1.X ^ p2.X, p1.Y ^ p2.Y);
    public static Point2 operator ^(Point2 p, int v)
      => new Point2(p.X ^ v, p.Y ^ v);
    public static Point2 operator ^(int v, Point2 p)
      => new Point2(v ^ p.X, v ^ p.Y);

    public static Point2 operator <<(Point2 p, int v)
      => new Point2(p.X << v, p.Y << v);
    public static Point2 operator >>(Point2 p, int v)
      => new Point2(p.X >> v, p.Y >> v);
    #endregion Overloaded operators

    #region Implemented interfaces
    // System.IComparable
    public int CompareTo(Point2 other)
      => X < other.X ? -1 : X > other.X ? 1 : Y < other.Y ? -1 : Y > other.Y ? 1 : 0;

    // System.IEquatable
    public bool Equals(Point2 other)
      => X == other.X && Y == other.Y;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
       => obj is Point2 o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y);
    public override string ToString()
      => $"<{GetType().Name}: {X}, {Y}>";
    #endregion Object overrides
  }
}
