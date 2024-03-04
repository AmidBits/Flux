namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.InductanceUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.InductanceUnit.Henry => "H",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum InductanceUnit
    {
      /// <summary>This is the default unit for <see cref="Inductance"/>.</summary>
      Henry,
    }

    /// <summary>Electrical inductance unit of Henry.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inductance"/>
    public readonly record struct Inductance
      : System.IComparable, System.IComparable<Inductance>, System.IFormattable, IUnitValueQuantifiable<double, InductanceUnit>
    {
      private readonly double m_value;

      public Inductance(double value, InductanceUnit unit = InductanceUnit.Henry)
        => m_value = unit switch
        {
          InductanceUnit.Henry => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators

      public static bool operator <(Inductance a, Inductance b) => a.CompareTo(b) < 0;
      public static bool operator <=(Inductance a, Inductance b) => a.CompareTo(b) <= 0;
      public static bool operator >(Inductance a, Inductance b) => a.CompareTo(b) > 0;
      public static bool operator >=(Inductance a, Inductance b) => a.CompareTo(b) >= 0;

      public static Inductance operator -(Inductance v) => new(-v.m_value);
      public static Inductance operator +(Inductance a, double b) => new(a.m_value + b);
      public static Inductance operator +(Inductance a, Inductance b) => a + b.m_value;
      public static Inductance operator /(Inductance a, double b) => new(a.m_value / b);
      public static Inductance operator /(Inductance a, Inductance b) => a / b.m_value;
      public static Inductance operator *(Inductance a, double b) => new(a.m_value * b);
      public static Inductance operator *(Inductance a, Inductance b) => a * b.m_value;
      public static Inductance operator %(Inductance a, double b) => new(a.m_value % b);
      public static Inductance operator %(Inductance a, Inductance b) => a % b.m_value;
      public static Inductance operator -(Inductance a, double b) => new(a.m_value - b);
      public static Inductance operator -(Inductance a, Inductance b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Inductance o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Inductance other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(InductanceUnit.Henry, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Inductance.Value"/> property is in <see cref="InductanceUnit.Henry"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(InductanceUnit unit)
        => unit switch
        {
          InductanceUnit.Henry => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(InductanceUnit unit, string? format, System.IFormatProvider? formatProvider, bool preferUnicode, UnicodeSpacing unicodeSpacing, bool useFullName)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(format, formatProvider));
        sb.Append(unicodeSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(useFullName));
        return sb.ToString();
      }

      public string ToUnitValueString(InductanceUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
