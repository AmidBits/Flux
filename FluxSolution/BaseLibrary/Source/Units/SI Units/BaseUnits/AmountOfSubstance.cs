namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.AmountOfSubstanceUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.AmountOfSubstanceUnit.Mole => preferUnicode ? "\u33D6" : "mol",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum AmountOfSubstanceUnit
    {
      Mole,
    }

    /// <summary>
    /// <para>Enplethy, or amount of substance. SI unit of mole. This is a base quantity.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Amount_of_substance"/></para>
    /// </summary>
    public readonly record struct AmountOfSubstance
      : System.IComparable, System.IComparable<AmountOfSubstance>, System.IFormattable, ISiPrefixValueQuantifiable<double, AmountOfSubstanceUnit>
    {
      /// <summary>The exact number of elementary entities in one mole.</summary>
      public static readonly double AvogadroNumber = 6.02214076e23;

      /// <summary>The dimension of the Avagadro constant is the reciprocal of amount of substance.</summary>
      public static readonly AmountOfSubstance AvogadroConstant = new(1 / AvogadroNumber);

      private readonly double m_value;

      public AmountOfSubstance(double value, AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole)
        => m_value = unit switch
        {
          AmountOfSubstanceUnit.Mole => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      /// <summary>
      /// <para>Creates a new instance from the specified <see cref="MetricPrefix"/> (metric multiple) of <see cref="AmountOfSubstanceUnit.Mole"/>, e.g. <see cref="MetricPrefix.Yocto"/> for yoctomoles.</para>
      /// </summary>
      /// <param name="moles"></param>
      /// <param name="prefix"></param>
      public AmountOfSubstance(double moles, MetricPrefix prefix) => m_value = prefix.Convert(moles, MetricPrefix.NoPrefix);

      #region Static methods
      #endregion Static methods

      #region Overloaded operators

      public static bool operator <(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) < 0;
      public static bool operator <=(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) <= 0;
      public static bool operator >(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) > 0;
      public static bool operator >=(AmountOfSubstance a, AmountOfSubstance b) => a.CompareTo(b) >= 0;

      public static AmountOfSubstance operator -(AmountOfSubstance v) => new(-v.Value);
      public static AmountOfSubstance operator +(AmountOfSubstance a, double b) => new(a.m_value + b);
      public static AmountOfSubstance operator +(AmountOfSubstance a, AmountOfSubstance b) => a + b.m_value;
      public static AmountOfSubstance operator /(AmountOfSubstance a, double b) => new(a.m_value / b);
      public static AmountOfSubstance operator /(AmountOfSubstance a, AmountOfSubstance b) => a / b.m_value;
      public static AmountOfSubstance operator *(AmountOfSubstance a, double b) => new(a.m_value * b);
      public static AmountOfSubstance operator *(AmountOfSubstance a, AmountOfSubstance b) => a * b.m_value;
      public static AmountOfSubstance operator %(AmountOfSubstance a, double b) => new(a.m_value % b);
      public static AmountOfSubstance operator %(AmountOfSubstance a, AmountOfSubstance b) => a % b.m_value;
      public static AmountOfSubstance operator -(AmountOfSubstance a, double b) => new(a.m_value - b);
      public static AmountOfSubstance operator -(AmountOfSubstance a, AmountOfSubstance b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is AmountOfSubstance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(AmountOfSubstance other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(AmountOfSubstanceUnit.Mole, format, formatProvider);

      // ISiUnitValueQuantifiable<>
      public AmountOfSubstanceUnit BaseUnit => AmountOfSubstanceUnit.Mole;

      public AmountOfSubstanceUnit UnprefixedUnit => AmountOfSubstanceUnit.Mole;

      public string GetSiPrefixSymbol(MetricPrefix prefix, bool preferUnicode, bool useFullName) => prefix.GetUnitString(preferUnicode, useFullName) + GetUnitSymbol(UnprefixedUnit, preferUnicode, useFullName);

      public double GetSiPrefixValue(MetricPrefix prefix) => MetricPrefix.NoPrefix.Convert(m_value, prefix);

      public string ToSiPrefixValueString(MetricPrefix prefix, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetSiPrefixValue(prefix).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetSiPrefixSymbol(prefix, preferUnicode, useFullName));
        return sb.ToString();
      }

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="AmountOfSubstance.Value"/> property is in <see cref="AmountOfSubstanceUnit.Mole"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(AmountOfSubstanceUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(AmountOfSubstanceUnit unit)
        => unit switch
        {
          AmountOfSubstanceUnit.Mole => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(AmountOfSubstanceUnit unit = AmountOfSubstanceUnit.Mole, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unitSpacing.ToSpacingString());
        sb.Append(GetUnitSymbol(unit, preferUnicode, useFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
