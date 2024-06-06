namespace Flux
{
  public static partial class Fx
  {
    public static string GetUnitString(this Quantities.SurfaceTensionUnit unit, bool useFullName = false)
      => useFullName ? unit.ToString() : unit switch
      {
        Quantities.SurfaceTensionUnit.NewtonPerMeter => "N/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Quantities
  {
    public enum SurfaceTensionUnit
    {
      /// <summary>This is the default unit for <see cref="SurfaceTension"/>.</summary>
      NewtonPerMeter,
    }

    /// <summary>Surface tension, unit of Newton per meter.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Surface_tension"/>
    public readonly record struct SurfaceTension
      : System.IComparable, System.IComparable<SurfaceTension>, System.IFormattable, IUnitValueQuantifiable<double, SurfaceTensionUnit>
    {
      private readonly double m_value;

      public SurfaceTension(double value, SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter)
        => m_value = unit switch
        {
          SurfaceTensionUnit.NewtonPerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public SurfaceTension(Force force, Length length) : this(force.Value / length.Value) { }

      public SurfaceTension(Energy energy, Area area) : this(energy.Value / area.Value) { }

      #region Static methods

      #endregion // Static methods

      #region Overloaded operators

      public static bool operator <(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) < 0;
      public static bool operator <=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) <= 0;
      public static bool operator >(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) > 0;
      public static bool operator >=(SurfaceTension a, SurfaceTension b) => a.CompareTo(b) >= 0;

      public static SurfaceTension operator -(SurfaceTension v) => new(-v.m_value);
      public static SurfaceTension operator +(SurfaceTension a, double b) => new(a.m_value + b);
      public static SurfaceTension operator +(SurfaceTension a, SurfaceTension b) => a + b.m_value;
      public static SurfaceTension operator /(SurfaceTension a, double b) => new(a.m_value / b);
      public static SurfaceTension operator /(SurfaceTension a, SurfaceTension b) => a / b.m_value;
      public static SurfaceTension operator *(SurfaceTension a, double b) => new(a.m_value * b);
      public static SurfaceTension operator *(SurfaceTension a, SurfaceTension b) => a * b.m_value;
      public static SurfaceTension operator %(SurfaceTension a, double b) => new(a.m_value % b);
      public static SurfaceTension operator %(SurfaceTension a, SurfaceTension b) => a % b.m_value;
      public static SurfaceTension operator -(SurfaceTension a, double b) => new(a.m_value - b);
      public static SurfaceTension operator -(SurfaceTension a, SurfaceTension b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is SurfaceTension o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(SurfaceTension other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => ToUnitValueString(SurfaceTensionUnit.NewtonPerMeter, format, formatProvider);

      // IQuantifiable<>
      /// <summary>
      /// <para>The unit of the <see cref="SurfaceTension.Value"/> property is in <see cref="SurfaceTensionUnit.NewtonPerMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public string GetUnitSymbol(SurfaceTensionUnit unit, bool preferUnicode, bool useFullName) => unit.GetUnitString(useFullName);

      public double GetUnitValue(SurfaceTensionUnit unit)
        => unit switch
        {
          SurfaceTensionUnit.NewtonPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SurfaceTensionUnit unit = SurfaceTensionUnit.NewtonPerMeter, string? format = null, System.IFormatProvider? formatProvider = null, UnicodeSpacing unitSpacing = UnicodeSpacing.Space, bool preferUnicode = false, bool useFullName = false)
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
