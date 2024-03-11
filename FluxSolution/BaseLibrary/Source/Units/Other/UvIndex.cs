using Flux.Units;

namespace Flux
{
  #region ExtensionMethods

  public static partial class Em
  {
    public static System.Drawing.Color GetMediaGraphicColor(this Units.UvIndexRisk source)
      => source switch
      {
        UvIndexRisk.Low => System.Drawing.Color.Green,
        UvIndexRisk.Moderate => System.Drawing.Color.Yellow,
        UvIndexRisk.High => System.Drawing.Color.Orange,
        UvIndexRisk.VeryHigh => System.Drawing.Color.Red,
        UvIndexRisk.Extreme => System.Drawing.Color.Violet,
        _ => throw new System.ArgumentOutOfRangeException(nameof(source))
      };
  }

  #endregion // ExtensionMethods

  namespace Units
  {
    public enum UvIndexRisk
    {
      Low,
      Moderate,
      High,
      VeryHigh,
      Extreme,
    }

    /// <summary>
    /// <para>UV index, unit of itself.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Ultraviolet_index"/></para>
    /// </summary>
    public readonly record struct UvIndex
    : System.IComparable, System.IComparable<UvIndex>, System.IFormattable, IValueQuantifiable<double>
    {
      private readonly double m_value;

      public UvIndex(double value) => m_value = value >= 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value));

      public UvIndexRisk Risk
      {
        get => m_value switch
        {
          >= 0 and < 3 => UvIndexRisk.Low,
          >= 3 and < 6 => UvIndexRisk.Moderate,
          >= 6 and < 8 => UvIndexRisk.High,
          >= 8 and < 11 => UvIndexRisk.VeryHigh,
          >= 11 => UvIndexRisk.Extreme,
          _ => throw new NotImplementedException(),
        };
      }

      #region Overloaded operators

      public static bool operator <(UvIndex a, UvIndex b) => a.CompareTo(b) < 0;
      public static bool operator <=(UvIndex a, UvIndex b) => a.CompareTo(b) <= 0;
      public static bool operator >(UvIndex a, UvIndex b) => a.CompareTo(b) > 0;
      public static bool operator >=(UvIndex a, UvIndex b) => a.CompareTo(b) >= 0;

      public static UvIndex operator -(UvIndex v) => new(-v.m_value);
      public static UvIndex operator +(UvIndex a, double b) => new(a.m_value + b);
      public static UvIndex operator +(UvIndex a, UvIndex b) => a + b.m_value;
      public static UvIndex operator /(UvIndex a, double b) => new(a.m_value / b);
      public static UvIndex operator /(UvIndex a, UvIndex b) => a / b.m_value;
      public static UvIndex operator *(UvIndex a, double b) => new(a.m_value * b);
      public static UvIndex operator *(UvIndex a, UvIndex b) => a * b.m_value;
      public static UvIndex operator %(UvIndex a, double b) => new(a.m_value % b);
      public static UvIndex operator %(UvIndex a, UvIndex b) => a % b.m_value;
      public static UvIndex operator -(UvIndex a, double b) => new(a.m_value - b);
      public static UvIndex operator -(UvIndex a, UvIndex b) => a - b.m_value;

      #endregion Overloaded operators

      #region Implemented interfaces

      // IComparable
      public int CompareTo(object? other) => other is not null && other is UvIndex o ? CompareTo(o) : -1;

      // IComparable<>
      public int CompareTo(UvIndex other) => m_value.CompareTo(other.m_value);

      // IFormattable
      public string ToString(string? format, System.IFormatProvider? formatProvider)
        => $"UV Index {m_value.ToString(format ?? "N1", formatProvider)}";

      // IQuantifiable<>
      /// <summary>
      /// <para>The <see cref="UvIndex.Value"/> property is the ultraviolet index.</para>
      /// </summary>
      public double Value => m_value;

      #endregion Implemented interfaces

      public override string ToString() => ToString(null, null);
    }
  }
}
