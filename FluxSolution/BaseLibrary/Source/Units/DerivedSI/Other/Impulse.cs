namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.ImpulseUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.ImpulseUnit.NewtonSecond => preferUnicode ? "N\u22C5s" : "N·s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum ImpulseUnit
    {
      /// <summary>This is the default unit for <see cref="Impulse"/>.</summary>
      NewtonSecond,
    }

    /// <summary>Impulse, unit of Newton second.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Impulse"/>
    public readonly record struct Impulse
      : System.IComparable, System.IComparable<Impulse>, System.IFormattable, IUnitValueQuantifiable<double, ImpulseUnit>
    {
      private readonly double m_value;

      public Impulse(double value, ImpulseUnit unit = ImpulseUnit.NewtonSecond)
        => m_value = unit switch
        {
          ImpulseUnit.NewtonSecond => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static Impulse From(Force force, Time time)
        => new(force.Value / time.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(Impulse v) => v.m_value;
      public static explicit operator Impulse(double v) => new(v);

      public static bool operator <(Impulse a, Impulse b) => a.CompareTo(b) < 0;
      public static bool operator <=(Impulse a, Impulse b) => a.CompareTo(b) <= 0;
      public static bool operator >(Impulse a, Impulse b) => a.CompareTo(b) > 0;
      public static bool operator >=(Impulse a, Impulse b) => a.CompareTo(b) >= 0;

      public static Impulse operator -(Impulse v) => new(-v.m_value);
      public static Impulse operator +(Impulse a, double b) => new(a.m_value + b);
      public static Impulse operator +(Impulse a, Impulse b) => a + b.m_value;
      public static Impulse operator /(Impulse a, double b) => new(a.m_value / b);
      public static Impulse operator /(Impulse a, Impulse b) => a / b.m_value;
      public static Impulse operator *(Impulse a, double b) => new(a.m_value * b);
      public static Impulse operator *(Impulse a, Impulse b) => a * b.m_value;
      public static Impulse operator %(Impulse a, double b) => new(a.m_value % b);
      public static Impulse operator %(Impulse a, Impulse b) => a % b.m_value;
      public static Impulse operator -(Impulse a, double b) => new(a.m_value - b);
      public static Impulse operator -(Impulse a, Impulse b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is Impulse o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(Impulse other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(ImpulseUnit.NewtonSecond, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Impulse.Value"/> property is in <see cref="ImpulseUnit.NewtonSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(ImpulseUnit unit)
        => unit switch
        {
          ImpulseUnit.NewtonSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ImpulseUnit unit, UnitValueStringOptions options = default)
      {
        var sb = new System.Text.StringBuilder();
        sb.Append(GetUnitValue(unit).ToString(options.Format, options.FormatProvider));
        sb.Append(options.UnitSpacing.ToSpacingString());
        sb.Append(unit.GetUnitString(options.PreferUnicode, options.UseFullName));
        return sb.ToString();
      }

      #endregion Implemented interfaces
    }
  }
}
