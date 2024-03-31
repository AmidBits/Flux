namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.EnplethyUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.EnplethyUnit.Mole => preferUnicode ? "\u33D6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum EnplethyUnit
    {
      Mole,
    }

    /// <summary>
    /// <para>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Amount_of_substance"/></para>
    /// </summary>
    public readonly record struct Enplethy
      : System.IComparable, System.IComparable<Enplethy>, System.IFormattable, IMetricMultiplicable<double, EnplethyUnit>, IUnitValueQuantifiable<double, EnplethyUnit>
    {
      /// <summary>The exact number of elementary entities in one mole.</summary>
      public static readonly double AvagadroNumber = 6.02214076e23;

      /// <summary>The dimension of the Avagadro constant is the reciprocal of amount of substance.</summary>
      public static readonly Enplethy AvagadroConstant = new(1 / AvagadroNumber);

      private readonly double m_value;

      public Enplethy(double value, EnplethyUnit unit = EnplethyUnit.Mole)
        => m_value = unit switch
        {
          EnplethyUnit.Mole => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>
      /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="EnplethyUnit.Mole"/>, e.g. <see cref="MetricPrefix.Yocto"/> for yoctomoles.</para>
      /// </summary>
      /// <param name="moles"></param>
      /// <param name="prefix"></param>
      public Enplethy(double moles, MetricPrefix prefix) => m_value = prefix.Convert(moles, MetricPrefix.NoPrefix);

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(Enplethy a, Enplethy b) => a.CompareTo(b) < 0;
      public static bool operator <=(Enplethy a, Enplethy b) => a.CompareTo(b) <= 0;
      public static bool operator >(Enplethy a, Enplethy b) => a.CompareTo(b) > 0;
      public static bool operator >=(Enplethy a, Enplethy b) => a.CompareTo(b) >= 0;

      public static Enplethy operator -(Enplethy v) => new(-v.Value);
      public static Enplethy operator +(Enplethy a, double b) => new(a.m_value + b);
      public static Enplethy operator +(Enplethy a, Enplethy b) => a + b.m_value;
      public static Enplethy operator /(Enplethy a, double b) => new(a.m_value / b);
      public static Enplethy operator /(Enplethy a, Enplethy b) => a / b.m_value;
      public static Enplethy operator *(Enplethy a, double b) => new(a.m_value * b);
      public static Enplethy operator *(Enplethy a, Enplethy b) => a * b.m_value;
      public static Enplethy operator %(Enplethy a, double b) => new(a.m_value % b);
      public static Enplethy operator %(Enplethy a, Enplethy b) => a % b.m_value;
      public static Enplethy operator -(Enplethy a, double b) => new(a.m_value - b);
      public static Enplethy operator -(Enplethy a, Enplethy b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Enplethy o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Enplethy other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(EnplethyUnit.Mole, format, formatProvider);

      //IMetricMultiplicable<>
      public double GetMetricValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToMetricValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetMetricValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(prefix.GetUnitString(preferUnicode, useFullName));
        sb.Append(LengthUnit.Metre.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Enplethy.Value"/> property is in <see cref="EnplethyUnit.Mole"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(EnplethyUnit unit)
        => unit switch
        {
          EnplethyUnit.Mole => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(EnplethyUnit unit = EnplethyUnit.Mole, string? format = null, System.IFormatProvider? formatProvider = null, bool preferUnicode = false, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
