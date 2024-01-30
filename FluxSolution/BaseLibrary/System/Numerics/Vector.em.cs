using System.Numerics;

namespace Flux
{
  public static partial class Vector
  {
    /// <summary>Compute the Chebyshev length of a vector.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/>
    public static T ChebyshevLength<T>(this System.Numerics.Vector<T> source, T edgeLength)
      where T : System.Numerics.INumber<T>
      => System.Numerics.Vector.Abs(source).Max() / edgeLength;

    public static System.Numerics.Vector<T> Create<T>(params T[] source)
      where T : System.Numerics.INumber<T>
    {
      var a = new T[System.Numerics.Vector<T>.Count];
      System.Array.Copy(source, a, source.Length);
      //for (var i = 0; i < source.Length; i++)
      //  a[i] = source[i];
      return new System.Numerics.Vector<T>(a);
    }

    public static System.Numerics.Vector<T> CreateVector<T>(this T source)
      where T : System.Numerics.INumber<T>
    {
      var a = new T[System.Numerics.Vector<T>.Count];
      for (var i = 0; i < a.Length; i++)
        a[i] = source;
      return new System.Numerics.Vector<T>(a);
    }

    public static System.Numerics.Vector<T> CreateVector<T>(this T source, int index)
      where T : System.Numerics.INumber<T>
    {
      var a = new T[System.Numerics.Vector<T>.Count];
      a[index] = source;
      return new System.Numerics.Vector<T>(a);
    }

    public static T EuclideanLengthSquared<T>(this System.Numerics.Vector<T> v)
      where T : System.Numerics.INumber<T>
      => System.Numerics.Vector.Sum(System.Numerics.Vector.Multiply(v, v));

    public static T EuclideanLength<T>(this System.Numerics.Vector<T> source)
      where T : System.Numerics.INumber<T>
      => System.Numerics.Vector.SquareRoot(EuclideanLengthSquared(source).CreateVector()).GetElement(0);

    public static bool IsZero<T>(this System.Numerics.Vector<T> source)
      where T : System.Numerics.INumber<T>
      => System.Numerics.Vector.EqualsAll(source, System.Numerics.Vector<T>.Zero);

    /// <summary>Compute the Manhattan length (or magnitude) of the vector. Known as the Manhattan distance (i.e. from 0,0,0).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/>
    public static T ManhattanLength<T>(this System.Numerics.Vector<T> source, T edgeLength)
      where T : System.Numerics.INumber<T>
      => System.Numerics.Vector.Sum(System.Numerics.Vector.Abs(source)) / edgeLength;

    public static T Max<T>(this System.Numerics.Vector<T> source)
      where T : System.Numerics.INumber<T>
      => T.Max(T.Max(source.GetElement(0), source.GetElement(1)), T.Max(source.GetElement(2), source.GetElement(3)));

    public static T Min<T>(this System.Numerics.Vector<T> source)
      where T : System.Numerics.INumber<T>
      => T.Min(T.Min(source.GetElement(0), source.GetElement(1)), T.Min(source.GetElement(2), source.GetElement(3)));

    public static System.Numerics.Vector<T> Normalized<T>(this System.Numerics.Vector<T> source)
      where T : System.Numerics.INumber<T>
      => System.Numerics.Vector.Divide(source, source.EuclideanLength());

    /// <summary>
    /// <para>Computes <paramref name="source"/> raised to the power of <paramref name="exponent"/>, for each component, using exponentiation by squaring.</para>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    /// </summary>
    public static System.Numerics.Vector<T> Pow<T>(this System.Numerics.Vector<T> source, T exponent)
      where T : System.Numerics.INumber<T>
    {
      if (source.IsZero() || T.IsZero(exponent))
        return System.Numerics.Vector<T>.One; // If either value or exponent is zero, one is customary.

      Maths.AssertNonNegative(exponent, nameof(exponent));

      var result = System.Numerics.Vector<T>.One;

      while (exponent != T.One)
      {
        if (T.IsOddInteger(exponent)) // Only act on set bits in exponent.
          result *= source; // Multiply by the current corresponding power-of-radix (adjusts value/exponent below for each iteration).

        exponent /= (T.One + T.One); // Half the exponent for the next iteration.
        source *= source; // Compute power-of-radix for the next iteration.
      }

      return result * source;
    }
  }
}
