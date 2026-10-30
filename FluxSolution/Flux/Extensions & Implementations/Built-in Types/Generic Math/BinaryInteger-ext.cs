namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region CreateFormatStringWithCountDecimals

      /// <summary>
      /// <para>Returns a string format for a specified number of fractional digits.</para>
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static string CreateFormatStringWithCountDecimals(TInteger count)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(count, TInteger.One);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(count, TInteger.CreateChecked(339));

        return "0." + new string('#', int.CreateChecked(count));
      }

      #endregion

      #region DirichletConvolution

      /// <summary>
      /// <para>Computes the Dirichlet convolution of two arithmetic functions f and g for a given n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Dirichlet_convolution"/></para>
      /// </summary>
      /// <param name="n">The positive integer for which to compute the convolution.</param>
      /// <param name="f">Function f: int -> long</param>
      /// <param name="g">Function g: int -> long</param>
      /// <returns>The value of (f * g)(n)</returns>
      public static TInteger DirichletConvolution(TInteger n, Func<TInteger, TInteger> f, Func<TInteger, TInteger> g)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        return GetDivisors(n).Select(d => f(d) * g(n / d)).Sum();
      }

      ///// <summary>
      ///// <para></para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Dirichlet_convolution"/></para>
      ///// </summary>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger DirichletConvolutionFunction(TInteger n)
      //{
      //  var result = TInteger.Zero;

      //  for (var d = TInteger.One; d <= n; d++)
      //    if (TInteger.IsZero(n % d)) // If d is a divisor of n, add f(d) * g(n//d) to the result.
      //      result += SumDivisors(d).Sum * EulerTotient(n / d);

      //  return result;
      //}

      #endregion

      #region GenerateSubRanges

      /// <summary>
      /// <para>Generates a new sequence with <paramref name="count"/> <see cref="System.Range"/> objects.</para>
      /// <para>How many sub-ranges do you want?</para>
      /// </summary>
      /// <param name="length"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<System.Range> GenerateSubRangesByCount(TInteger length, TInteger count)
        => GenerateSubRangesBySubLength(length, Number.IntegerDivRemCeiling(length, count).Quotient);

      /// <summary>
      /// <para>Generates a new sequence of <see cref="System.Range"/> objects, each with <paramref name="subLength"/> (the last may contain less) elements from the total length of a super-sequence.</para>
      /// <para>How many items do you want in each sub-range?</para>
      /// </summary>
      /// <param name="length"></param>
      /// <param name="subLength"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<System.Range> GenerateSubRangesBySubLength(TInteger length, TInteger subLength)
      {
        var subRanges = new System.Collections.Generic.List<System.Range>();

        for (var index = TInteger.Zero; index < length; index += subLength)
          subRanges.Add(RangeExtensions.FromOffsetAndLength(int.CreateChecked(index), int.CreateChecked(TInteger.Min(subLength, length - index))));

        return subRanges;
      }

      #endregion

      #region JosephusProblem

      /// <summary>
      /// <para>Calculates the last longest surviving position (it's not a 0-based index) of the Flavius Josephus problem where <paramref name="value"/> people stand in a circle and every <paramref name="k"/> person commits suicide.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Josephus_problem"/></para>
      /// </summary>
      /// <remarks>This is about counting positions, so it is 1-based position that is computed.</remarks>
      /// <param name="value">The number of people in the initial circle.</param>
      /// <param name="k">The count of each step. I.e. k-1 people are skipped and the k-th is executed.</param>
      /// <returns>The 1-indexed position that the survivor occupies.</returns>
      public static TInteger JosephusProblem(TInteger n, TInteger k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(k, n);

        var survivingPosition = TInteger.Zero;

        for (var positionCounter = TInteger.One; positionCounter <= n; positionCounter++)
          survivingPosition = (survivingPosition + k) % positionCounter;

        return survivingPosition + TInteger.One;
      }

      #endregion

      #region ToOrdinalFieldName(s)

      /// <summary>
      /// <para>Returns a generic <paramref name="fieldNamePrefix"/> for the <paramref name="fieldIndex"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="fieldIndex"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <param name="fieldIndex"></param>
      /// <param name="numericWidth"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string ToOrdinalFieldName(TInteger fieldIndex, int numericWidth, string fieldNamePrefix = "Column")
        => fieldNamePrefix + (fieldIndex + TInteger.One).ToString($"D{numericWidth}", null);

      /// <summary>
      /// <para>Returns a generic <paramref name="fieldNamePrefix"/> for the <paramref name="fieldIndex"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="fieldIndex"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <param name="fieldIndex"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string ToOrdinalFieldName(TInteger fieldIndex, string fieldNamePrefix = "Column")
        => ToOrdinalFieldName(fieldIndex, int.CreateChecked(fieldIndex <= TInteger.Zero ? TInteger.Zero : DigitCount(fieldIndex, TInteger.CreateChecked(10))), fieldNamePrefix);

      /// <summary>
      /// <para>Creates an array of generic column-<paramref name="fieldNamePrefix"/>s for <paramref name="fieldCount"/> amount of columns.</para>
      /// <example><paramref name="fieldCount"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
      /// </summary>
      /// <param name="fieldCount"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string[] ToOrdinalFieldNames(TInteger fieldCount, string fieldNamePrefix = "Column")
      {
        var maxWidth = int.CreateChecked(DigitCount(fieldCount, 10));

        return [.. Number.ArithmeticSequence(TInteger.One, fieldCount).Select(ci => ToOrdinalFieldName(ci, maxWidth, fieldNamePrefix))];
      }

      #endregion

      #region TryConvertTo

      public static bool TryConvertTo<TTarget>(TInteger source, out TTarget target)
        where TTarget : System.Numerics.IBinaryInteger<TTarget>
      {
        target = TTarget.CreateTruncating(source); // Convert TInteger → TTarget (truncating, never throws)

        var roundTrip = TInteger.CreateTruncating(target); // Convert back TTarget → TInteger (truncating, never throws)

        return roundTrip == source; // If round-trip preserved the value, it fits exactly.
      }

      #endregion

      //#region Twelvefold way

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnyDistinct(TInteger x, TInteger n) => Pow(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveDistinct(TInteger x, TInteger n) => FallingFactorial(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveDistinct(TInteger x, TInteger n) => Factorial(x) * StirlingNumber2ndKind(n, x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x + n - TInteger.One, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(n - TInteger.One, n - x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSxOrbits(TInteger x, TInteger n) => StirlingNumber2ndKind(n, x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      //#endregion
    }

    #region ..DeBruijnSequence.. (has nested methods)

    /// <summary>
    /// <para>Returns the total length of the DeBruijn sequence.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <remarks>The formula for the length is <c>(<paramref name="k"/> * <paramref name="n"/> + <paramref name="n"/> - 1)</c>.</remarks>
    public static int DeBruijnSequenceLength(int k, int n)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      return (int)System.Numerics.BigInteger.Pow(k, n) + n - 1;
    }

    /// <summary>
    /// <para>Creates a new DeBruijn sequence with DeBruijn numbers, which are the indices in a <paramref name="k"/> alphabet (e.g. 10 digit number pad) of <paramref name="n"/> size (e.g. 4 digit codes).</para>
    /// <para>The indices can be translated into symbols using an "alphabet".</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.List<int> GenerateDeBruijnSequence(int k, int n)
    {
      var sequence = new System.Collections.Generic.List<int>(DeBruijnSequenceLength(k, n));

      var a = new int[k * n];

      DeBruijn(1, 1);

      sequence.AddRange(sequence.GetRange(0, n - 1));

      return sequence;

      void DeBruijn(int t, int p)
      {
        if (t > n)
        {
          if ((n % p) == 0)
            sequence.AddRange(new System.ArraySegment<int>(a, 1, p));
        }
        else
        {
          a[t] = a[t - p];
          DeBruijn(t + 1, p);
          var j = a[t - p] + 1;

          while (j < k)
          {
            a[t] = j;
            DeBruijn(t + 1, t);
            j++;
          }
        }
      }
    }

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of indices based on <paramref name="k"/> and <paramref name="n"/>.</para>
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> GenerateDeBruijnSequenceExpanded(int k, int n)
      => GenerateDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.ToArray());

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of symbols based on <paramref name="k"/>, <paramref name="n"/> and <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TSymbol>> GenerateDeBruijnSequenceExpandedSymbols<TSymbol>(int k, int n, params TSymbol[] alphabet)
      => GenerateDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.Select(i => alphabet[i]).ToList());

    #endregion
  }
}
