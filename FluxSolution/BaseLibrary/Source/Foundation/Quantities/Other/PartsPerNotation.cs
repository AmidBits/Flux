namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetUnitSymbol(this Quantity.PartsPerNotationUnit unit)
      => unit switch
      {
        Quantity.PartsPerNotationUnit.Hundred => Quantity.PartsPerNotation.PercentSymbol.ToString(),
        Quantity.PartsPerNotationUnit.Thousand => Quantity.PartsPerNotation.PermilleSymbol.ToString(),
        Quantity.PartsPerNotationUnit.TenThousand => Quantity.PartsPerNotation.PermyriadSymbol.ToString(),
        Quantity.PartsPerNotationUnit.HundredThousand => @" pcm",
        Quantity.PartsPerNotationUnit.Million => @" ppm",
        Quantity.PartsPerNotationUnit.Billion => @" ppb",
        Quantity.PartsPerNotationUnit.Trillion => @" ppt",
        Quantity.PartsPerNotationUnit.Quadrillion => @" ppq",
        _ => string.Empty,
      };
  }

  namespace Quantity
  {
    public enum PartsPerNotationUnit
    {
      /// <summary>Percent.</summary>
      Hundred,
      /// <summary>Permille.</summary>
      Thousand,
      /// <summary>Permyriad.</summary>
      TenThousand,
      /// <summary>Per cent mille, abbreviated "pcm".</summary>
      HundredThousand,
      /// <summary>Abbreviated "ppm".</summary>
      Million,
      /// <summary>Abbreviated "ppb".</summary>
      Billion,
      /// <summary>Abbreviated "ppt".</summary>
      Trillion,
      /// <summary>Abbreviated "ppq".</summary>
      Quadrillion,
    }

    /// <summary>Parts per notation. In science and engineering, the parts-per notation is a set of pseudo-units to describe small values of miscellaneous dimensionless quantities, e.g. mole fraction or mass fraction. Since these fractions are quantity-per-quantity measures, they are pure numbers with no associated units of measurement.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Parts-per_notation"/>
    public struct PartsPerNotation
      : System.IComparable<PartsPerNotation>, System.IEquatable<PartsPerNotation>, IValuedUnit<double>
    {
      public const char PercentSymbol = '\u0025';
      public const char PermilleSymbol = '\u2030';
      public const char PermyriadSymbol = '\u2031';

      private readonly double m_parts;
      private readonly PartsPerNotationUnit m_unit;

      /// <summary>Creates a new instance of this type.</summary>
      /// <param name="parts">The parts in parts per notation.</param>
      /// <param name="unit">The notation in parts per notation.</param>
      public PartsPerNotation(double parts, PartsPerNotationUnit unit = PartsPerNotationUnit.Hundred)
      {
        m_parts = unit switch
        {
          PartsPerNotationUnit.Hundred => parts / 1e2,
          PartsPerNotationUnit.Thousand => parts / 1e3,
          PartsPerNotationUnit.TenThousand => parts / 1e4,
          PartsPerNotationUnit.HundredThousand => parts / 1e5,
          PartsPerNotationUnit.Million => parts / 1e6,
          PartsPerNotationUnit.Billion => parts / 1e9,
          PartsPerNotationUnit.Trillion => parts / 1e12,
          PartsPerNotationUnit.Quadrillion => parts / 1e15,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

        m_unit = unit;
      }

      public double Value
        => m_parts;

      public double ToUnitValue(PartsPerNotationUnit unit = PartsPerNotationUnit.Hundred)
        => unit switch
        {
          PartsPerNotationUnit.Hundred => m_parts * 1e2,
          PartsPerNotationUnit.Thousand => m_parts * 1e3,
          PartsPerNotationUnit.TenThousand => m_parts * 1e4,
          PartsPerNotationUnit.HundredThousand => m_parts * 1e5,
          PartsPerNotationUnit.Million => m_parts * 1e6,
          PartsPerNotationUnit.Billion => m_parts * 1e9,
          PartsPerNotationUnit.Trillion => m_parts * 1e12,
          PartsPerNotationUnit.Quadrillion => m_parts * 1e15,
          _ => throw new System.ArgumentOutOfRangeException(nameof(unit)),
        };

      #region Static methods
      #endregion Static methods

      #region Overloaded operators
      public static explicit operator double(PartsPerNotation v)
        => v.Value;

      public static bool operator <(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) < 0;
      public static bool operator <=(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) <= 0;
      public static bool operator >(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) > 0;
      public static bool operator >=(PartsPerNotation a, PartsPerNotation b)
        => a.CompareTo(b) >= 0;

      public static bool operator ==(PartsPerNotation a, PartsPerNotation b)
        => a.Equals(b);
      public static bool operator !=(PartsPerNotation a, PartsPerNotation b)
        => !a.Equals(b);

      public static PartsPerNotation operator -(PartsPerNotation v)
        => new(-v.m_parts);
      public static PartsPerNotation operator +(PartsPerNotation a, double b)
        => new(a.m_parts + b);
      public static PartsPerNotation operator +(PartsPerNotation a, PartsPerNotation b)
        => a + b.m_parts;
      public static PartsPerNotation operator /(PartsPerNotation a, double b)
        => new(a.m_parts / b);
      public static PartsPerNotation operator /(PartsPerNotation a, PartsPerNotation b)
        => a / b.m_parts;
      public static PartsPerNotation operator *(PartsPerNotation a, double b)
        => new(a.m_parts * b);
      public static PartsPerNotation operator *(PartsPerNotation a, PartsPerNotation b)
        => a * b.m_parts;
      public static PartsPerNotation operator %(PartsPerNotation a, double b)
        => new(a.m_parts % b);
      public static PartsPerNotation operator %(PartsPerNotation a, PartsPerNotation b)
        => a % b.m_parts;
      public static PartsPerNotation operator -(PartsPerNotation a, double b)
        => new(a.m_parts - b);
      public static PartsPerNotation operator -(PartsPerNotation a, PartsPerNotation b)
        => a - b.m_parts;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable
      public int CompareTo(PartsPerNotation other)
        => m_parts.CompareTo(other.m_parts);

      // IEquatable
      public bool Equals(PartsPerNotation other)
        => m_parts == other.m_parts;
      #endregion Implemented interfaces

      #region Object overrides
      public override bool Equals(object? obj)
        => obj is PartsPerNotation o && Equals(o);
      public override int GetHashCode()
        => System.HashCode.Combine(m_parts);
      public override string ToString()
        => $"{GetType().Name} {{ Value = {ToUnitValue(m_unit)}{m_unit.GetUnitSymbol()} }}";
      #endregion Object overrides
    }
  }
}
