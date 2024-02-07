namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.SurfaceTensionUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.SurfaceTensionUnit.NewtonPerMeter => "N/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
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

      #region Static methods

      public static SurfaceTension From(Force force, Length length)
        => new(force.Value / length.Value);

      public static SurfaceTension From(Energy energy, Area area)
        => new(energy.Value / area.Value);
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(SurfaceTension v) => v.m_value;
      public static explicit operator SurfaceTension(double v) => new(v);

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
      public string ToString(string? format, System.IFormatProvider? formatProvider) => ToValueString(QuantifiableValueStringOptions.Default with { Format = format, FormatProvider = formatProvider });

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(SurfaceTensionUnit.NewtonPerMeter, options);

      /// <summary>
      /// <para>The unit of the <see cref="SurfaceTension.Value"/> property is in <see cref="SurfaceTensionUnit.NewtonPerMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(SurfaceTensionUnit unit)
        => unit switch
        {
          SurfaceTensionUnit.NewtonPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(SurfaceTensionUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
