////using System.Linq;

//namespace Flux.Maui.Graphics
//{
//  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
//  public struct Point
//    : System.IEquatable<Point>
//  {
//    public static readonly Point Empty;
//    public bool IsEmpty => Equals(Empty);

//    [System.Runtime.InteropServices.FieldOffset(0)] private double m_x;
//    [System.Runtime.InteropServices.FieldOffset(4)] private double m_y;

//    public double X { get => m_x; set => m_x = value; }
//    public double Y { get => m_y; set => m_y = value; }

//    #region Static Instances
//    /// <summary>Returns the vector (1,0,0).</summary>
//    public static readonly Point UnitX = new Point(1, 0);
//    /// <summary>Returns the vector (0,1,0).</summary>
//    public static readonly Point UnitY = new Point(0, 1);

//    /// <summary>Returns the vector (0,0).</summary>
//    public static readonly Point Zero;

//    ///// <summary>Returns the vector (0,1).</summary>
//    //public static Point2 North => new Point2(0, 1);
//    ///// <summary>Returns the vector (1,1).</summary>
//    //public static Point2 NorthEast => new Point2(1, 1);
//    ///// <summary>Returns the vector (1,0).</summary>
//    //public static Point2 East => new Point2(1, 0);
//    ///// <summary>Returns the vector (1,-1).</summary>
//    //public static Point2 SouthEast => new Point2(1, -1);
//    ///// <summary>Returns the vector (0,-1).</summary>
//    //public static Point2 South => new Point2(0, -1);
//    ///// <summary>Returns the vector (-1,-1).</summary>
//    //public static Point2 SouthWest => new Point2(-1, -1);
//    ///// <summary>Returns the vector (-1,0).</summary>
//    //public static Point2 West => new Point2(-1, 0);
//    ///// <summary>Returns the vector (-1,1).</summary>
//    //public static Point2 NorthWest => new Point2(-1, 1);
//    #endregion Static Instances

//    public Point(double value)
//      : this(value, value) { }
//    public Point(double x, double y)
//    {
//      m_x = x;
//      m_y = y;
//    }
//    public Point(int[] array, int startIndex)
//    {
//      if (array is null || array.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(array));

//      m_x = array[startIndex++];
//      m_y = array[startIndex];
//    }

//    #region Static members
//    /// <summary>Create a new vector with the sum from the vector added to the other.</summary>
//    public static Point Add(Point p1, Point p2)
//      => new Point(p1.m_x + p2.m_x, p1.m_y + p2.m_y);
//    /// <summary>Create a new vector with the sum from each member added to the value.</summary>
//    public static Point Add(Point p, int value)
//      => new Point(p.m_x + value, p.m_y + value);
//    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
//    public static double ChebyshevDistance(Point p1, Point p2)
//      => System.Math.Max(System.Math.Abs(p2.m_x - p1.m_x), System.Math.Abs(p2.m_y - p1.m_y));
//    /// <summary>Computes the closest cartesian coordinate vector at the specified angle and distance from the vector.</summary>
//    public static Point ComputeVector(Point p, double angle, double distance = 1)
//      => new Point((int)(distance * System.Math.Sin(angle)) + p.m_x, (int)(distance * System.Math.Cos(angle)) + p.m_y);
//    /// <summary>Create a new vector with each member subtracted by 1.</summary>
//    public static Point Decrement(Point p1)
//      => Subtract(p1, 1);
//    /// <summary>Create a new vector with the quotient from the vector divided by the other.</summary>
//    public static Point Divide(Point p1, Point p2)
//      => new Point(p1.m_x / p2.m_x, p1.m_y / p2.m_y);
//    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
//    public static Point Divide(Point p, int value)
//      => new Point(p.m_x / value, p.m_y / value);
//    /// <summary>Create a new vector with the quotient from each member divided by the value.</summary>
//    public static Point Divide(Point p, double value)
//      => new Point(System.Convert.ToInt32(p.m_x / value), System.Convert.ToInt32(p.m_y / value));
//    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
//    public static Point DivideCeiling(Point p, double value)
//      => new Point((int)(p.m_x / value), (int)(p.m_y / value));
//    /// <summary>Create a new vector with the floor(quotient) from each member divided by the value.</summary>
//    public static Point DivideFloor(Point p, double value)
//      => new Point((int)(p.m_x / value), (int)(p.m_y / value));
//    /// <summary>Compute the dot product, i.e. dot(a, b), of the vector (a) and vector b.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Dot_product"/>
//    public static double DotProduct(Point p1, Point p2)
//      => p1.m_x * p2.m_x + p1.m_y * p2.m_y;
//    /// <summary>Compute the euclidean distance of the vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
//    public static double EuclideanDistance(Point p1, Point p2)
//      => GetLength(p1 - p2);
//    /// <summary>Compute the euclidean distance squared of the vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
//    public static double EuclideanDistanceSquare(Point p1, Point p2)
//      => GetLengthSquared(p1 - p2);
//    /// <summary>Create a new vector from the labels and label definitions.</summary>
//    public static Point FromLabels(string column, string row, System.Collections.Generic.IList<string> columnLabels, System.Collections.Generic.IList<string> rowLabels)
//      => new Point(columnLabels?.IndexOf(column) ?? throw new System.ArgumentOutOfRangeException(nameof(column)), rowLabels?.IndexOf(row) ?? throw new System.ArgumentOutOfRangeException(nameof(column)));
//    /// <summary>Create a new vector from the index and the length of the m_x axis.</summary>
//    public static Point FromUniqueIndex(int index, int lengthX)
//      => new Point(index % lengthX, index / lengthX);
//    /// <summary>Compute the length (or magnitude) of the vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
//    public static double GetLength(in Point p)
//      => System.Math.Sqrt(p.m_x * p.m_x + p.m_y * p.m_y);
//    /// <summary>Compute the length (or magnitude) squared of the vector. This is much faster than Getlength(), if comparing magnitudes of vectors.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
//    public static double GetLengthSquared(in Point p)
//      => p.m_x * p.m_x + p.m_y * p.m_y;
//    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
//    public static System.Collections.Generic.IEnumerable<Point> GetQuadrantCenterVectors(Point source, Size subQuadrant)
//    {
//      yield return new Point(source.X + subQuadrant.Width, source.Y + subQuadrant.Height);
//      yield return new Point(source.X - subQuadrant.Width, source.Y + subQuadrant.Height);
//      yield return new Point(source.X - subQuadrant.Width, source.Y - subQuadrant.Height);
//      yield return new Point(source.X + subQuadrant.Width, source.Y - subQuadrant.Height);
//    }
//    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
//    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
//    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
//    public static int GetQuadrantNumber(Point source, in Point center)
//      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0);
//    /// <summary>Create a new vector with 1 added to each member.</summary>
//    public static Point Increment(in Point p1)
//      => Add(p1, 1);
//    public static Point InterpolateCosine(Point y1, Point y2, double mu)
//      => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2
//      ? (y1 * (1.0 - mu2) + y2 * mu2)
//      : throw new System.ArgumentNullException(nameof(mu));
//    public static Point InterpolateCubic(Point y0, Point y1, Point y2, Point y3, double mu)
//    {
//      var mu2 = mu * mu;

//      var a0 = y3 - y2 - y0 + y1;
//      var a1 = y0 - y1 - a0;
//      var a2 = y2 - y0;
//      var a3 = y1;

//      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
//    }
//    public static Point InterpolateHermite(Point y0, Point y1, Point y2, Point y3, double mu, double tension, double bias)
//    {
//      var mu2 = mu * mu;
//      var mu3 = mu2 * mu;

//      var onePbias = 1 + bias;
//      var oneMbias = 1 - bias;

//      var oneMtension = 1 - tension;

//      var m0 = (y1 - y0) * onePbias * oneMtension / 2;
//      m0 += (y2 - y1) * oneMbias * oneMtension / 2;
//      var m1 = (y2 - y1) * onePbias * oneMtension / 2;
//      m1 += (y3 - y2) * oneMbias * oneMtension / 2;

//      var a0 = 2 * mu3 - 3 * mu2 + 1;
//      var a1 = mu3 - 2 * mu2 + mu;
//      var a2 = mu3 - mu2;
//      var a3 = -2 * mu3 + 3 * mu2;

//      return a0 * y1 + a1 * m0 + a2 * m1 + a3 * y2;
//    }
//    public static Point InterpolateLinear(Point y1, Point y2, double mu)
//      => y1 * (1 - mu) + y2 * mu;
//    /// <summary>Compute the Manhattan distance between the vectors.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
//    public static double ManhattanDistance(Point p1, Point p2)
//      => System.Math.Abs(p2.m_x - p1.m_x) + System.Math.Abs(p2.m_y - p1.m_y);
//    /// <summary>Create a new vector with the product from the vector multiplied with the other.</summary>
//    public static Point Multiply(Point p1, Point p2)
//      => new Point(p1.m_x * p2.m_x, p1.m_y * p2.m_y);
//    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
//    public static Point Multiply(Point p, int value)
//      => new Point(p.m_x * value, p.m_y * value);
//    /// <summary>Create a new vector with the product from each member multiplied with the value.</summary>
//    public static Point Multiply(Point p, double value)
//      => new Point(System.Convert.ToInt32(p.m_x * value), System.Convert.ToInt32(p.m_y * value));
//    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
//    public static Point MultiplyCeiling(Point p, double value)
//      => new Point((int)System.Math.Ceiling(p.m_x * value), (int)System.Math.Ceiling(p.m_y * value));
//    /// <summary>Create a new vector with the floor(product) from each member multiplied with the value.</summary>
//    public static Point MultiplyFloor(Point p, double value)
//      => new Point((int)System.Math.Floor(p.m_x * value), (int)System.Math.Floor(p.m_y * value));
//    /// <summary>Create a new vector from the additive inverse, i.e. a negation of the member in the vector.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Additive_inverse"/>
//    public static Point Negate(in Point p)
//      => new Point(-p.m_x, -p.m_y); // Negate the members of the vector.
//    private static readonly System.Text.RegularExpressions.Regex m_regexParse = new System.Text.RegularExpressions.Regex(@"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]*$");
//    public static Point Parse(string pointAsString)
//      => m_regexParse.Match(pointAsString) is var m && m.Success && m.Groups["X"] is var gX && gX.Success && int.TryParse(gX.Value, out var x) && m.Groups["Y"] is var gY && gY.Success && int.TryParse(gY.Value, out var y)
//      ? new Point(x, y)
//      : throw new System.ArgumentOutOfRangeException(nameof(pointAsString));
//    public static bool TryParse(string pointAsString, out Point point)
//    {
//      try
//      {
//        point = Parse(pointAsString);
//        return true;
//      }
//      catch
//      {
//        point = default!;
//        return false;
//      }
//    }
//    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only m_x and m_y.</summary>
//    public static Point PerpendicularCcw(Point p)
//      => new Point(-p.m_y, p.m_x);
//    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only m_x and m_y.</summary>
//    public static Point PerpendicularCw(Point p)
//      => new Point(p.m_y, -p.m_x);
//    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
//    public static Point Random(int toExlusiveX, int toExclusiveY)
//      => new Point(Flux.Random.NumberGenerator.Crypto.NextInt32(toExlusiveX), Flux.Random.NumberGenerator.Crypto.NextInt32(toExclusiveY));
//    /// <summary>Create a new random vector in the range [(0, 0), toExclusive] using the crypto-grade rng.</summary>
//    public static Point Random(Point toExclusive)
//      => new Point(Flux.Random.NumberGenerator.Crypto.NextDouble(toExclusive.m_x), Flux.Random.NumberGenerator.Crypto.NextDouble(toExclusive.m_y));
//    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
//    public static Point RandomZero(double toExlusiveX, double toExclusiveY)
//      => new Point(Flux.Random.NumberGenerator.Crypto.NextDouble(toExlusiveX * 2) - toExlusiveX, Flux.Random.NumberGenerator.Crypto.NextDouble(toExclusiveY * 2) - toExclusiveY);
//    /// <summary>Create a new random vector in the range [-toExclusive, toExclusive] using the crypto-grade rng.</summary>
//    public static Point RandomZero(Point toExclusive)
//      => RandomZero(toExclusive.m_x, toExclusive.m_y);
//    /// <summary>Create a new vector with the remainder from the vector divided by the other.</summary>
//    public static Point Remainder(Point p1, Point p2)
//      => new Point(p1.m_x % p2.m_x, p1.m_y % p2.m_y);
//    /// <summary>Create a new vector with the remainder from each member divided by the value. Integer math is used.</summary>
//    public static Point Remainder(Point p, int value)
//      => new Point(p.m_x % value, p.m_y % value);
//    /// <summary>Create a new vector with the floor(remainder) from each member divided by the value.</summary>
//    public static Point Remainder(Point p, double value)
//      => new Point((int)(p.m_x % value), (int)(p.m_y % value));
//    /// <summary>Create a new vector with the difference from the vector subtracted by the other.</summary>
//    public static Point Subtract(Point p1, Point p2)
//      => new Point(p1.m_x - p2.m_x, p1.m_y - p2.m_y);
//    /// <summary>Create a new vector with the difference from each member subtracted by the value.</summary>
//    public static Point Subtract(Point p, int value)
//      => new Point(p.m_x - value, p.m_y - value);
//    /// <summary>Creates a <see cref='Size2'/> from a <see cref='Point'/>.</summary>
//    public static Size ToSize2(Point point)
//      => new Size(point.m_x, point.m_y);
//    #endregion Static members

//    #region Overloaded operators
//    public static Point operator -(Point v) => Negate(v);

//    public static Point operator --(Point v) => Subtract(v, 1);
//    public static Point operator ++(Point v) => Add(v, 1);

//    public static Point operator +(Point p1, Point p2) => Add(p1, p2);
//    public static Point operator +(Point p1, int v) => Add(p1, v);
//    public static Point operator +(int p1, Point p2) => Add(p2, p1);

//    public static Point operator -(Point p1, Point p2) => Subtract(p1, p2);
//    public static Point operator -(Point p1, int v) => Subtract(p1, v);

//    public static Point operator *(Point p1, Point p2) => Multiply(p1, p2);
//    public static Point operator *(Point p1, int v) => Multiply(p1, v);
//    public static Point operator *(int v, Point p2) => Multiply(p2, v);
//    public static Point operator *(Point p1, double v) => Multiply(p1, v);
//    public static Point operator *(double v, Point p2) => Multiply(p2, v);

//    public static Point operator /(Point p1, Point p2) => Divide(p1, p2);
//    public static Point operator /(Point p1, int v) => Divide(p1, v);
//    public static Point operator /(int v, Point p1) => Divide(p1, v);
//    public static Point operator /(Point p1, double v) => Divide(p1, v);
//    public static Point operator /(double v, Point p1) => Divide(p1, v);

//    public static Point operator %(Point p1, int v) => Remainder(p1, v);
//    public static Point operator %(Point p1, double v) => Remainder(p1, v);

//    public static bool operator ==(Point p1, Point p2) => p1.Equals(p2);
//    public static bool operator !=(Point p1, Point p2) => !p1.Equals(p2);
//    #endregion Overloaded operators

//    // System.IEquatable<Point2>
//    public bool Equals(Point other)
//      => m_x == other.m_x && m_y == other.m_y;

//    // Overrides
//    public override bool Equals(object? obj)
//       => obj is Point o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(m_x, m_y);
//    public override string ToString()
//      => $"<Point {m_x}, {m_y}>";
//  }
//}
