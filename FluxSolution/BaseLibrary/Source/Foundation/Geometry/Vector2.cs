//namespace Flux.Geometry
//{
//  public struct Vector2
//  : System.IComparable<Vector2>, System.IEquatable<Vector2>
//  {
//    /// <summary>Returns the vector (0, 0).</summary>
//    public static readonly Vector2 Zero;

//    public double X;
//    public double Y;

//    public Vector2(double value)
//      : this(value, value) { }
//    public Vector2(double x, double y)
//    {
//      X = x;
//      Y = y;
//    }
//    public Vector2(double[] values, int startIndex)
//    {
//      if (values is null || values.Length - startIndex < 2) throw new System.ArgumentOutOfRangeException(nameof(values));

//      X = values[startIndex++];
//      Y = values[startIndex];
//    }

//    #region Static methods
//    /// <summary>Computes the closest cartesian coordinate point at the specified angle and distance.</summary>
//    public static Vector2 ComputePoint(double angle, double distance)
//      => new Vector2(System.Convert.ToInt32(distance * System.Math.Sin(angle)), System.Convert.ToInt32(distance * System.Math.Cos(angle)));
//    /// <summary>Create a new random vector using the crypto-grade rng.</summary>
//    public static Vector2 FromRandom(double toExclusiveX, double toExclusiveY)
//      => new Vector2(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY));
//    /// <summary>Create a new random vector in the range [(-toExlusiveX, -toExclusiveY), (toExlusiveX, toExclusiveY)] using the crypto-grade rng.</summary>
//    public static Vector2 FromRandomZero(double toExclusiveX, double toExclusiveY)
//      => new Vector2(Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveX * 2 - 1) - (toExclusiveX - 1), Randomization.NumberGenerator.Crypto.NextDouble(toExclusiveY * 2 - 1) - (toExclusiveY - 1));
//    /// <summary>Creates four vectors, each of which represents the center axis for each of the quadrants for the vector and the specified sizes of X and Y.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
//    public static System.Collections.Generic.IEnumerable<Vector2> GetQuadrantCenters(Vector2 source, Size2 subQuadrant)
//    {
//      yield return new Vector2(source.X + subQuadrant.Width, source.Y + subQuadrant.Height);
//      yield return new Vector2(source.X - subQuadrant.Width, source.Y + subQuadrant.Height);
//      yield return new Vector2(source.X - subQuadrant.Width, source.Y - subQuadrant.Height);
//      yield return new Vector2(source.X + subQuadrant.Width, source.Y - subQuadrant.Height);
//    }
//    /// <summary>Convert the 2D vector to a quadrant based on the specified center vector.</summary>
//    /// <returns>The quadrant identifer in the range 0-3, i.e. one of the four quadrants.</returns>
//    /// <see cref="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
//    public static int GetQuadrantNumber(Vector2 source, Vector2 center)
//      => (source.X >= center.X ? 1 : 0) + (source.Y >= center.Y ? 2 : 0);
//    /// <summary>Create a new vector by parsing a string.</summary>
//    public static Vector2 Parse(string s)
//      => System.Text.RegularExpressions.Regex.Match(s, @"^[^\d]*(?<X>\d+)[^\d]+(?<Y>\d+)[^\d]*$") is var m && m.Success && m.Groups["X"] is var gX && gX.Success && int.TryParse(gX.Value, out var x) && m.Groups["Y"] is var gY && gY.Success && int.TryParse(gY.Value, out var y)
//      ? new Vector2(x, y)
//      : throw new System.ArgumentOutOfRangeException(nameof(s));
//    /// <summary>Use the try paradigm to create a new vector by parsing a string and return whether successful.</summary>
//    public static bool TryParse(string s, out Vector2 v)
//    {
//      try
//      {
//        v = Parse(s);
//        return true;
//      }
//      catch
//      {
//        v = default!;
//        return false;
//      }
//    }
//    #endregion Static methods

//    #region Overloaded operators
//    public static bool operator ==(Vector2 p1, Vector2 p2)
//      => p1.Equals(p2);
//    public static bool operator !=(Vector2 p1, Vector2 p2)
//      => !p1.Equals(p2);

//    public static Vector2 operator -(Vector2 v)
//      => new Vector2(-v.X, -v.Y);

//    public static Vector2 operator --(Vector2 p)
//      => new Vector2(p.X - 1, p.Y - 1);
//    public static Vector2 operator ++(Vector2 p)
//      => new Vector2(p.X + 1, p.Y + 1);

//    public static Vector2 operator +(Vector2 p1, Vector2 p2)
//      => new Vector2(p1.X + p2.X, p1.Y + p2.Y);
//    public static Vector2 operator +(Vector2 p, int v)
//      => new Vector2(p.X + v, p.Y + v);
//    public static Vector2 operator +(int v, Vector2 p)
//      => new Vector2(v + p.X, v + p.Y);

//    public static Vector2 operator -(Vector2 p1, Vector2 p2)
//      => new Vector2(p1.X - p2.X, p1.Y - p2.Y);
//    public static Vector2 operator -(Vector2 p, int v)
//      => new Vector2(p.X - v, p.Y - v);
//    public static Vector2 operator -(int v, Vector2 p)
//      => new Vector2(v - p.X, v - p.Y);

//    public static Vector2 operator *(Vector2 p1, Vector2 p2)
//      => new Vector2(p1.X * p2.X, p1.Y * p2.Y);
//    public static Vector2 operator *(Vector2 p, int v)
//      => new Vector2(p.X * v, p.Y * v);
//    public static Vector2 operator *(Vector2 p, double v)
//      => new Vector2((int)(p.X * v), (int)(p.Y * v));
//    public static Vector2 operator *(int v, Vector2 p)
//      => new Vector2(v * p.X, v * p.Y);
//    public static Vector2 operator *(double v, Vector2 p)
//      => new Vector2((int)(v * p.X), (int)(v * p.Y));

//    public static Vector2 operator /(Vector2 p1, Vector2 p2)
//      => new Vector2(p1.X / p2.X, p1.Y / p2.Y);
//    public static Vector2 operator /(Vector2 p, int v)
//      => new Vector2(p.X / v, p.Y / v);
//    public static Vector2 operator /(Vector2 p, double v)
//      => new Vector2((int)(p.X / v), (int)(p.Y / v));
//    public static Vector2 operator /(int v, Vector2 p)
//      => new Vector2(v / p.X, v / p.Y);
//    public static Vector2 operator /(double v, Vector2 p)
//      => new Vector2((int)(v / p.X), (int)(v / p.Y));

//    public static Vector2 operator %(Vector2 p1, Vector2 p2)
//      => new Vector2(p1.X % p2.X, p1.Y % p2.Y);
//    public static Vector2 operator %(Vector2 p, int v)
//      => new Vector2(p.X % v, p.Y % v);
//    public static Vector2 operator %(Vector2 p, double v)
//      => new Vector2((int)(p.X % v), (int)(p.Y % v));
//    public static Vector2 operator %(int v, Vector2 p)
//      => new Vector2(v % p.X, v % p.Y);
//    public static Vector2 operator %(double v, Vector2 p)
//      => new Vector2((int)(v % p.X), (int)(v % p.Y));
//    #endregion Overloaded operators

//    #region Implemented interfaces
//    // System.IComparable
//    public int CompareTo(Vector2 other)
//      => X < other.X ? -1 : X > other.X ? 1 : Y < other.Y ? -1 : Y > other.Y ? 1 : 0;

//    // System.IEquatable
//    public bool Equals(Vector2 other)
//      => X == other.X && Y == other.Y;
//    #endregion Implemented interfaces

//    #region Object overrides
//    public override bool Equals(object? obj)
//       => obj is Vector2 o && Equals(o);
//    public override int GetHashCode()
//      => System.HashCode.Combine(X, Y);
//    public override string ToString()
//      => $"<{GetType().Name}: {X}, {Y}>";
//    #endregion Object overrides
//  }
//}
