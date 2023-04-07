namespace Flux
{
  namespace Quantities
  {
    public enum RatioFormat
    {
      AtoB,
      AcolonB,
    }

    /// <summary>A ratio indicates how many times one number contains another. It is two related quantities measured with the same unit, it is a dimensionless number (value). This struct stores both constituting numbers of the ratio (numerator and denominator) and returns the quotient as a value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Ratio"/>
    public readonly record struct Ratio
      : IQuantifiable<double>
    {
      private readonly double m_numerator;
      private readonly double m_denominator;

      public Ratio(double numerator, double denominator)
      {
        m_numerator = numerator;
        m_denominator = denominator;
      }
      /// <summary>Use a pre-computed ratio. This is less accurate because losses may already have been accumulated, and the ratio is no longer the original numerator and denominator.</summary>
      public Ratio(double ratio) : this(ratio, 1) { }

      public double Numerator => m_numerator;
      public double Denominator => m_denominator;

      /// <summary>If a diagonal length is known, the proportional width and height can be computed using the Pythagorean theorem.</summary>
      /// <param name="diagonalLength">The length of the known diagonal.</param>
      /// <returns>The proportional (to the arguments passed) lengths of width and height.</returns>
      public Flux.Numerics.CartesianCoordinate2<double> ToSize(double diagonalLength) => (Numerics.CartesianCoordinate2<double>)ToSize(diagonalLength, m_numerator, m_denominator);

      public string ToRatioString(RatioFormat format)
        => format switch
        {
          RatioFormat.AcolonB => $"{m_numerator}\u2236{m_denominator}",
          RatioFormat.AtoB => $"{m_numerator} to {m_denominator}",
          _ => throw new System.ArgumentOutOfRangeException(nameof(format))
        };

      #region Static methods

      /// <summary>When the diagonal length and side-to-side ratio is known, the proportional width and height can be computed using the Pythagorean theorem. E.g. A diagonal of 65" and a ratio of 16:9.</summary>
      /// <param name="diagonalLength">The length of the known diagonal. E.g. 65.</param>
      /// <param name="ratioX">The x-axis portion of a ratio, which corresponds to width in the result. E.g. 16.</param>
      /// <param name="ratioY">The y-axis portion of a ratio, which corresponds to height in the result. E.g. 9.</param>
      /// <returns>The proportional (to the arguments passed) lengths of width (e.g. 56.6524099130957) and height (e.g. 31.866980576116333).</returns>
      public static (double width, double height) ToSize(double diagonalLength, double ratioX, double ratioY)
      {
        var m = double.Sqrt(ratioX * ratioX + ratioY * ratioY);

        return new(diagonalLength * ratioX / m, diagonalLength * ratioY / m);
      }
      /// <summary>When the diagonal length and side-to-side ratio is known, the proportional width and height can be computed using the Pythagorean theorem. E.g. A diagonal of 65" and a ratio of 16:9.</summary>
      /// <param name="diagonalLength">The length of the known diagonal. E.g. 65.</param>
      /// <param name="ratio">The pre-computed ratio, e.g. if the two parts of a ratio is unknown, which then corresponds to width AND height in the result. E.g. 1.777777777777778 (16/9).</param>
      /// <returns>The proportional (to the arguments passed) lengths of width and height.</returns>
      public static (double width, double height) ToSize(double diagonalLength, double ratio) => ToSize(diagonalLength, ratio, 1);

      #endregion // Static methods

      #region Overloaded operators
      public static explicit operator double(Ratio v) => v.Value;
      #endregion Overloaded operators

      #region Implemented interfaces

      // IQuantifiable<>
      public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false) => $"{m_numerator} / {m_denominator} ({Value})";
      public double Value => m_numerator / m_denominator;

      #endregion Implemented interfaces

      public override string ToString() => ToQuantityString();
    }
  }
}
