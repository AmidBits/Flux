namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.ImpulseUnit unit, bool preferUnicode = false, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.ImpulseUnit.NewtonSecond => preferUnicode ? "N\u22C5s" : "N�s",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
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

      public Impulse(Force force, Time time) : this(force.Value / time.Value) { }

      #region Static methods

      #endregion // Static methods

      #region Overloaded operators

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
        => ToUnitValueString(ImpulseUnit.NewtonSecond, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="Impulse.Value"/> property is in <see cref="ImpulseUnit.NewtonSecond"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(ImpulseUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(preferUnicode, useFullName);

      public double GetUnitValue(ImpulseUnit unit)
        => unit switch
        {
          ImpulseUnit.NewtonSecond => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(ImpulseUnit unit = ImpulseUnit.NewtonSecond, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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