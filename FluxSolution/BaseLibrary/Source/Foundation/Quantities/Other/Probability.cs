namespace Flux
{
  /// <summary>Probability is a ratio, represented as a range [0, 1] of values where 0 indicates impossibility of an event and 1 indicates certainty.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Probability"/>
  public struct Probability
    : System.IComparable<Probability>, System.IConvertible, System.IEquatable<Probability>, IQuantifiable<double>
  {
    public const double MaxValue = 1;
    public const double MinValue = 0;

    private readonly double m_probability;

    public Probability(double ratio)
      => m_probability = ratio >= MinValue && ratio <= MaxValue ? ratio : throw new System.ArgumentOutOfRangeException(nameof(ratio));

    [System.Diagnostics.Contracts.Pure]
    public double Value
      => m_probability;

    #region Static methods
    /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
    [System.Diagnostics.Contracts.Pure]
    public static double Expit(double x)
      => 1 / (System.Math.Exp(-x) + 1);

    /// <summary>A logistic function or logistic curve is a common "S" shape (sigmoid curve).</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_function"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Sigmoid_function"/>
    /// <remarks>The standard logistic function is the logistic function with parameters (k = 1, x0 = 0, L = 1), a.k.a. sigmoid function.</remarks>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity] (x).</param>
    /// <param name="k">The logistic growth rate or steepness of the curve (k).</param>
    /// <param name="x0">The x-value of the sigmoid's midpoint (x0).</param>
    /// <param name="L">The curve's maximum value (L).</param>
    [System.Diagnostics.Contracts.Pure]
    public static double Logistic(double x, double k = 1, double x0 = 0, double L = 1)
      => L / (System.Math.Exp(-(k * (x - x0))) + 1);

    /// <summary>This nonlinear difference equation is intended to capture two effects.<list type="number"><item>Reproduction where the population will increase at a rate proportional to the current population when the population size is small.</item><item>Starvation (density-dependent mortality) where the growth rate will decrease at a rate proportional to the value obtained by taking the theoretical "carrying capacity" of the environment less the current population.</item></list></summary>
    /// <param name="Xn">The ratio of existing population to maximum possible population (Xn).</param>
    /// <param name="r">A value in the range [0, 4] (r).</param>
    /// <returns>The ratio of population to max possible population in the next generation (Xn + 1)</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Logistic_map"/>
    /// <seealso cref="RickerModel(double, double, double)"/>
    [System.Diagnostics.Contracts.Pure]
    public static double LogisticMap(double Xn, double r)
      => r * Xn * (1 - Xn);

    /// <summary>The logit function, which is the inverse of expit (or the logistic function), is the logarithm of the odds (p / (1 - p)) where p is the probability. Creates a map of probability values from [0, 1] to [-infinity, +infinity].</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    [System.Diagnostics.Contracts.Pure]
    public static double Logit(double probability)
      => System.Math.Log(OddsRatio(probability));

    /// <summary>Computes the odds (p / (1 - p)) of a probability p.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Logit"/>
    /// <param name="probability">The probability in the range [0, 1].</param>
    /// <returns>The odds of the specified probablility in the range [-infinity, +infinity].</returns>
    [System.Diagnostics.Contracts.Pure]
    public static double OddsRatio(double probability)
      => probability / (1 - probability);

    /// <summary>Returns the probability that specified event count in a group of total event count are all different (or unique). This is the computation P(A').</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/>
    /// <returns>The probability, which is in the range [0, 1].</returns>
    [System.Diagnostics.Contracts.Pure]
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
    [System.Diagnostics.Contracts.Pure]
    public static Probability OfDuplicates(System.Numerics.BigInteger whenCount, System.Numerics.BigInteger ofTotalCount)
      => new(1 - OfNoDuplicates(whenCount, ofTotalCount).m_probability);

    /// <summary>Computes the odds (p / (1 - p)) ratio of the probability.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Odds"/>
    [System.Diagnostics.Contracts.Pure]
    public Ratio ToOdds()
      => new(m_probability, 1 - m_probability);
    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Probability v)
      => v.m_probability;
    public static explicit operator Probability(double v)
      => new(v);

    public static bool operator <(Probability a, Probability b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(Probability a, Probability b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(Probability a, Probability b)
      => a.CompareTo(b) > 0;
    public static bool operator >=(Probability a, Probability b)
      => a.CompareTo(b) >= 0;

    public static bool operator ==(Probability a, Probability b)
      => a.Equals(b);
    public static bool operator !=(Probability a, Probability b)
      => !a.Equals(b);

    public static Probability operator -(Probability v)
      => new(-v.m_probability);
    public static Probability operator +(Probability a, double b)
      => new(a.m_probability + b);
    public static Probability operator +(Probability a, Probability b)
      => a + b.m_probability;
    public static Probability operator /(Probability a, double b)
      => new(a.m_probability / b);
    public static Probability operator /(Probability a, Probability b)
      => a / b.m_probability;
    public static Probability operator *(Probability a, double b)
      => new(a.m_probability * b);
    public static Probability operator *(Probability a, Probability b)
      => a * b.m_probability;
    public static Probability operator %(Probability a, double b)
      => new(a.m_probability % b);
    public static Probability operator %(Probability a, Probability b)
      => a % b.m_probability;
    public static Probability operator -(Probability a, double b)
      => new(a.m_probability - b);
    public static Probability operator -(Probability a, Probability b)
      => a - b.m_probability;
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    [System.Diagnostics.Contracts.Pure] public int CompareTo(Probability other) => m_probability.CompareTo(other.m_probability);

    #region IConvertible
    [System.Diagnostics.Contracts.Pure] public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    [System.Diagnostics.Contracts.Pure] public bool ToBoolean(System.IFormatProvider? provider) => m_probability != 0;
    [System.Diagnostics.Contracts.Pure] public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(m_probability);
    [System.Diagnostics.Contracts.Pure] public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(m_probability);
    [System.Diagnostics.Contracts.Pure] public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(m_probability);
    [System.Diagnostics.Contracts.Pure] public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(m_probability);
    [System.Diagnostics.Contracts.Pure] public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(m_probability);
    [System.Diagnostics.Contracts.Pure] public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(m_probability);
    [System.Diagnostics.Contracts.Pure] public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(m_probability);
    [System.Diagnostics.Contracts.Pure] public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(m_probability);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(m_probability);
    [System.Diagnostics.Contracts.Pure] public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(m_probability);
    [System.Diagnostics.Contracts.Pure] public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", m_probability);
    [System.Diagnostics.Contracts.Pure] public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(m_probability, conversionType, provider);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(m_probability);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(m_probability);
    [System.CLSCompliant(false)][System.Diagnostics.Contracts.Pure] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(m_probability);
    #endregion IConvertible

    // IEquatable
    [System.Diagnostics.Contracts.Pure] public bool Equals(Probability other) => m_probability == other.m_probability;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is Probability o && Equals(o);
    public override int GetHashCode()
      => m_probability.GetHashCode();
    public override string ToString()
      => $"{GetType().Name} {{ Value = {m_probability} }}";
    #endregion Object overrides
  }
}
