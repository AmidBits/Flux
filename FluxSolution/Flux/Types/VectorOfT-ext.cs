namespace Flux
{
  public static partial class XtensionVectorOfT
  {
    extension(System.Numerics.Vector)
    {
      /// <summary>
      /// <para>Computes the Chebyshev length of a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <param name="edgeLength"></param>
      /// <returns></returns>
      public static T ChebyshevLength<T>(System.Numerics.Vector<T> value, T edgeLength)
        where T : System.Numerics.INumber<T>
        => HorizontalMax(System.Numerics.Vector.Abs(value)) / edgeLength;

      public static System.Numerics.Vector<T> Create<T>(params System.ReadOnlySpan<T> values)
        where T : System.Numerics.INumber<T>
        => new(values);

      /// <summary>
      /// <para>Computes the 2D scalar cross product for two <see cref="System.Numerics.Vector{T}"/>.</para>
      /// <para>2D means that only the first two vector elements in each vector are used.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <returns></returns>
      public static T CrossProduct2<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> target)
        where T : System.Numerics.INumber<T>
        => source[0] * target[1] - target[0] * source[1];

      /// <summary>
      /// <para>Returns the 3D cross product for two <see cref="System.Numerics.Vector{T}"/>.</para>
      /// <para>3D means that only the first three vector elements in each vector are used.</para>
      /// <remarks>
      /// Cross product of A(x, y, z, _) and B(x, y, z, _) is
      ///                    0  1  2  3        0  1  2  3
      ///
      /// '(X = (Ay * Bz) - (Az * By), Y = (Az * Bx) - (Ax * Bz), Z = (Ax * By) - (Ay * Bx)'
      ///           1           2              1           2              1            2
      ///
      /// So we can do (Ay, Az, Ax, _) * (Bz, Bx, By, _) (last elem is irrelevant, as this is for Vector3)
      /// which leaves us with a of the first subtraction element for each (marked 1 above)
      /// Then we repeat with the right hand of subtractions (Az, Ax, Ay, _) * (By, Bz, Bx, _)
      /// which leaves us with the right hand sides (marked 2 above)
      /// Then we subtract them to get the correct vector
      /// We then mask out W to zero, because that is required for the Vector3 representation
      ///
      /// We perform the first 2 multiplications by shuffling the vectors and then multiplying them
      /// Helpers.Shuffle is the same as the C++ macro _MM_SHUFFLE, and you provide the order you wish the elements
      /// to be in *reversed* (no clue why), so here (3, 0, 2, 1) means you have the 2nd elem (1, 0 indexed) in the first slot,
      /// the 3rd elem (2) in the next one, the 1st elem (0) in the next one, and the 4th (3, W/_, unused here) in the last reg
      /// </remarks>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <returns></returns>
      public static System.Numerics.Vector<T> CrossProduct3<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> target)
        where T : System.Numerics.INumber<T>
        => Create(source[1] * target[2] - source[2] * target[1], source[2] * target[0] - source[0] * target[2], source[0] * target[1] - source[1] * target[0]);

      /// <summary>
      /// <para>Returns two new <see cref="System.Numerics.Vector{T}"/> with quotients and remainders.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="divisor"></param>
      /// <returns></returns>
      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) CeilingDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = ModuloOperations.CeilingDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) ClosestDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = ModuloOperations.ClosestDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) EuclideanDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = ModuloOperations.EuclideanDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) FlooredDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = ModuloOperations.FlooredDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) RoundedDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = ModuloOperations.RoundedDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) TruncatedDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = ModuloOperations.TruncatedDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      /// <summary>
      /// <para>Returns the Euclidean length of a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <returns></returns>
      public static System.Numerics.Vector<T> EuclideanLength<T>(System.Numerics.Vector<T> value)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.SquareRoot(System.Numerics.Vector.Create(EuclideanLengthSquared(value)));

      /// <summary>
      /// <para>Returns the Euclidean length squared of a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <returns></returns>
      public static T EuclideanLengthSquared<T>(System.Numerics.Vector<T> value)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.Sum(System.Numerics.Vector.Multiply(value, value));

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetOctant3<T>(System.Numerics.Vector<T> source)
        where T : System.Numerics.INumber<T>
        => T.IsNegative(source[2]) ? (T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 5 : 4) : (T.IsNegative(source[0]) ? 6 : 7)) : (T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 2 : 3) : (T.IsNegative(source[0]) ? 1 : 0));

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetOctant3<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
        where T : System.Numerics.INumber<T>
        => source[2] < center[2] ? (source[1] < center[1] ? (source[0] < center[0] ? 5 : 4) : (source[0] < center[0] ? 6 : 7)) : (source[1] < center[1] ? (source[0] < center[0] ? 2 : 3) : (source[0] < center[0] ? 1 : 0));

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetOctant3NegativeAs1<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
        where T : System.Numerics.INumber<T>
        => GetQuadrant2NegativeAs1(source, center) + (source[2] < center[2] ? 4 : 0);

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetOctant3PositiveAs1<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
        where T : System.Numerics.INumber<T>
        => GetQuadrant2PositiveAs1(source, center) + (source[2] >= center[2] ? 4 : 0);

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetQuadrant2<T>(System.Numerics.Vector<T> source)
        where T : System.Numerics.INumber<T>
        => T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 2 : 3) : (T.IsNegative(source[0]) ? 1 : 0);

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetQuadrant2<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
        where T : System.Numerics.INumber<T>
        => source[1] < center[1] ? (source[0] < center[0] ? 2 : 3) : (source[0] < center[0] ? 1 : 0);

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetQuadrant2NegativeAs1<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
        where T : System.Numerics.INumber<T>
        => (source[0] < center[0] ? 1 : 0) + (source[1] < center[1] ? 2 : 0);

      /// <summary>
      /// <para></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="center"></param>
      /// <returns></returns>
      public static int GetQuadrant2PositiveAs1<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
        where T : System.Numerics.INumber<T>
        => (source[0] >= center[0] ? 1 : 0) + (source[1] >= center[1] ? 2 : 0);

      /// <summary>
      /// <para>Returns the maximum of all values in a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <returns></returns>
      public static T HorizontalMax<T>(System.Numerics.Vector<T> value)
        where T : System.Numerics.INumber<T>
      {
        var max = value[0];

        for (var i = System.Numerics.Vector<T>.Count - 1; i < 0; i--)
          if (value[i] is var current && current > max)
            max = current;

        return max;
      }

      /// <summary>
      /// <para>Returns the minimum of all values in a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <returns></returns>
      public static T HorizontalMin<T>(System.Numerics.Vector<T> value)
        where T : System.Numerics.INumber<T>
      {
        var min = value[0];

        for (var i = System.Numerics.Vector<T>.Count - 1; i < 0; i--)
          if (value[i] is var current && current < min)
            min = current;

        return min;
      }

      ///// <summary>
      ///// <para>Returns the sum of all values in a <see cref="System.Numerics.Vector{T}"/>.</para>
      ///// </summary>
      ///// <typeparam name="T"></typeparam>
      ///// <param name="vector"></param>
      ///// <returns></returns>
      //public static T HorizontalSum<T>(System.Numerics.Vector<T> value)
      //  where T : System.Numerics.INumber<T>
      //{
      //  var sum = value[0];

      //  for (var i = System.Numerics.Vector<T>.Count - 1; i < 0; i--)
      //    sum += value[i];

      //  return sum;
      //}

      /// <summary>
      /// <para>Compute the Manhattan length (or magnitude) of a <see cref="System.Numerics.Vector{T}"/>. A.k.a. the Manhattan distance (i.e. from 0,0,0).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <param name="edgeLength"></param>
      /// <returns></returns>
      public static T ManhattanLength<T>(System.Numerics.Vector<T> value, T edgeLength)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.Sum(System.Numerics.Vector.Abs(value)) / edgeLength;

      /// <summary>
      /// <para>Returns the Minkowski length of a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Minkowski_distance"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <param name="order">Typically 1 or 2, which correspond to the Manhattan distance and the Euclidean distance, respectively. In the limiting case of reaching infinity, we obtain the Chebyshev distance.</param>
      /// <returns></returns>
      public static double MinkowskiLength<T>(System.Numerics.Vector<T> value, int order)
        where T : System.Numerics.INumber<T>
        => double.Pow(double.CreateChecked(System.Numerics.Vector.Sum(Pow(System.Numerics.Vector.Abs(value), order))), 1d / order);

      /// <summary>
      /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all <paramref name="vector"/> elements normalized.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <returns></returns>
      public static System.Numerics.Vector<T> Normalize<T>(System.Numerics.Vector<T> value)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.Divide(value, EuclideanLength(value));

      /// <summary>
      /// <para>Power function using exponentiation by squaring,</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static System.Numerics.Vector<T> Pow<T>(System.Numerics.Vector<T> value, int exponent)
        where T : System.Numerics.INumber<T>
      {
        if (int.IsNegative(exponent))
          return System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, Pow(value, int.Abs(exponent)));

        var pow = System.Numerics.Vector<T>.One;

        while (exponent > 0)
        {
          if ((exponent & 1) == 1)
            pow = System.Numerics.Vector.Multiply(pow, value);

          value = System.Numerics.Vector.Multiply(value, value);
          exponent >>= 1;
        }

        return pow;
      }

      /// <summary>
      /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all element reciprocals of a vector (i.e. 1 / element).</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Numerics.Vector<T> Reciprocal<T>(System.Numerics.Vector<T> source)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, source);

      public System.Numerics.Vector<double> Reciprocal(System.Numerics.Vector<double> source)
        => System.Numerics.Vector.Divide(System.Numerics.Vector<double>.One, source);

      public System.Numerics.Vector<float> Reciprocal(System.Numerics.Vector<float> source)
        => System.Numerics.Vector.Divide(System.Numerics.Vector<float>.One, source);

      /// <summary>
      /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all element remainders of a division of two vectors.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <param name="divisor"></param>
      /// <returns></returns>
      public static System.Numerics.Vector<T> Remainder<T>(System.Numerics.Vector<T> value, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.INumber<T>
      {
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = remainder.Length - 1; i >= 0; i--)
          remainder[i] = value[i] % divisor[i];

        return new System.Numerics.Vector<T>(remainder);
      }

      public static System.Numerics.Vector<double> Remainder(System.Numerics.Vector<double> source, System.Numerics.Vector<double> divisor)
        => System.Numerics.Vector.Subtract(source, System.Numerics.Vector.Multiply(System.Numerics.Vector.Round(System.Numerics.Vector.Divide(source, divisor), MidpointRounding.ToZero), divisor));

      public static System.Numerics.Vector<float> Remainder(System.Numerics.Vector<float> source, System.Numerics.Vector<float> divisor)
        => System.Numerics.Vector.Subtract(source, System.Numerics.Vector.Multiply(System.Numerics.Vector.Round(System.Numerics.Vector.Divide(source, divisor), MidpointRounding.ToZero), divisor));

      /// <summary>
      /// <para>Computes the 3D scalar triple product for three <see cref="System.Numerics.Vector{T}"/>.</para>
      /// <para>Dot(a, Cross3D(b, c))</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="second"></param>
      /// <param name="third"></param>
      /// <returns></returns>
      public T ScalarTripleProduct3<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.Dot(source, CrossProduct3(second, third));

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="divisor"></param>
      /// <returns></returns>
      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) TruncRem<T>(System.Numerics.Vector<T> value, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.INumber<T>
      {
        var remainder = Remainder(value, divisor);

        return (System.Numerics.Vector.Divide(System.Numerics.Vector.Subtract(value, remainder), divisor), remainder);
      }

      /// <summary>
      /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with the signs of each elements in a <see cref="System.Numerics.Vector{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="vector"></param>
      /// <returns></returns>
      public static System.Numerics.Vector<T> UnitSign<T>(System.Numerics.Vector<T> value)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.CopySign(System.Numerics.Vector<T>.One, value);

      /// <summary>
      /// <para>Create a new vector triple product for three <see cref="System.Numerics.Vector{T}"/>.</para>
      /// <para><c>Cross3D(a, Cross3D(b, c))</c></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="second"></param>
      /// <param name="third"></param>
      /// <returns></returns>
      public System.Numerics.Vector<T> VectorTripleProduct3<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
        where T : System.Numerics.INumber<T>
        => CrossProduct3(source, CrossProduct3(second, third));
    }

    //extension<T>(System.Numerics.Vector<T> source)
    //  where T : System.Numerics.INumber<T>
    //{
    //  public static System.Numerics.Vector<T> Create(params T[] values) => new(values);

    //  /// <summary>
    //  /// <para>Computes the Chebyshev length of a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="edgeLength"></param>
    //  /// <returns></returns>
    //  public T ChebyshevLength(T edgeLength)
    //    => HorizontalMax(System.Numerics.Vector.Abs(source)) / edgeLength;

    //  /// <summary>
    //  /// <para>Computes the 2D scalar cross product for two <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// <para>2D means that only the first two vector elements in each vector are used.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="target"></param>
    //  /// <returns></returns>
    //  public T Cross2D(System.Numerics.Vector<T> target)
    //    => source[0] * target[1] - target[0] * source[1];

    //  /// <summary>
    //  /// <para>Returns the 3D cross product for two <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// <para>3D means that only the first three vector elements in each vector are used.</para>
    //  /// <remarks>
    //  /// Cross product of A(x, y, z, _) and B(x, y, z, _) is
    //  ///                    0  1  2  3        0  1  2  3
    //  ///
    //  /// '(X = (Ay * Bz) - (Az * By), Y = (Az * Bx) - (Ax * Bz), Z = (Ax * By) - (Ay * Bx)'
    //  ///           1           2              1           2              1            2
    //  ///
    //  /// So we can do (Ay, Az, Ax, _) * (Bz, Bx, By, _) (last elem is irrelevant, as this is for Vector3)
    //  /// which leaves us with a of the first subtraction element for each (marked 1 above)
    //  /// Then we repeat with the right hand of subtractions (Az, Ax, Ay, _) * (By, Bz, Bx, _)
    //  /// which leaves us with the right hand sides (marked 2 above)
    //  /// Then we subtract them to get the correct vector
    //  /// We then mask out W to zero, because that is required for the Vector3 representation
    //  ///
    //  /// We perform the first 2 multiplications by shuffling the vectors and then multiplying them
    //  /// Helpers.Shuffle is the same as the C++ macro _MM_SHUFFLE, and you provide the order you wish the elements
    //  /// to be in *reversed* (no clue why), so here (3, 0, 2, 1) means you have the 2nd elem (1, 0 indexed) in the first slot,
    //  /// the 3rd elem (2) in the next one, the 1st elem (0) in the next one, and the 4th (3, W/_, unused here) in the last reg
    //  /// </remarks>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="vector"></param>
    //  /// <param name="target"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> Cross3D(System.Numerics.Vector<T> target)
    //    => Create(source[1] * target[2] - source[2] * target[1], source[2] * target[0] - source[0] * target[2], source[0] * target[1] - source[1] * target[0]);

    //  /// <summary>
    //  /// <para>Returns the Euclidean length of a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> EuclideanLength()
    //    => System.Numerics.Vector.SquareRoot(System.Numerics.Vector.Create(EuclideanLengthSquared(source)));

    //  /// <summary>
    //  /// <para>Returns the Euclidean length squared of a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <returns></returns>
    //  public T EuclideanLengthSquared()
    //    => System.Numerics.Vector.Sum(System.Numerics.Vector.Multiply(source, source));

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetOctant3()
    //    => T.IsNegative(source[2]) ? (T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 5 : 4) : (T.IsNegative(source[0]) ? 6 : 7)) : (T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 2 : 3) : (T.IsNegative(source[0]) ? 1 : 0));

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetOctant3(System.Numerics.Vector<T> center)
    //    => source[2] < center[2] ? (source[1] < center[1] ? (source[0] < center[0] ? 5 : 4) : (source[0] < center[0] ? 6 : 7)) : (source[1] < center[1] ? (source[0] < center[0] ? 2 : 3) : (source[0] < center[0] ? 1 : 0));

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetOctant3NegativeAs1(System.Numerics.Vector<T> center)
    //    => GetQuadrant2NegativeAs1(source, center) + (source[2] < center[2] ? 4 : 0);

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetOctant3PositiveAs1(System.Numerics.Vector<T> center)
    //    => GetQuadrant2PositiveAs1(source, center) + (source[2] >= center[2] ? 4 : 0);

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetQuadrant2()
    //    => T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 2 : 3) : (T.IsNegative(source[0]) ? 1 : 0);

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetQuadrant2(System.Numerics.Vector<T> center)
    //    => source[1] < center[1] ? (source[0] < center[0] ? 2 : 3) : (source[0] < center[0] ? 1 : 0);

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetQuadrant2NegativeAs1(System.Numerics.Vector<T> center)
    //    => (source[0] < center[0] ? 1 : 0) + (source[1] < center[1] ? 2 : 0);

    //  /// <summary>
    //  /// <para></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="center"></param>
    //  /// <returns></returns>
    //  public int GetQuadrant2PositiveAs1(System.Numerics.Vector<T> center)
    //    => (source[0] >= center[0] ? 1 : 0) + (source[1] >= center[1] ? 2 : 0);

    //  /// <summary>
    //  /// <para>Returns the maximum element of the first <paramref name="count"/> elements in a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// </summary>
    //  /// <param name="count"></param>
    //  /// <returns></returns>
    //  public T HorizontalMax(int count)
    //  {
    //    var maxValue = source[0];

    //    for (var i = count - 1; i < 0; i--)
    //      if (source[i] is var value && value > maxValue)
    //        maxValue = value;

    //    return maxValue;
    //  }

    //  public T HorizontalMax() => HorizontalMax(source, System.Numerics.Vector<T>.Count);

    //  /// <summary>
    //  /// <para>Returns the minimum element of the first <paramref name="count"/> elements in a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// </summary>
    //  /// <param name="count"></param>
    //  /// <returns></returns>
    //  public T HorizontalMin(int count)
    //  {
    //    var minValue = source[0];

    //    for (var i = count - 1; i < 0; i--)
    //      if (source[i] is var value && value < minValue)
    //        minValue = value;

    //    return minValue;
    //  }

    //  public T HorizontalMin() => HorizontalMin(source, System.Numerics.Vector<T>.Count);

    //  public T HorizontalSum(int count)
    //  {
    //    var value = source[0];

    //    for (var i = count - 1; i < 0; i--)
    //      value += source[i];

    //    return value;
    //  }

    //  public T HorizontalSum() => HorizontalSum(source, System.Numerics.Vector<T>.Count);

    //  /// <summary>
    //  /// <para>Compute the Manhattan length (or magnitude) of a <see cref="System.Numerics.Vector{T}"/>. A.k.a. the Manhattan distance (i.e. from 0,0,0).</para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="edgeLength"></param>
    //  /// <returns></returns>
    //  public T ManhattanLength(T edgeLength)
    //    => System.Numerics.Vector.Sum(System.Numerics.Vector.Abs(source)) / edgeLength;

    //  /// <summary>
    //  /// <para>Returns the Minkowski length of a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="order"></param>
    //  /// <returns></returns>
    //  public double MinkowskiLength(int order)
    //    => double.Pow(double.CreateChecked(System.Numerics.Vector.Sum(System.Numerics.Vector.Abs(source).Pow(order))), 1d / order);

    //  /// <summary>
    //  /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all <paramref name="source"/> elements normalized.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> Normalize()
    //    => System.Numerics.Vector.Divide(source, EuclideanLength(source));

    //  /// <summary>
    //  /// <para>Power function using exponentiation by squaring,</para>
    //  /// </summary>
    //  /// <param name="exponent"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> Pow(int exponent)
    //  {
    //    if (int.IsNegative(exponent))
    //      return System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, Pow(source, int.Abs(exponent)));

    //    var pow = System.Numerics.Vector<T>.One;

    //    while (exponent > 0)
    //    {
    //      if ((exponent & 1) == 1)
    //        pow = System.Numerics.Vector.Multiply(pow, source);

    //      source = System.Numerics.Vector.Multiply(source, source);
    //      exponent >>= 1;
    //    }

    //    return pow;
    //  }

    //  /// <summary>
    //  /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all element reciprocals of a vector (i.e. 1 / element).</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> Reciprocal()
    //    => System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, source);

    //  /// <summary>
    //  /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all element remainders of a division of two vectors.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="divisor"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> Remainder(System.Numerics.Vector<T> divisor)
    //  {
    //    var remainder = new T[System.Numerics.Vector<T>.Count];

    //    for (var i = 0; i < remainder.Length; i++)
    //      remainder[i] = source[i] % divisor[i];

    //    return new System.Numerics.Vector<T>(remainder);
    //  }

    //  /// <summary>
    //  /// <para>Computes the 3D scalar triple product for three <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// <para>Dot(a, Cross3D(b, c))</para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="second"></param>
    //  /// <param name="third"></param>
    //  /// <returns></returns>
    //  public T ScalarTripleProduct3D(System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
    //    => System.Numerics.Vector.Dot(source, Cross3D(second, third));

    //  /// <summary>
    //  /// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with the signs of each elements in a <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> Sign()
    //    => System.Numerics.Vector.CopySign(System.Numerics.Vector<T>.One, source);

    //  /// <summary>
    //  /// <para></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="divisor"></param>
    //  /// <returns></returns>
    //  public (System.Numerics.Vector<T> TruncatedQuotient, System.Numerics.Vector<T> Remainder) TruncRem(System.Numerics.Vector<T> divisor)
    //  {
    //    var remainder = Remainder(source, divisor);

    //    return (System.Numerics.Vector.Divide(System.Numerics.Vector.Subtract(source, remainder), divisor), remainder);
    //  }

    //  /// <summary>
    //  /// <para>Create a new vector triple product for three <see cref="System.Numerics.Vector{T}"/>.</para>
    //  /// <para><c>Cross3D(a, Cross3D(b, c))</c></para>
    //  /// <para><see href="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/></para>
    //  /// </summary>
    //  /// <typeparam name="T"></typeparam>
    //  /// <param name="source"></param>
    //  /// <param name="second"></param>
    //  /// <param name="third"></param>
    //  /// <returns></returns>
    //  public System.Numerics.Vector<T> VectorTripleProduct3D(System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
    //    => Cross3D(source, Cross3D(second, third));

    //  /// <summary>
    //  /// <para>Computes <paramref name="source"/> raised to the power of <paramref name="exponent"/>, for each component, using exponentiation by squaring.</para>
    //  /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
    //  /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
    //  /// </summary>
    //  public static System.Numerics.Vector<T> Pow<T>(this System.Numerics.Vector<T> source, T exponent)
    //    where T : struct, System.Numerics.INumber<T>
    //  {
    //    if (source.IsZero() || T.IsZero(exponent))
    //      return System.Numerics.Vector<T>.One; // If either value or exponent is zero, one is customary.

    //    exponent.AssertNonNegativeRealNumber(nameof(exponent));

    //    var result = System.Numerics.Vector<T>.One;

    //    while (exponent != T.One)
    //    {
    //      if (T.IsOddInteger(exponent)) // Only act on set bits in exponent.
    //        result *= source; // Multiply by the current corresponding power-of-radix (adjusts value/exponent below for each iteration).

    //      exponent /= (T.One + T.One); // Half the exponent for the next iteration.
    //      source *= source; // Compute power-of-radix for the next iteration.
    //    }

    //    return result * source;
    //  }
    //}

    //public static System.Numerics.Vector<double> Remainder(this System.Numerics.Vector<double> source, System.Numerics.Vector<double> divisor)
    //  => System.Numerics.Vector.Subtract(source, System.Numerics.Vector.Multiply(System.Numerics.Vector.Round(System.Numerics.Vector.Divide(source, divisor), MidpointRounding.ToZero), divisor));

    //public static System.Numerics.Vector<float> Remainder(this System.Numerics.Vector<float> source, System.Numerics.Vector<float> divisor)
    //=> System.Numerics.Vector.Subtract(source, System.Numerics.Vector.Multiply(System.Numerics.Vector.Round(System.Numerics.Vector.Divide(source, divisor), MidpointRounding.ToZero), divisor));
  }
}

//public static partial class VectorT
//{
//internal static System.Numerics.Vector<T> Create<T>(params T[] values)
//  => new System.Numerics.Vector<T>(values);
////{
////  var array = System.Buffers.ArrayPool<T>.Shared.Rent(System.Numerics.Vector<T>.Count);

////  for (var i = 0; i < values.Length; i++)
////    array[i] = values[i];

////  var vector = new System.Numerics.Vector<T>(array);

////  System.Buffers.ArrayPool<T>.Shared.Return(array);

////  return vector;
////}

///// <summary>
///// <para>Computes the Chebyshev length of a <see cref="System.Numerics.Vector{T}"/>.</para>
///// <para><see href="https://en.wikipedia.org/wiki/Chebyshev_distance"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="edgeLength"></param>
///// <returns></returns>
//public static T ChebyshevLength<T>(this System.Numerics.Vector<T> source, T edgeLength)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.Abs(source).Max() / edgeLength;

///// <summary>
///// <para>Computes the 2D scalar cross product for two <see cref="System.Numerics.Vector{T}"/>.</para>
///// <para>2D means that only the first two vector elements in each vector are used.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="target"></param>
///// <returns></returns>
//public static T Cross2D<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> target)
//  where T : System.Numerics.INumber<T>
//  => source[0] * target[1] - target[0] * source[1];

///// <summary>
///// <para>Returns the 3D cross product for two <see cref="System.Numerics.Vector{T}"/>.</para>
///// <para>3D means that only the first three vector elements in each vector are used.</para>
///// <remarks>
///// Cross product of A(x, y, z, _) and B(x, y, z, _) is
/////                    0  1  2  3        0  1  2  3
/////
///// '(X = (Ay * Bz) - (Az * By), Y = (Az * Bx) - (Ax * Bz), Z = (Ax * By) - (Ay * Bx)'
/////           1           2              1           2              1            2
/////
///// So we can do (Ay, Az, Ax, _) * (Bz, Bx, By, _) (last elem is irrelevant, as this is for Vector3)
///// which leaves us with a of the first subtraction element for each (marked 1 above)
///// Then we repeat with the right hand of subtractions (Az, Ax, Ay, _) * (By, Bz, Bx, _)
///// which leaves us with the right hand sides (marked 2 above)
///// Then we subtract them to get the correct vector
///// We then mask out W to zero, because that is required for the Vector3 representation
/////
///// We perform the first 2 multiplications by shuffling the vectors and then multiplying them
///// Helpers.Shuffle is the same as the C++ macro _MM_SHUFFLE, and you provide the order you wish the elements
///// to be in *reversed* (no clue why), so here (3, 0, 2, 1) means you have the 2nd elem (1, 0 indexed) in the first slot,
///// the 3rd elem (2) in the next one, the 1st elem (0) in the next one, and the 4th (3, W/_, unused here) in the last reg
///// </remarks>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="target"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> Cross3D<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> target)
//  where T : System.Numerics.INumber<T>
//  => Create(source[1] * target[2] - source[2] * target[1], source[2] * target[0] - source[0] * target[2], source[0] * target[1] - source[1] * target[0]);

///// <summary>
///// <para>Returns the Euclidean length of a <see cref="System.Numerics.Vector{T}"/>.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> EuclideanLength<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.SquareRoot(System.Numerics.Vector.Create(EuclideanLengthSquared(source)));

///// <summary>
///// <para>Returns the Euclidean length squared of a <see cref="System.Numerics.Vector{T}"/>.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static T EuclideanLengthSquared<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.Sum(System.Numerics.Vector.Multiply(source, source));

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetOctant<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => T.IsNegative(source[2]) ? (T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 5 : 4) : (T.IsNegative(source[0]) ? 6 : 7)) : (T.IsNegative(source[1]) ? (T.IsNegative(source[0]) ? 2 : 3) : (T.IsNegative(source[0]) ? 1 : 0));

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetOctant<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
//  where T : System.Numerics.INumber<T>
//  => source[2] < center[2] ? (source[1] < center[1] ? (source[0] < center[0] ? 5 : 4) : (source[0] < center[0] ? 6 : 7)) : (source[1] < center[1] ? (source[0] < center[0] ? 2 : 3) : (source[0] < center[0] ? 1 : 0));

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetOctantNegativeAs1<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
//  where T : System.Numerics.INumber<T>
//  => source.GetQuadrantNegativeAs1(center) + (source[2] < center[2] ? 4 : 0);

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetOctantPositiveAs1<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
//  where T : System.Numerics.INumber<T>
//  => source.GetQuadrantPositiveAs1(center) + (source[2] >= center[2] ? 4 : 0);

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetQuadrant<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
//  where T : System.Numerics.INumber<T>
//  => source[1] < center[1] ? (source[0] < center[0] ? 2 : 3) : (source[0] < center[0] ? 1 : 0);

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetQuadrantNegativeAs1<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
//  where T : System.Numerics.INumber<T>
//  => (source[0] < center[0] ? 1 : 0) + (source[1] < center[1] ? 2 : 0);

///// <summary>
///// <para></para>
///// <para><see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="center"></param>
///// <returns></returns>
//public static int GetQuadrantPositiveAs1<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> center)
//  where T : System.Numerics.INumber<T>
//  => (source[0] >= center[0] ? 1 : 0) + (source[1] >= center[1] ? 2 : 0);

///// <summary>
///// <para>Compute the Manhattan length (or magnitude) of a <see cref="System.Numerics.Vector{T}"/>. A.k.a. the Manhattan distance (i.e. from 0,0,0).</para>
///// <para><see href="https://en.wikipedia.org/wiki/Taxicab_geometry"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="edgeLength"></param>
///// <returns></returns>
//public static T ManhattanLength<T>(this System.Numerics.Vector<T> source, T edgeLength)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.Sum(System.Numerics.Vector.Abs(source)) / edgeLength;

///// <summary>
///// <para>Returns the maximum element in a <see cref="System.Numerics.Vector{T}"/>.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static T Max<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => T.Max(T.Max(source[0], source[1]), T.Max(source[2], source[3]));

///// <summary>
///// <para>Returns the minimum element in a <see cref="System.Numerics.Vector{T}"/>.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static T Min<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => T.Min(T.Min(source[0], source[1]), T.Min(source[2], source[3]));

///// <summary>
///// <para>Returns the Minkowski length of a <see cref="System.Numerics.Vector{T}"/>.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="order"></param>
///// <returns></returns>
//public static double MinkowskiLength<T>(this System.Numerics.Vector<T> source, int order)
//  where T : System.Numerics.IFloatingPoint<T>
//  => double.Pow(double.CreateChecked(System.Numerics.Vector.Sum(System.Numerics.Vector.Abs(source).Pow(order))), 1d / order);

///// <summary>
///// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all <paramref name="source"/> elements normalized.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> Normalize<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.Divide(source, source.EuclideanLength());

///// <summary>
///// <para></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="exponent"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> Pow<T>(this System.Numerics.Vector<T> source, int exponent)
//{
//  if (int.IsNegative(exponent))
//    return System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, source.Pow(int.Abs(exponent)));

//  var pow = System.Numerics.Vector<T>.One;

//  while (exponent > 0)
//  {
//    if ((exponent & 1) == 1)
//      pow = System.Numerics.Vector.Multiply(pow, source);

//    source = System.Numerics.Vector.Multiply(source, source);
//    exponent >>= 1;
//  }

//  return pow;
//}

///// <summary>
///// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all element reciprocals of a vector (i.e. 1 / element).</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> Reciprocal<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.IFloatingPoint<T>
//  => System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, source);

///// <summary>
///// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with all element remainders of a division of two vectors.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="divisor"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> Remainder<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
//  where T : System.Numerics.INumber<T>
//{
//  var remainder = new T[System.Numerics.Vector<T>.Count];

//  for (var i = 0; i < remainder.Length; i++)
//    remainder[i] = source[i] % divisor[i];

//  return new System.Numerics.Vector<T>(remainder);
//}

//public static System.Numerics.Vector<double> Remainder(this System.Numerics.Vector<double> source, System.Numerics.Vector<double> divisor)
//  => System.Numerics.Vector.Subtract(source, System.Numerics.Vector.Multiply(System.Numerics.Vector.Round(System.Numerics.Vector.Divide(source, divisor), MidpointRounding.ToZero), divisor));

//public static System.Numerics.Vector<float> Remainder(this System.Numerics.Vector<float> source, System.Numerics.Vector<float> divisor)
//  => System.Numerics.Vector.Subtract(source, System.Numerics.Vector.Multiply(System.Numerics.Vector.Round(System.Numerics.Vector.Divide(source, divisor), MidpointRounding.ToZero), divisor));

///// <summary>
///// <para>Computes the 3D scalar triple product for three <see cref="System.Numerics.Vector{T}"/>.</para>
///// <para>Dot(a, Cross3D(b, c))</para>
///// <para><see href="https://en.wikipedia.org/wiki/Triple_product#Scalar_triple_product"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="second"></param>
///// <param name="third"></param>
///// <returns></returns>
//public static T ScalarTripleProduct3D<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.Dot(source, Cross3D(second, third));

///// <summary>
///// <para>Returns a new <see cref="System.Numerics.Vector{T}"/> with the signs of each elements in a <see cref="System.Numerics.Vector{T}"/>.</para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> Sign<T>(this System.Numerics.Vector<T> source)
//  where T : System.Numerics.INumber<T>
//  => System.Numerics.Vector.CopySign(System.Numerics.Vector<T>.One, source);

///// <summary>
///// <para></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="divisor"></param>
///// <returns></returns>
//public static (System.Numerics.Vector<T> TruncatedQuotient, System.Numerics.Vector<T> Remainder) TruncRem<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
//  where T : System.Numerics.INumber<T>
//{
//  var remainder = source.Remainder(divisor);

//  return (System.Numerics.Vector.Divide(System.Numerics.Vector.Subtract(source, remainder), divisor), remainder);
//}

///// <summary>
///// <para>Create a new vector triple product for three <see cref="System.Numerics.Vector{T}"/>.</para>
///// <para><c>Cross3D(a, Cross3D(b, c))</c></para>
///// <para><see href="https://en.wikipedia.org/wiki/Triple_product#Vector_triple_product"/></para>
///// </summary>
///// <typeparam name="T"></typeparam>
///// <param name="source"></param>
///// <param name="second"></param>
///// <param name="third"></param>
///// <returns></returns>
//public static System.Numerics.Vector<T> VectorTripleProduct3D<T>(this System.Numerics.Vector<T> source, System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
//  where T : System.Numerics.INumber<T>
//  => Cross3D(source, Cross3D(second, third));

//    /// <summary>
//    /// <para>Computes <paramref name="source"/> raised to the power of <paramref name="exponent"/>, for each component, using exponentiation by squaring.</para>
//    /// <see href="https://en.wikipedia.org/wiki/Exponentiation"/>
//    /// <see href="https://en.wikipedia.org/wiki/Exponentiation_by_squaring"/>
//    /// </summary>
//    public static System.Numerics.Vector<T> Pow<T>(this System.Numerics.Vector<T> source, T exponent)
//      where T : struct, System.Numerics.INumber<T>
//    {
//      if (source.IsZero() || T.IsZero(exponent))
//        return System.Numerics.Vector<T>.One; // If either value or exponent is zero, one is customary.

//      exponent.AssertNonNegativeRealNumber(nameof(exponent));

//      var result = System.Numerics.Vector<T>.One;

//      while (exponent != T.One)
//      {
//        if (T.IsOddInteger(exponent)) // Only act on set bits in exponent.
//          result *= source; // Multiply by the current corresponding power-of-radix (adjusts value/exponent below for each iteration).

//        exponent /= (T.One + T.One); // Half the exponent for the next iteration.
//        source *= source; // Compute power-of-radix for the next iteration.
//      }

//      return result * source;
//    }
//  }
//}
