using System.Runtime.Intrinsics;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Flux.Numerics.Vector3 ToVector3(this System.Runtime.Intrinsics.Vector256<double> source)
      => new Flux.Numerics.Vector3(Mask3D(source));
  }
}

namespace Flux.Numerics
{
  public struct Vector3
    : System.IEquatable<Vector3>
  {
    private readonly System.Runtime.Intrinsics.Vector256<double> m_v256;

    public Vector3(double x, double y, double z)
      : this(System.Runtime.Intrinsics.Vector256.Create(x, y, z, 0))
    { }
    public Vector3(System.Runtime.Intrinsics.Vector256<double> v256)
      => m_v256 = v256.Mask3D();

    public double X
      => m_v256.GetElement(0);
    public double Y
      => m_v256.GetElement(1);
    public double Z
      => m_v256.GetElement(2);

    /// <summary>Compute the Chebyshev length (distance, magnitude, etc.) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public double ChebyshevLength(double edgeLength = 1)
      => m_v256.ChebyshevLength3D(edgeLength).GetElement(0);
    /// <summary>Compute the euclidean length of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public double EuclideanLength()
      => m_v256.EuclideanLength3D().GetElement(0);
    /// <summary>Compute the euclidean length squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public double EuclideanLengthSquared()
      => m_v256.EuclideanLengthSquared3D().GetElement(0);
    /// <summary>Compute the Manhattan length (distance, magnitude, etc.) of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public double ManhattanLength(double edgeLength = 1)
      => m_v256.ManhattanLength3D(edgeLength).GetElement(0);

    public static Vector3 Add(Vector3 v1, Vector3 v2)
      => v1.m_v256.Add(v2.m_v256).ToVector3();
    public static Vector3 Add(Vector3 v1, double scalar)
      => v1.m_v256.Add(scalar).ToVector3();
    /// <summary>Compute the Chebyshev distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static double ChebyshevDistance(Vector3 v1, Vector3 v2, double edgeLength = 1)
      => (v2 - v1).ChebyshevLength(edgeLength);
    /// <summary>Returns the cross product of the vector.</summary>
    public static double CrossProduct(Vector3 v1, Vector3 v2)
      => v1.m_v256.CrossProduct3D(v2.m_v256).GetElement(0);
    public static Vector3 Divide(Vector3 v1, Vector3 v2)
      => v1.m_v256.Divide(v2.m_v256).ToVector3();
    public static Vector3 Divide(Vector3 v1, double scalar)
      => v1.m_v256.Divide(scalar).ToVector3();
    /// <summary>Returns the dot product of the vector.</summary>
    public static double DotProduct(Vector3 v1, Vector3 v2)
      => v1.m_v256.DotProduct3D(v2.m_v256).GetElement(0);
    /// <summary>Compute the euclidean distance of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistance(Vector3 v1, Vector3 v2)
      => (v2 - v1).EuclideanLength();
    /// <summary>Compute the euclidean distance squared of the vector.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Norm_(mathematics)#Euclidean_norm"/>
    public static double EuclideanDistanceSquared(Vector3 v1, Vector3 v2)
      => (v2 - v1).EuclideanLengthSquared();
    public static Vector3 Lerp(Vector3 v1, Vector3 v2, double mu)
      => v1.m_v256.Lerp(v2.m_v256, mu).ToVector3();
    /// <summary>Compute the Manhattan distance between the vectors.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static double ManhattanDistance(Vector3 v1, Vector3 v2, double edgeLength = 1)
      => (v2 - v1).ManhattanLength(edgeLength);
    public static Vector3 Multiply(Vector3 v1, Vector3 v2)
      => v1.m_v256.Multiply(v2.m_v256).ToVector3();
    public static Vector3 Multiply(Vector3 v1, double scalar)
      => v1.m_v256.Multiply(scalar).ToVector3();
    /// <summary>Returns a new vector with the components negated.</summary>
    public static Vector3 Negate(Vector3 v)
      => v.m_v256.Negate3D().ToVector3();
    public static Vector3 Nlerp(Vector3 v1, Vector3 v2, double mu)
      => v1.m_v256.Nlerp3D(v2.m_v256, mu).ToVector3();
    public static Vector3 Remainder(Vector3 v1, Vector3 v2)
      => v1.m_v256.Remainder(v2.m_v256).ToVector3();
    public static Vector3 Remainder(Vector3 v1, double scalar)
      => v1.m_v256.Remainder(scalar).ToVector3();
    public static Vector3 Slerp(Vector3 v1, Vector3 v2, double mu)
      => v1.m_v256.Slerp3D(v2.m_v256, mu).ToVector3();
    public static Vector3 Subtract(Vector3 v1, Vector3 v2)
      => v1.m_v256.Subtract(v2.m_v256).ToVector3();
    public static Vector3 Subtract(Vector3 v1, double scalar)
      => v1.m_v256.Subtract(scalar).ToVector3();

    #region Overloaded operators
    public static Vector3 operator +(Vector3 a, Vector3 b)
      => Add(a, b);
    public static Vector3 operator +(Vector3 a, double b)
      => Add(a, b);
    public static Vector3 operator /(Vector3 a, Vector3 b)
      => Divide(a, b);
    public static Vector3 operator /(Vector3 a, double b)
      => Divide(a, b);
    public static Vector3 operator *(Vector3 a, Vector3 b)
      => Multiply(a, b);
    public static Vector3 operator *(Vector3 a, double b)
      => Multiply(a, b);
    public static Vector3 operator -(Vector3 a, Vector3 b)
      => Subtract(a, b);
    public static Vector3 operator -(Vector3 a, double b)
      => Subtract(a, b);
    public static Vector3 operator %(Vector3 a, Vector3 b)
      => Remainder(a, b);
    public static Vector3 operator %(Vector3 a, double b)
      => Remainder(a, b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    /// <summary>Returns a boolean indicating whether the given Vector4 is equal to this Vector4 instance.</summary>
    public bool Equals(Vector3 other)
      => m_v256.Equals(other.m_v256);
    #endregion Implemented interfaces

    #region Object overrides
    /// <summary>Returns a boolean indicating whether the given Object is equal to this Vector4 instance.</summary>
    public override bool Equals(object? obj)
      => obj is Vector3 o && Equals(o);
    /// <summary>Returns the hash code for this instance.</summary>
    public override int GetHashCode()
      => m_v256.GetHashCode();
    /// <summary>Returns a String representing this Quaternion instance.</summary>
    public override string ToString()
      => $"<{GetType().Name}: X={X} Y={Y} Z={Z}>";
    #endregion Object overrides
  }
}
