namespace Flux
{
  public static partial class Em
  {
    public static string GetUnitString(this Units.InductanceUnit unit, QuantifiableValueStringOptions options = default)
      => options.UseFullName ? unit.ToString() : unit switch
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
      : System.IComparable, System.IComparable<Inductance>, IUnitValueQuantifiable<double, InductanceUnit>
    {
      public const InductanceUnit DefaultUnit = InductanceUnit.Henry;

      private readonly double m_value;

      public Inductance(double value, InductanceUnit unit = DefaultUnit)
        => m_value = unit switch
        {
          InductanceUnit.Henry => value,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Overloaded operators
      public static explicit operator double(Inductance v) => v.m_value;
      public static explicit operator Inductance(double v) => new(v);

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

      // IQuantifiable<>
      public string ToValueString(QuantifiableValueStringOptions options = default) => ToUnitValueString(DefaultUnit, options);

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

      public string ToUnitValueString(InductanceUnit unit, QuantifiableValueStringOptions options = default)
        => $"{string.Format(options.CultureInfo, $"{{0{(options.Format is null ? string.Empty : $":{options.Format}")}}}", GetUnitValue(unit))} {unit.GetUnitString(options)}";

      #endregion Implemented interfaces

      public override string ToString() => ToValueString();
    }
  }
}
