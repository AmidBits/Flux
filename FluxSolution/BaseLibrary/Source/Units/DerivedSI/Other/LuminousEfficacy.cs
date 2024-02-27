namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.LuminousEfficacyUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Units.LuminousEfficacyUnit.LumensPerWatt => "lm/W",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum LuminousEfficacyUnit
    {
      /// <summary>This is the default unit for <see cref="LuminousEfficacy"/>.</summary>
      LumensPerWatt,
    }

    /// <summary>Torque unit of newton meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Torque"/>
    public readonly record struct LuminousEfficacy
      : System.IComparable, System.IComparable<LuminousEfficacy>, System.IFormattable, IUnitValueQuantifiable<double, LuminousEfficacyUnit>
    {
      public static readonly LuminousEfficacy LuminousEfficacyOf540THzRadiation = new(683);

      private readonly double m_value;

      public LuminousEfficacy(double value, LuminousEfficacyUnit unit = LuminousEfficacyUnit.LumensPerWatt)
        => m_value = unit switch
        {
          LuminousEfficacyUnit.LumensPerWatt => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods

      public static LuminousEfficacy From(Energy energy, Angle angle)
        => new(energy.Value / angle.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(LuminousEfficacy v) => v.m_value;
      public static explicit operator LuminousEfficacy(double v) => new(v);

      public static bool operator <(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) < 0;
      public static bool operator <=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) <= 0;
      public static bool operator >(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) > 0;
      public static bool operator >=(LuminousEfficacy a, LuminousEfficacy b) => a.CompareTo(b) >= 0;

      public static LuminousEfficacy operator -(LuminousEfficacy v) => new(-v.m_value);
      public static LuminousEfficacy operator +(LuminousEfficacy a, double b) => new(a.m_value + b);
      public static LuminousEfficacy operator +(LuminousEfficacy a, LuminousEfficacy b) => a + b.m_value;
      public static LuminousEfficacy operator /(LuminousEfficacy a, double b) => new(a.m_value / b);
      public static LuminousEfficacy operator /(LuminousEfficacy a, LuminousEfficacy b) => a / b.m_value;
      public static LuminousEfficacy operator *(LuminousEfficacy a, double b) => new(a.m_value * b);
      public static LuminousEfficacy operator *(LuminousEfficacy a, LuminousEfficacy b) => a * b.m_value;
      public static LuminousEfficacy operator %(LuminousEfficacy a, double b) => new(a.m_value % b);
      public static LuminousEfficacy operator %(LuminousEfficacy a, LuminousEfficacy b) => a % b.m_value;
      public static LuminousEfficacy operator -(LuminousEfficacy a, double b) => new(a.m_value - b);
      public static LuminousEfficacy operator -(LuminousEfficacy a, LuminousEfficacy b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is LuminousEfficacy o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(LuminousEfficacy other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(LuminousEfficacyUnit.LumensPerWatt, UnitValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="LuminousEfficacy.Value"/> property is in <see cref="LuminousEfficacyUnit.LumensPerWatt"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(LuminousEfficacyUnit unit)
        => unit switch
        {
          LuminousEfficacyUnit.LumensPerWatt => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(LuminousEfficacyUnit unit, UnitValueStringOptions options = default)
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
