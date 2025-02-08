namespace Flux.Units
{
  /// <summary>
  /// <para>Probability is a ratio, represented as a closed interval [<see cref="Probability.MinValue"/> = 0.0, <see cref="Probability.MaxValue"/> = 1.0], where 0.0 indicates impossibility of an event and 1.0 indicates certainty.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Probability"/></para>
  /// </summary>
  public readonly record struct Probability
    : System.IComparable, System.IComparable<Probability>, System.IFormattable, IValueQuantifiable<double>
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly double m_value;

    public Probability(double ratio) => m_value = IntervalNotation.Closed.AssertWithinInterval(ratio, MinValue, MaxValue, nameof(ratio));

    /// <summary>
    /// <para>Computes the odds (p / (1 - p)) ratio of the probability.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Odds"/></para>
    /// </summary>
    /// <returns></returns>
    public Ratio ToOdds() => new(m_value, 1 - m_value);

    #region Static methods

    /// <summary>
    /// <para>Asserts that the <paramref name="probability"/> is within <see cref="Probability"/> constrained by the specified <paramref name="notation"/>. If not, it throws an exception.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf AssertMember<TSelf>(TSelf probability, IntervalNotation notation, string? paramName = null)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => notation.AssertWithinInterval(probability, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue), paramName ?? nameof(probability));

    /// <summary>
    /// <para>Returns whether the <paramref name="probability"/> is within <see cref="Probability"/> constrained by the specified <paramref name="notation"/>.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static bool IsMember<TSelf>(TSelf probability, IntervalNotation notation)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => notation.IsWithinInterval(probability, TSelf.CreateChecked(MinValue), TSelf.CreateChecked(MaxValue));

    #region Bernoulli distribution

    /// <summary>
    /// <para>In probability theory and statistics, the Bernoulli distribution, is the discrete probability distribution of a random variable which takes the value 1 with probability p and the value 0 with probability q = 1 - p. Less formally, it can be thought of as a model for the set of possible outcomes of any single experiment that asks a yes–no question.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Bernoulli_distribution"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
    /// <example>It can be used to represent a (possibly biased) coin toss where 1 and 0 would represent "heads" and "tails", respectively, and p would be the probability of the coin landing on heads (or vice versa where 1 would represent tails and p would be the probability of tails). In particular, unfair coins would have p != 1/2.</example>
    /// </summary>
    /// <param name="p">The probability.</param>
    /// <param name="k">A value of 0 or 1.</param>
    /// <returns></returns>
    public static TProbability BernoulliDistributionPmf<TProbability, TCount>(TProbability p, TCount k)
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => p * TProbability.CreateChecked(k) + (TProbability.One - p) * TProbability.CreateChecked(TCount.One - k);

    /// <summary>
    /// <para>In probability theory and statistics, the Bernoulli distribution, is the discrete probability distribution of a random variable which takes the value 1 with probability p and the value 0 with probability q = 1 - p. Less formally, it can be thought of as a model for the set of possible outcomes of any single experiment that asks a yes–no question.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Bernoulli_distribution"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
    /// <example>It can be used to represent a (possibly biased) coin toss where 1 and 0 would represent "heads" and "tails", respectively, and p would be the probability of the coin landing on heads (or vice versa where 1 would represent tails and p would be the probability of tails). In particular, unfair coins would have p != 1/2.</example>
    /// </summary>
    /// <param name="p">The probability.</param>
    /// <param name="k">True (1) or false (0).</param>
    /// <returns></returns>
    public static TProbability BernoulliDistributionPmf<TProbability>(TProbability p, bool k)
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      => BernoulliDistributionPmf(p, k ? 1 : 0);

    #endregion // Bernoulli distribution

    #region Binomial distribution

    /// <summary>
    /// <para>The probability of getting exactly <paramref name="k"/> successes in <paramref name="n"/> independent Bernoulli trials (each with success probability <paramref name="p"/>).</para>
    /// </summary>
    /// <typeparam name="TProbability"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="p">The success probability for each trial.</param>
    /// <param name="k">The exact number of successes of an outcome inquired.</param>
    /// <param name="n">The number of trials.</param>
    /// <returns></returns>
    public static TProbability BinomialDistributionPmf<TProbability, TCount>(TProbability p, TCount k, TCount n)
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => TProbability.CreateChecked(n.BinomialCoefficient(k)) * TProbability.Pow(p, TProbability.CreateChecked(k)) * TProbability.Pow(TProbability.One - p, TProbability.CreateChecked(n - k));

    /// <summary>
    /// <para>The the number of <paramref name="k"/> failures in a sequence of independent and identically distributed Bernoulli (each with success probability <paramref name="p"/>) trials before a specified (non-random) number of successes (denoted <paramref name="r"/>) occurs.</para>
    /// </summary>
    /// <typeparam name="TProbability"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="p">The success probability for each trial.</param>
    /// <param name="k">The number of failures.</param>
    /// <param name="r">The number of successes.</param>
    /// <returns></returns>
    public static TProbability NegativeBinomialDistributionPmf<TProbability, TCount>(TProbability p, TCount k, TCount r)
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => TProbability.CreateChecked((k + r - TCount.One).BinomialCoefficient(k)) * TProbability.Pow(TProbability.One - p, TProbability.CreateChecked(k)) * TProbability.Pow(p, TProbability.CreateChecked(r));

    #endregion // Binomial distribution

    /// <summary>Create a random probability from the specified <see cref="System.Random"/>.</summary>
    /// <param name="rng"></param>
    /// <returns></returns>
    public static Probability CreateRandom(System.Random? rng = null) => new((rng ?? System.Random.Shared).Next());

    /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
    public static TSelf Expit<TSelf>(TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => TSelf.One / (TSelf.Exp(-x) + TSelf.One);

    #region Geometric distribution

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static TSelf GeometricDistributionCdf1<TSelf>(TSelf p, TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => x >= TSelf.One ? TSelf.One - TSelf.Pow(TSelf.One - p, TSelf.Floor(x)) : TSelf.Zero;

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="p"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static TSelf GeometricDistributionCdf2<TSelf>(TSelf p, TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => x < TSelf.Zero ? TSelf.Zero : TSelf.One - TSelf.Pow(TSelf.One - p, TSelf.Floor(x) + TSelf.One);

    /// <summary>
    /// <para>The probability distribution of the number <paramref name="k"/> of Bernoulli trials needed to get one success, each with success probability <paramref name="p"/>.</para>
    /// <para><example>Suppose an ordinary die is thrown repeatedly until the first time a "1" appears. The probability distribution of the number of times it is thrown is supported on the infinite set {1,2,3,...} and is a geometric distribution with p = 1/6.</example></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Geometric_distribution"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
    /// </summary>
    /// <typeparam name="TProbability"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="p">The success probability (0, 1].</param>
    /// <param name="k">The number of trials [1, ..].</param>
    /// <returns></returns>
    public static TProbability GeometricDistributionPmf1<TProbability, TCount>(TProbability p, TCount k)
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => TProbability.Pow(TProbability.One - p, TProbability.CreateChecked(k - TCount.One)) * p;

    /// <summary>
    /// <para>The probability distribution of the number <paramref name="k"/> of failures before the first success, each with success probability <paramref name="p"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Geometric_distribution"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Probability_mass_function"/></para>
    /// </summary>
    /// <typeparam name="TProbability"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="p">The success probability (0, 1].</param>
    /// <param name="k">The number of failures [0, ..].</param>
    /// <returns></returns>
    public static TProbability GeometricDistributionPmf2<TProbability, TCount>(TProbability p, TCount k)
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      where TCount : System.Numerics.IBinaryInteger<TCount>
      => TProbability.Pow(TProbability.One - p, TProbability.CreateChecked(k)) * p;

    #endregion // Geometric distribution

    /// <summary>
    /// <para>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Logistic_function"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Sigmoid_function"/></para>
    /// </summary>
    /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity] (x).</param>
    /// <param name="k">The logistic growth rate or steepness of the curve (k). Default of (1).</param>
    /// <param name="x0">The x-value of the sigmoid's midpoint (x0). Default of (0)</param>
    /// <param name="L">The curve's maximum value (L).</param>
    /// <returns></returns>
    public static TSelf Logistic<TSelf>(TSelf x, TSelf k, TSelf x0, TSelf L)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => L / (TSelf.Exp(-(k * (x - x0))) + TSelf.One);

    /// <summary>
    /// <para>This nonlinear difference equation is intended to capture two effects.<list type="number"><item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item><item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item></list></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Logistic_map"/></para>
    /// <seealso cref="Statistics.PopulationModelRicker(double, double, double)"/>
    /// </summary>
    /// <param name="Xn">The ratio of existing population to maximum possible population (Xn).</param>
    /// <param name="r">A value in the range [0, 4] (r).</param>
    /// <returns>The ratio of population to max possible population in the next generation (Xn + 1)</returns>
    public static TSelf LogisticMap<TSelf>(TSelf Xn, TSelf r)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => r * Xn * (TSelf.One - Xn);

    /// <summary>
    /// <para>The logit function, which is the inverse of expit (or the logistic function), is the logarithm of the odds (p / (1 - p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Logit"/></para>
    /// </summary>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    public static TSelf Logit<TSelf>(TSelf probability)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.Log(OddsRatio(probability));

    /// <summary>
    /// <para>Computes the odds (p / (1 - p)) of a probability p.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Logit"/></para>
    /// </summary>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    public static TSelf OddsRatio<TSelf>(TSelf probability)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => probability / (TSelf.One - probability);

    /// <summary>
    /// <para>Returns the probability that specified event count in a group of total event count are all different (or unique). This is the computation P(A').</para>
    /// <para><seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/></para>
    /// <para><seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/></para>
    /// </summary>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static Probability OfNoDuplicates<TSelf>(TSelf whenCount, TSelf ofTotalCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var accumulation = 1d;
      for (var index = ofTotalCount - whenCount + TSelf.One; index < ofTotalCount; index++)
        accumulation *= double.CreateChecked(index) / double.CreateChecked(ofTotalCount);
      return new(accumulation);
    }

    /// <summary>
    /// <para>Returns the probability that at least 2 events are equal. This is computation P(A), which is the complement to P(A') computed in (<see cref="OfNoDuplicates(System.Numerics.BigInteger, System.Numerics.BigInteger)"/>).</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Birthday_problem"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Conditional_probability"/></para>
    /// </summary>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    public static Probability OfDuplicates<TSelf>(TSelf whenCount, TSelf ofTotalCount)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new(1 - OfNoDuplicates(whenCount, ofTotalCount).m_value);

    #region Poisson distribution

    /// <summary>
    /// <para>With the expectation of <paramref name="a"/> events in a given interval, the probability of <paramref name="k"/> events in the same interval is.</para>
    /// </summary>
    /// <param name="a">The number of expected events [1, ..] in a given interval.</param>
    /// <param name="k">The number of events [1, ..] in the same interval.</param>
    public static TProbability PoissonDistributionPmf<TCount, TProbability>(TCount a, TCount k)
      where TCount : System.Numerics.IBinaryInteger<TCount>
      where TProbability : System.Numerics.IFloatingPoint<TProbability>, System.Numerics.IPowerFunctions<TProbability>
      => TProbability.CreateChecked(a.IntegerPow(k)) * TProbability.Pow(TProbability.E, -TProbability.CreateChecked(a)) / TProbability.CreateChecked(k.Factorial());

    #endregion // Poisson distribution

    /// <summary>
    /// <para>A sigmoid function is any mathematical function whose graph has a characteristic S-shaped or sigmoid curve.</para>
    /// <para><see cref="https://en.wikipedia.org/wiki/Sigmoid_function"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Logistic_function"/></para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static TSelf Sigmoid<TSelf>(TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => Logistic(x, TSelf.One, TSelf.Zero, TSelf.One);

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
    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    #region IValueQuantifiable<>

    /// <summary>
    /// <para>The <see cref="Probability.Value"/> property is a value of the closed interval of probability [<see cref="MinValue"/>, <see cref="MaxValue"/>].</para>
    /// </summary>
    public double Value => m_value;

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
