namespace Flux.Units
{
  /// <summary>Probability is a ratio, represented as a range [0, 1] of values where 0 indicates impossibility of an event and 1 indicates certainty.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Probability"/>
  public readonly record struct Probability
    : System.IComparable, System.IComparable<Probability>, System.IFormattable, IQuantifiable<double>
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly double m_probability;

    public Probability(double ratio)
      => m_probability = ratio >= MinValue && ratio <= MaxValue ? ratio : throw new System.ArgumentOutOfRangeException(nameof(ratio));

    #region Static methods

    /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
    public static double Expit(double x) => 1 / (System.Math.Exp(-x) + 1);

    /// <summary>Create a random probability from the specified <see cref="System.Random"/>.</summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static Probability FromRandom(System.Random rng) => new(rng.Next() / (double)int.MaxValue);

    /// <summary>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_function"/>
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
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_map"/>
    /// <seealso cref="RickerModel(double, double, double)"/>
    public static double LogisticMap(double Xn, double r) => r * Xn * (1 - Xn);

    /// <summary>The logit function, which is the inverse of expit (or the logistic function), is the logarithm of the odds (p / (1 - p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    public static double Logit(double probability) => System.Math.Log(OddsRatio(probability));

    /// <summary>Computes the odds (p / (1 - p)) of a probability p.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
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
    public static Probability OfDuplicates(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount) => new(1 - OfNoDuplicates(whenCount, ofTotalCount).m_probability);

    /// <summary>Computes the odds (p / (1 - p)) ratio of the probability.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Odds"/>
    public Ratio ToOdds() => new(m_probability, 1 - m_probability);

    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Probability v) => v.m_probability;
    public static explicit operator Probability(double v) => new(v);

    public static bool operator <(Probability a, Probability b) => a.CompareTo(b) < 0;
    public static bool operator <=(Probability a, Probability b) => a.CompareTo(b) <= 0;
    public static bool operator >(Probability a, Probability b) => a.CompareTo(b) > 0;
    public static bool operator >=(Probability a, Probability b) => a.CompareTo(b) >= 0;

    public static Probability operator -(Probability v) => new(-v.m_probability);
    public static Probability operator +(Probability a, double b) => new(a.m_probability + b);
    public static Probability operator +(Probability a, Probability b) => a + b.m_probability;
    public static Probability operator /(Probability a, double b) => new(a.m_probability / b);
    public static Probability operator /(Probability a, Probability b) => a / b.m_probability;
    public static Probability operator *(Probability a, double b) => new(a.m_probability * b);
    public static Probability operator *(Probability a, Probability b) => a * b.m_probability;
    public static Probability operator %(Probability a, double b) => new(a.m_probability % b);
    public static Probability operator %(Probability a, Probability b) => a % b.m_probability;
    public static Probability operator -(Probability a, double b) => new(a.m_probability - b);
    public static Probability operator -(Probability a, Probability b) => a - b.m_probability;
    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(object? other) => other is not null && other is Probability o ? CompareTo(o) : -1;

    // IComparable<>
    public int CompareTo(Probability other) => m_probability.CompareTo(other.m_probability);

    // IFormattable
    public string ToString(string? format, IFormatProvider? formatProvider) => m_probability.ToString(format, formatProvider);

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{m_probability}";
    public double Value { get => m_probability; init => m_probability = value; }

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
