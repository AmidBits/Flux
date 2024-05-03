namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Quantities.PermeabilityUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.PermeabilityUnit.HenryPerMeter => "H/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum PermeabilityUnit
    {
      /// <summary>This is the default unit for <see cref="Permeability"/>.</summary>
      HenryPerMeter,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct Permeability
      : System.IComparable, System.IComparable<Permeability>, System.IFormattable, IUnitValueQuantifiable<double, PermeabilityUnit>
    {
      private readonly double m_value;

      public Permeability(double value, PermeabilityUnit unit = PermeabilityUnit.HenryPerMeter)
        => m_value = unit switch
        {
          PermeabilityUnit.HenryPerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(Permeability a, Permeability b) => a.CompareTo(b) < 0;
      public static bool operator <=(Permeability a, Permeability b) => a.CompareTo(b) <= 0;
      public static bool operator >(Permeability a, Permeability b) => a.CompareTo(b) > 0;
      public static bool operator >=(Permeability a, Permeability b) => a.CompareTo(b) >= 0;

      public static Permeability operator -(Permeability v) => new(-v.m_value);
      public static Permeability operator +(Permeability a, double b) => new(a.m_value + b);
      public static Permeability operator +(Permeability a, Permeability b) => a + b.m_value;
      public static Permeability operator /(Permeability a, double b) => new(a.m_value / b);
      public static Permeability operator /(Permeability a, Permeability b) => a / b.m_value;
      public static Permeability operator *(Permeability a, double b) => new(a.m_value * b);
      public static Permeability operator *(Permeability a, Permeability b) => a * b.m_value;
      public static Permeability operator %(Permeability a, double b) => new(a.m_value % b);
      public static Permeability operator %(Permeability a, Permeability b) => a % b.m_value;
      public static Permeability operator -(Permeability a, double b) => new(a.m_value - b);
      public static Permeability operator -(Permeability a, Permeability b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Permeability o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Permeability other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(PermeabilityUnit.HenryPerMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Permeability.Value"/> property is in <see cref="PermeabilityUnit.HenryPerMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(PermeabilityUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(PermeabilityUnit unit)
        => unit switch
        {
          PermeabilityUnit.HenryPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PermeabilityUnit unit = PermeabilityUnit.HenryPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
