namespace Flux
{
  namespace Quantities
  {
    /// <summary>
    /// <para>Probability is a ratio, represented as a closed interval [0.0, 1.0], where 0.0 indicates impossibility of an event and 1.0 indicates certainty.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Probability"/></para>
    /// </summary>
    public readonly record struct Probability
      : System.IComparable, System.IComparable<Probability>, System.IFormattable, IValueQuantifiable<double>
    {
      public const double MaxValue = 1;
      public const double MinValue = 0;

      private readonly double m_value;

      public Probability(double ratio)
        => m_value = IntervalNotation.Closed.AssertValidMember(ratio, MinValue, MaxValue, nameof(ratio));

      #region Static methods

      /// <summary>Asserts that the value is a member of the probability (throws an exception if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf AssertMember<TSelf>(TSelf probability, string? paramName = null)
        where TSelf : System.Numerics.IFloatingPoint<TSelf>
        => IntervalNotation.Closed.AssertValidMember(probability, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue), paramName ?? nameof(probability));

      /// <summary>Returns whether the value is a member of the probability.</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static bool VerifyMember<TSelf>(TSelf probability)
        where TSelf : System.Numerics.IFloatingPoint<TSelf>
        => IntervalNotation.Closed.IsValidMember(probability, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue));

      /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
      /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
      public static double Expit(double x) => 1 / (System.Math.Exp(-x) + 1);

      /// <summary>Create a random probability from the specified <see cref="System.Random"/>.</summary>
      /// <param name="rng"></param>
      /// <returns></returns>
      public static Probability FromRandom(System.Random? rng = null) => new((rng ?? System.Random.Shared).Next() / (double)int.MaxValue);

      /// <summary>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</summary>
      /// <see href="https://en.wikipedia.org/wiki/Logistic_function"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Sigmoid_function"/>
      /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
      /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity] (x).</param>
      /// <param name="k">The logistic growth rate or steepness of the curve (k).</param>
      /// <param name="x0">The x-value of the sigmoid's midpoint (x0).</param>
      /// <param name="L">The curve's maximum value (L).</param>
      public static double Logistic(double x, double k = 1, double x0 = 0, double L = 1) => L / (System.Math.Exp(-(k * (x - x0))) + 1);

      /// <summary>This nonlinear difference equation is intended to capture two effects.<list type="number"><item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item><item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item></list></summary>
      /// <param name="Xn">The ratio of existing population to maximum possible population (Xn).</param>
      /// <param name="r">A value in the range [0, 4] (r).</param>
      /// <returns>The ratio of population to max possible population in the next generation (Xn + 1)</returns>
      /// <see href="https://en.wikipedia.org/wiki/Logistic_map"/>
      /// <seealso cref="RickerModel(double, double, double)"/>
      public static double LogisticMap(double Xn, double r) => r * Xn * (1 - Xn);

      /// <summary>The logit function, which is the inverse of expit (or the logistic function), is the logarithm of the odds (p / (1 - p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</summary>
      /// <see href="https://en.wikipedia.org/wiki/Logit"/>
      /// <param name="probability">The probability in the range [0, 1].</param>
      /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
      public static double Logit(double probability) => System.Math.Log(OddsRatio(probability));

      /// <summary>Computes the odds (p / (1 - p)) of a probability p.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Logit"/>
      /// <param name="probability">The probability in the range [0, 1].</param>
      /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
      public static double OddsRatio(double probability) => probability / (1 - probability);

      /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). This is the computation P(A').</summary>
      /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
      /// <returns>The probability, which is in the range [0, 1].</returns>
      public static Probability OfNoDuplicates(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount)
      {
        var accumulation = 1d;
        for (var index = ofTotalCount - whenCount + 1; index < ofTotalCount; index++)
          accumulation *= (double)index / (double)ofTotalCount;
        return new(accumulation);
      }

      /// <summary>Returns the probability that at least 2 events are equal. This is computation P(A), which is the complement to P(A') computed in (<see cref="OfNoDuplicates(System.Numerics.BigInteger, System.Numerics.BigInteger)"/>).</summary>
      /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
      /// <returns>The probability, which is in the range [0, 1].</returns>
      public static Probability OfDuplicates(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount) => new(1 - OfNoDuplicates(whenCount, ofTotalCount).m_value);

      /// <summary>Computes the odds (p / (1 - p)) ratio of the probability.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Odds"/>
      public Ratio ToOdds() => new(m_value, 1 - m_value);

      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Probability a, Probability b) => a.CompareTo(b) < 0;
      public static bool operator <=(Probability a, Probability b) => a.CompareTo(b) <= 0;
      public static bool operator >(Probability a, Probability b) => a.CompareTo(b) > 0;
      public static bool operator >=(Probability a, Probability b) => a.CompareTo(b) >= 0;

      public static Probability operator -(Probability v) => new(-v.m_value);
      public static Probability operator +(Probability a, double b) => new(a.m_value + b);
      public static Probability operator +(Probability a, Probability b) => a + b.m_value;
      public static Probability operator /(Probability a, double b) => new(a.m_value / b);
      public static Probability operator /(Probability a, Probability b) => a / b.m_value;
      public static Probability operator *(Probability a, double b) => new(a.m_value * b);
      public static Probability operator *(Probability a, Probability b) => a * b.m_value;
      public static Probability operator %(Probability a, double b) => new(a.m_value % b);
      public static Probability operator %(Probability a, Probability b) => a % b.m_value;
      public static Probability operator -(Probability a, double b) => new(a.m_value - b);
      public static Probability operator -(Probability a, Probability b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Probability o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Probability other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => string.Format(formatProvider, $"{{0{(format is null ? string.Empty : $":{format}")}}}", m_value);

      // IQuantifiable<>
      /// <summary>
      /// <para>The <see cref="Probability.Value"/> property is a value of the closed interval of probability [<see cref="MinValue"/>, <see cref="MaxValue"/>].</para>
      /// </summary>
      public double Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}