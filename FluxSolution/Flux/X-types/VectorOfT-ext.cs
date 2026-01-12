namespace Flux
{
  public static class VectorOfTExtensions
  {
    extension(System.Numerics.Vector)
    {
      /// <summary>Returns the angle for the source point to the other two specified points.</summary>>
      public static double AngleBetween<TNumber>(System.Numerics.Vector<TNumber> source, System.Numerics.Vector<TNumber> target1, System.Numerics.Vector<TNumber> target2)
        where TNumber : System.Numerics.INumber<TNumber>
        => AngleTo(target1 - source, target2 - source);

      /// <summary>(3D) Calculate the angle between the source vector and the specified target vector.
      /// When dot eq 0 then the vectors are perpendicular.
      /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
      /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
      /// </summary>
      public static double AngleTo<TNumber>(System.Numerics.Vector<TNumber> source, System.Numerics.Vector<TNumber> target)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        var dot = System.Numerics.Vector.Dot(System.Numerics.Vector.Normalize(source), System.Numerics.Vector.Normalize(target));

        return double.Acos(double.Clamp(double.CreateChecked(dot), -1, 1));
      }

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
        => source[0] * target[1] - source[1] * target[0];

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
        => Create(
          source[1] * target[2] - source[2] * target[1],
          source[2] * target[0] - source[0] * target[2],
          source[0] * target[1] - source[1] * target[0]
        );

      /// <summary>Returns the dot product of two non-normalized 3D vectors.</summary>
      /// <remarks>This method saves a square root computation by doing a two-in-one.</remarks>
      /// <see href="https://gamedev.stackexchange.com/a/89832/129646"/>
      public static TNumber DotProductEx<TNumber>(System.Numerics.Vector<TNumber> a, System.Numerics.Vector<TNumber> b)
        where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IRootFunctions<TNumber>
        => System.Numerics.Vector.Dot(a, b) / TNumber.Sqrt(EuclideanLengthSquared(a) * EuclideanLengthSquared(b));

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
          (quotient[i], remainder[i]) = BinaryIntegers.CeilingDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) ClosestDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = BinaryIntegers.ClosestDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) EuclideanDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = BinaryIntegers.EuclideanDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) FlooredDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = BinaryIntegers.FlooredDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) RoundedDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = BinaryIntegers.RoundedDivRem(source[i], divisor[i]);

        return (new System.Numerics.Vector<T>(quotient), new System.Numerics.Vector<T>(remainder));
      }

      public static (System.Numerics.Vector<T> Quotient, System.Numerics.Vector<T> Remainder) TruncatedDivRem<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> divisor)
        where T : System.Numerics.IBinaryInteger<T>
      {
        var quotient = new T[System.Numerics.Vector<T>.Count];
        var remainder = new T[System.Numerics.Vector<T>.Count];

        for (var i = System.Numerics.Vector<T>.Count - 1; i >= 0; i--)
          (quotient[i], remainder[i]) = T.DivRem(source[i], divisor[i]);

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
      /// <para>Slerp travels the torque-minimal path, which means it travels along the straightest path the rounded surface of a sphere.</para>
      /// </summary>
      public static System.Numerics.Vector<TNumber> Lerp<TNumber>(System.Numerics.Vector<TNumber> source, System.Numerics.Vector<TNumber> target, double percent = 0.5f)
        where TNumber : System.Numerics.INumber<TNumber>
      {
        var unit = Normalize(target - source);

        return source + unit * TNumber.CreateChecked(percent);
      }

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
      public static System.Numerics.Vector<T> Reciprocal<T>(System.Numerics.Vector<T> source)
        where T : System.Numerics.INumber<T>
        => System.Numerics.Vector.Divide(System.Numerics.Vector<T>.One, source);

      public static System.Numerics.Vector<double> Reciprocal(System.Numerics.Vector<double> source)
        => System.Numerics.Vector.Divide(System.Numerics.Vector<double>.One, source);

      public static System.Numerics.Vector<float> Reciprocal(System.Numerics.Vector<float> source)
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
      public static T ScalarTripleProduct3<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
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

      public static System.Numerics.Vector<T> UnitNormal<T>(System.Numerics.Vector<T> a, System.Numerics.Vector<T> b, System.Numerics.Vector<T> c)
        where T : System.Numerics.INumber<T>
        => Normalize(CrossProduct3(a - b, a - c));

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
      public static System.Numerics.Vector<T> VectorTripleProduct3<T>(System.Numerics.Vector<T> source, System.Numerics.Vector<T> second, System.Numerics.Vector<T> third)
        where T : System.Numerics.INumber<T>
        => CrossProduct3(source, CrossProduct3(second, third));
    }

    #region Vector collection (shape) algorithms

    extension<TNumber>(System.Collections.Generic.IList<System.Numerics.Vector<TNumber>> source)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      public double AngleSum(System.Numerics.Vector<TNumber> vector)
        => source.AggregateTuple2(0d, true, (a, v1, v2, i) => a + System.Numerics.Vector.AngleBetween(vector, v1, v2), (a, i) => a);

      /// <summary>
      /// <para>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative or positive. (2D)</para>
      /// </summary>
      public TNumber ComputeAreaSigned2()
        => source.AggregateTuple2(TNumber.Zero, true, (a, e1, e2, i) => a + CrossProduct2(e1, e2), (a, i) => a / TNumber.CreateChecked(2));

      /// <summary>
      /// <para>Compute the surface area of the polygon. (2D)</para>
      /// </summary>
      public TNumber ComputeArea2()
        => TNumber.Abs(ComputeAreaSigned2(source));

      /// <summary>
      /// <para>Compute the surface area of a simple (non-intersecting sides) polygon. The resulting area will be negative or positive. (3D)</para>
      /// </summary>
      public TNumber ComputeAreaSigned3()
      {
        var (x, y, z) = (TNumber.Zero, TNumber.Zero, TNumber.Zero);

        foreach (var cp3 in source.PartitionTuple2(true, System.Numerics.Vector.CrossProduct3))
        {
          x += cp3[0];
          y += cp3[1];
          z += cp3[2];
        }

        return System.Numerics.Vector.Dot(Create(x, y, z, TNumber.Zero), UnitNormal(source)) / TNumber.CreateChecked(2);
      }

      /// <summary>
      /// <para>Compute the surface area of the polygon. (3D)</para>
      /// </summary>
      public TNumber ComputeArea3()
        => TNumber.Abs(ComputeAreaSigned3(source));

      /// <summary>Returns the centroid (a.k.a. geometric center, arithmetic mean, barycenter, etc.) point of the polygon. (2D/3D)</summary>
      public System.Numerics.Vector<TNumber> ComputeCentroid()
        => source.Aggregate(System.Numerics.Vector<TNumber>.Zero, (a, e, index) => a + e, (a, count) => a / TNumber.CreateChecked(count));

      /// <summary>Compute the perimeter length of the polygon. (2D/3D)</summary>
      public TNumber ComputePerimeter()
        => source.AggregateTuple2(TNumber.Zero, true, (a, e1, e2, i) => a + EuclideanLength(e2 - e1)[0], (a, i) => a);

      /// <summary>Returns a sequence triplet angles.</summary>
      public System.Collections.Generic.IEnumerable<double> GetAngles()
        => source.PartitionTuple3(2, (v1, v2, v3, index) => System.Numerics.Vector.AngleBetween(v2, v1, v3));

      /// <summary>Returns a sequence triplet angles.</summary>
      public System.Collections.Generic.IEnumerable<(System.Numerics.Vector<TNumber> v1, System.Numerics.Vector<TNumber> v2, System.Numerics.Vector<TNumber> v3, int index, double angle)> GetAnglesEx()
        => source.PartitionTuple3(2, (v1, v2, v3, index) => (v1, v2, v3, index, System.Numerics.Vector.AngleBetween(v2, v1, v3)));

      public bool InsidePolygon(System.Numerics.Vector<TNumber> vector)
        => double.Abs(AngleSum(source, vector)) > 1;

      /// <summary>Determines whether the polygon is convex. (2D/3D)</summary>
      public bool IsConvexPolygon()
      {
        bool negative = false, positive = false;

        foreach (var angle in GetAngles(source))
        {
          if (double.IsNegative(angle))
            negative = true;
          else
            positive = true;

          if (negative && positive)
            return false;
        }

        return negative ^ positive;
      }

      /// <summary>Determines whether the polygon is equiangular, i.e. all angles are the same. (2D/3D)</summary>
      public bool IsEquiangularPolygon()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var e = source.PartitionTuple3(2, (v1, v2, v3, index) => AngleBetween(v2, v1, v3)).GetEnumerator();

        if (e.MoveNext())
        {
          var initialAngle = e.Current;

          while (e.MoveNext())
            if (initialAngle != e.Current)
              return false;
        }

        return true;
      }

      /// <summary>Determines whether the polygon is equiateral, i.e. all sides have the same length.</summary>
      public bool IsEquilateralPolygon()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var e = source.PartitionTuple2(true, (v1, v2, index) => EuclideanLength(v2 - v1)).GetEnumerator();

        if (e.MoveNext())
        {
          var initialLength = e.Current;

          while (e.MoveNext())
            if (initialLength != e.Current)
              return false;
        }

        return true;
      }

      /// <summary>Creates a new sequence with the midpoints between all vertices in the sequence.</summary>
      public System.Collections.Generic.IEnumerable<System.Numerics.Vector<TNumber>> GetMidpoints()
        => source.PartitionTuple2(true, (e1, e2, index) => (e2 + e1) / TNumber.CreateChecked(2));

      /// <summary>Creates a new sequence of triplets consisting of the leading vector, a newly computed midling vector and the trailing vector.</summary>
      public System.Collections.Generic.IEnumerable<(System.Numerics.Vector<TNumber> v1, System.Numerics.Vector<TNumber> vm, System.Numerics.Vector<TNumber> v2, int index)> GetMidpointsEx()
        => source.PartitionTuple2(true, (e1, e2, index) => (e1, (e2 + e1) / TNumber.CreateChecked(2), e2, index));

      /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 1 and 8 in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitAlongMidpoints()
      {
        var midpointPolygon = new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>();

        foreach (var pair in GetMidpointsEx(source).PartitionTuple2(true, (a, b, index) => (a, b)))
        {
          midpointPolygon.Add(pair.a.vm);

          yield return new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { pair.a.v2, pair.b.vm, pair.a.vm };
        }

        yield return midpointPolygon;
      }

      /// <summary>Returns a sequence of triangles from the centroid to all midpoints and vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 5 in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitCentroidToMidpoints()
        => ComputeCentroid(source) is var c ? GetMidpoints(source).PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { c, v1, v2 }) : throw new System.InvalidOperationException();

      /// <summary>Returns a sequence of triangles from the centroid to all vertices. Creates a triangle fan from the centroid point. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 3 and 10 in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitCentroidToVertices()
        => ComputeCentroid(source) is var c ? source.PartitionTuple2(true, (v1, v2, index) => new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { c, v1, v2 }) : throw new System.InvalidOperationException();

      /// <summary>Returns two polygons by splitting the polygon at two points. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 2 if odd count vertices and figure 9 if even count vertices, in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitInHalf(int index, double mu = 0.5)
      {
        var half1 = new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>();
        var half2 = new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>();

        var halfCount = source.Count / 2;

        if (int.IsOddInteger(source.Count))
        {
          var leftIndex = index + halfCount;
          var rightIndex = index + halfCount + 1;

          var midpointVector = System.Numerics.Vector.Lerp(source[leftIndex], source[rightIndex], mu);

          half1.Add(midpointVector); // Use .Insert() below.
          half2.Add(midpointVector); // Use .Add() below.
        }

        if (int.IsEvenInteger(source.Count))
        {
          var oppositeIndex = (index + halfCount) % source.Count;

          // Gather index + halfCount - 1 in one, and halfCount + the rest in the other.
        }
        else // Odd number of vertices, so manufacture a new vertex between the two surrounding the midpoint.
        {
          var midpointVector = System.Numerics.Vector.Lerp(source[index + halfCount], source[index + halfCount + 1], mu);

          // Gather index + halfCount + midpointVector in one and midpointVector + the rest in the other.
        }

        throw new System.NotImplementedException();
        //yield break;
      }

      /// <summary>Returns two polygons by splitting the polygon at two points. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 2 if odd count vertices and figure 9 if even count vertices, in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitInHalf()
      {
        var polygon1 = new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>();
        var polygon2 = new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>();

        foreach (var item in source ?? throw new System.ArgumentNullException(nameof(source)))
        {
          if (polygon1.Count > polygon2.Count)
          {
            polygon2.Add(polygon1[0]);

            polygon1.RemoveAt(0);
          }

          polygon1.Add(item);
        }

        if (polygon2.Count < 3)
        {
          var midpoint = System.Numerics.Vector.Lerp(polygon1[^1], polygon2[0], 0.5);

          polygon1.Add(midpoint);
          polygon2.Insert(0, midpoint);
        }

        polygon1.Add(polygon2[0]);
        polygon2.Add(polygon1[0]);

        yield return polygon1;
        yield return polygon2;
      }

      /// <summary>Returns a sequence of triangles from the specified polygon index to all midpoints, splitting all triangles at their midpoint along the polygon perimeter. Creates a triangle fan from the specified point. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 2, in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitMidpointToMidpoints(int index)
        => source.GetMidpoints().ToList().SplitVertexToMidpoints(index);

      /// <summary>Returns a sequence of triangles from the specified polygon index to all midpoints, splitting all triangles at their midpoint along the polygon perimeter. Creates a triangle fan from the specified point. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape. (Figure 2, in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitVertexToMidpoints(int index)
      {
        var angles = GetAnglesEx(source).ToList();

        if (index < 0 || index > angles.Count - 1)
        {
          index = (index == -1) ? angles.Aggregate((a, b) => (a.angle > b.angle) ? a : b).index : throw new System.ArgumentOutOfRangeException(nameof(index));
        }

        var vertex = angles[index].v2;

        var startIndex = index + 1;
        var count = startIndex + angles.Count - 2;

        for (var i = startIndex; i < count; i++)
        {
          var triplet = angles[i % angles.Count];
          var midpoint23 = System.Numerics.Vector.Lerp(triplet.v2, triplet.v3, 0.5f);

          yield return new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { vertex, triplet.v2, midpoint23 };
          yield return new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { vertex, midpoint23, triplet.v3 };
        }
      }

      /// <summary>Returns a sequence of triangles from the specified polygon index to all other points. Creates a triangle fan from the specified point. (2D/3D)</summary>
      /// <seealso cref="http://paulbourke.net/geometry/polygonmesh/"/>
      /// <remarks>Applicable to any shape with more than 3 vertices. (Figure 9, in link)</remarks>
      public System.Collections.Generic.IEnumerable<System.Collections.Generic.List<System.Numerics.Vector<TNumber>>> SplitVertexToVertices(int index)
      {
        var indices = source.Select((e, i) => i).ToList();

        while (indices.Count >= 3)
        {
          var ii1 = (index + 1) % source.Count;

          yield return new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { source[indices[index]], source[indices[ii1]], source[indices[(index + 2) % source.Count]] };

          indices.RemoveAt(ii1);
        }

        //var angles = GetAnglesEx(source).ToList();

        //if (index < 0 || index > angles.Count - 1)
        //  index = (index == -1) ? (angles.Aggregate((a, b) => (a.angle > b.angle) ? a : b).index - 1 + angles.Count) % angles.Count : throw new System.ArgumentOutOfRangeException(nameof(index));

        //var vertex = angles[index].v2;

        //var startIndex = index + 1;
        //var count = startIndex + angles.Count - 2;

        //for (var i = startIndex; i < count; i++)
        //  yield return new System.Collections.Generic.List<System.Numerics.Vector<TNumber>>() { vertex, angles[i % angles.Count].v2, angles[(i + 1) % angles.Count].v2 };
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <returns></returns>
      public System.Numerics.Vector<TNumber> UnitNormal()
      {
        using var e = source.GetEnumerator();

        if (e.MoveNext() && e.Current is var v0 && e.MoveNext() && e.Current is var v1 && e.MoveNext() && e.Current is var v2)
          return Normalize(CrossProduct3(v0 - v1, v0 - v2));

        throw new System.ArgumentException("At least three vertices are required to compute unit-normal.");
      }
    }

    #endregion

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
