namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.CatalyticActivityUnit unit, bool preferUnicode, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.CatalyticActivityUnit.Katal => preferUnicode ? "\u33CF" : "kat",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum CatalyticActivityUnit
    {
      /// <summary>This is the default unit for <see cref="CatalyticActivity"/>. Katal = (mol/s).</summary>
      Katal,
    }

    /// <summary>Catalytic activity unit of Katal.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Catalysis"/>
    public readonly record struct CatalyticActivity
      : System.IComparable, System.IComparable<CatalyticActivity>, System.IFormattable, IUnitValueQuantifiable<double, CatalyticActivityUnit>
    {
      private readonly double m_value;

      public CatalyticActivity(double value, CatalyticActivityUnit unit = CatalyticActivityUnit.Katal)
        => m_value = unit switch
        {
          CatalyticActivityUnit.Katal => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) < 0;
      public static bool operator <=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) <= 0;
      public static bool operator >(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) > 0;
      public static bool operator >=(CatalyticActivity a, CatalyticActivity b) => a.CompareTo(b) >= 0;

      public static CatalyticActivity operator -(CatalyticActivity v) => new(-v.m_value);
      public static CatalyticActivity operator +(CatalyticActivity a, double b) => new(a.m_value + b);
      public static CatalyticActivity operator +(CatalyticActivity a, CatalyticActivity b) => a + b.m_value;
      public static CatalyticActivity operator /(CatalyticActivity a, double b) => new(a.m_value / b);
      public static CatalyticActivity operator /(CatalyticActivity a, CatalyticActivity b) => a / b.m_value;
      public static CatalyticActivity operator *(CatalyticActivity a, double b) => new(a.m_value * b);
      public static CatalyticActivity operator *(CatalyticActivity a, CatalyticActivity b) => a * b.m_value;
      public static CatalyticActivity operator %(CatalyticActivity a, double b) => new(a.m_value % b);
      public static CatalyticActivity operator %(CatalyticActivity a, CatalyticActivity b) => a % b.m_value;
      public static CatalyticActivity operator -(CatalyticActivity a, double b) => new(a.m_value - b);
      public static CatalyticActivity operator -(CatalyticActivity a, CatalyticActivity b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is CatalyticActivity o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(CatalyticActivity other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(CatalyticActivityUnit.Katal, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="CatalyticActivity.Value"/> property is in <see cref="CatalyticActivityUnit.Katal"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(CatalyticActivityUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(CatalyticActivityUnit unit)
        => unit switch
        {
          CatalyticActivityUnit.Katal => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(CatalyticActivityUnit unit = CatalyticActivityUnit.Katal, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
