namespace Flux
{
  public static partial class Em
  {
    public static double Convert(this Quantities.BinaryPrefix source, double value, Quantities.BinaryPrefix target) => value * System.Math.Pow(10, (int)source - (int)target);

    public static double GetUnitFactor(this Quantities.BinaryPrefix source) => System.Math.Pow(2, (int)source);

    public static string GetUnitString(this Quantities.BinaryPrefix source, bool useFullName)
      => useFullName ? source.ToString() : source switch
      {
        Quantities.BinaryPrefix.Count => string.Empty,
        Quantities.BinaryPrefix.Kibi => "Ki",
        Quantities.BinaryPrefix.Mebi => "Mi",
        Quantities.BinaryPrefix.Gibi => "Gi",
        Quantities.BinaryPrefix.Tebi => "Ti",
        Quantities.BinaryPrefix.Pebi => "Pi",
        Quantities.BinaryPrefix.Exbi => "Ei",
        Quantities.BinaryPrefix.Zebi => "Zi",
        Quantities.BinaryPrefix.Yobi => "Yi",
        _ => string.Empty,
      };
  }

  namespace Quantities
  {
    public enum BinaryPrefix
    {
      /// <summary>Represents a value that is not a metric multiple. A.k.a. One.</summary>
      Count = 0,
      /// <summary>A.k.a. kiloByte.</summary>
      Kibi = 10,
      /// <summary>A.k.a. megaByte.</summary>
      Mebi = 20,
      /// <summary>A.k.a. gigaByte.</summary>
      Gibi = 30,
      /// <summary>A.k.a. teraByte.</summary>
      Tebi = 40,
      /// <summary>A.k.a. petaByte.</summary>
      Pebi = 50,
      /// <summary>A.k.a. exaByte.</summary>
      Exbi = 60,
      /// <summary>A.k.a. zettaByte.</summary>
      Zebi = 70,
      /// <summary>A.k.a. yottaByte.</summary>
      Yobi = 80,
    }

    //  /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    //  /// <see href="https://en.wikipedia.org/wiki/Metric_prefix"/>
    //  public readonly record struct BinaryMultiplicative
    //    : System.IComparable, System.IComparable<BinaryMultiplicative>, System.IFormattable, IUnitValueQuantifiable<double, BinaryMultiplicativePrefix>
    //  {
    //    private readonly double m_value;

    //    /// <summary>Creates a new instance of this type.</summary>
    //    /// <param name="value">The value to represent.</param>
    //    /// <param name="multiplicativePrefix">The metric multiplicative prefix of the specified value.</param>
    //    public BinaryMultiplicative(double value, BinaryMultiplicativePrefix multiplicativePrefix) => m_value = value * multiplicativePrefix.GetUnitFactor();

    //    #region Static methods

    //    public static double Convert(double value, BinaryMultiplicativePrefix from, BinaryMultiplicativePrefix to) => value * System.Math.Pow(10, (int)from - (int)to);

    //    public static BinaryMultiplicativePrefix FindMetricMultiplicativePrefix(double value, out double outValue, BinaryMultiplicativePrefix prefix = BinaryMultiplicativePrefix.One)
    //    {
    //      var sourceFactor = (int)prefix;
    //      var target = (BinaryMultiplicativePrefix)System.Convert.ToInt64((long)Radix.DigitCount(new System.Numerics.BigInteger(System.Math.Truncate(value)), 2) / 3 * 3 + sourceFactor);
    //      var targetFactor = (int)target;

    //      outValue = value / System.Math.Pow(2, targetFactor - sourceFactor);

    //      return target;
    //    }

    //    #endregion // Static methods

    //    #region Overloaded operators
    //    public static explicit operator double(BinaryMultiplicative v) => v.Value;
    //    public static explicit operator BinaryMultiplicative(double v) => new(v, BinaryMultiplicativePrefix.One);

    //    public static bool operator <(BinaryMultiplicative a, BinaryMultiplicative b) => a.CompareTo(b) < 0;
    //    public static bool operator <=(BinaryMultiplicative a, BinaryMultiplicative b) => a.CompareTo(b) <= 0;
    //    public static bool operator >(BinaryMultiplicative a, BinaryMultiplicative b) => a.CompareTo(b) > 0;
    //    public static bool operator >=(BinaryMultiplicative a, BinaryMultiplicative b) => a.CompareTo(b) >= 0;

    //    public static BinaryMultiplicative operator -(BinaryMultiplicative v) => new(-v.m_value, BinaryMultiplicativePrefix.One);
    //    public static BinaryMultiplicative operator +(BinaryMultiplicative a, double b) => new(a.m_value + b, BinaryMultiplicativePrefix.One);
    //    public static BinaryMultiplicative operator +(BinaryMultiplicative a, BinaryMultiplicative b) => a + b.m_value;
    //    public static BinaryMultiplicative operator /(BinaryMultiplicative a, double b) => new(a.m_value / b, BinaryMultiplicativePrefix.One);
    //    public static BinaryMultiplicative operator /(BinaryMultiplicative a, BinaryMultiplicative b) => a / b.m_value;
    //    public static BinaryMultiplicative operator *(BinaryMultiplicative a, double b) => new(a.m_value * b, BinaryMultiplicativePrefix.One);
    //    public static BinaryMultiplicative operator *(BinaryMultiplicative a, BinaryMultiplicative b) => a * b.m_value;
    //    public static BinaryMultiplicative operator %(BinaryMultiplicative a, double b) => new(a.m_value % b, BinaryMultiplicativePrefix.One);
    //    public static BinaryMultiplicative operator %(BinaryMultiplicative a, BinaryMultiplicative b) => a % b.m_value;
    //    public static BinaryMultiplicative operator -(BinaryMultiplicative a, double b) => new(a.m_value - b, BinaryMultiplicativePrefix.One);
    //    public static BinaryMultiplicative operator -(BinaryMultiplicative a, BinaryMultiplicative b) => a - b.m_value;
    //    #endregion Overloaded operators

    //    #region Implemented interfaces

    //    // IComparable
    //    public int CompareTo(object? other) => other is not null && other is BinaryMultiplicative o ? CompareTo(o) : -1;

    //    // IComparable<>
    //    public int CompareTo(BinaryMultiplicative other) => m_value.CompareTo(other.m_value);

    //    // IFormattable
    //    public string ToString(string? format, System.IFormatProvider? formatProvider) => m_value.ToString(format, formatProvider);

    //    // IQuantifiable<>
    //    public string ToValueString(TextOptions options = default)
    //      => ToUnitValueString(BinaryMultiplicativePrefix.One, options);

    //    public double Value => m_value;

    //    // IUnitQuantifiable<>
    //    public double GetUnitValue(BinaryMultiplicativePrefix multiplicativePrefix)
    //      => m_value / multiplicativePrefix.GetUnitFactor();

    //    public string ToUnitValueString(BinaryMultiplicativePrefix multiplicativePrefix, TextOptions options = default)
    //      => $"{string.Format($"{{0{(options.Format is null ? string.Empty : $":format")}}}", GetUnitValue(multiplicativePrefix))}{(multiplicativePrefix.GetUnitString(false) is var prefix && prefix.Length > 0 ? $" {prefix}" : string.Empty)}";

    //    #endregion Implemented interfaces

    //    public override string ToString() => ToValueString();
    //  }
  }
}
