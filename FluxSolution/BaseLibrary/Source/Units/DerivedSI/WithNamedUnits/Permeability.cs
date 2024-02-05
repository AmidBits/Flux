namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.PermeabilityUnit unit, QuantifiableValueStringOptions options)
      => options.UseFullName ? unit.ToString() : unit switch
      {
        Units.PermeabilityUnit.HenryPerMeter => "H/m",
        _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
      };
  }

  namespace Units
  {
    public enum PermeabilityUnit
    {
      /// <summary>This is the default unit for <see cref="Permeability"/>.</summary>
      HenryPerMeter,
    }

    /// <summary>Force, unit of newton. This is an SI derived quantity.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Force"/>
    public readonly record struct Permeability
      : System.IComparable, System.IComparable<Permeability>, IUnitValueQuantifiable<double, PermeabilityUnit>
    {
      public const PermeabilityUnit DefaultUnit = PermeabilityUnit.HenryPerMeter;

      private readonly double m_value;

      public Permeability(double value, PermeabilityUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          PermeabilityUnit.HenryPerMeter => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Permeability v) => v.m_value;
      public static explicit operator Permeability(double v) => new(v);

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

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options) => ToUnitValueString(DefaultUnit, options);

      /// <summary>
      /// <para>The unit of the <see cref="Permeability.Value"/> property is in <see cref="PermeabilityUnit.HenryPerMeter"/>.</para>
      /// </summary>
      public double Value => m_value;

      // IUnitQuantifiable<>
      public double GetUnitValue(PermeabilityUnit unit)
        => unit switch
        {
          PermeabilityUnit.HenryPerMeter => m_value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      public string ToUnitValueString(PermeabilityUnit unit, QuantifiableValueStringOptions options)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString(QuantifiableValueStringOptions.Default);
    }
  }
}
