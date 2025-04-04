namespace Flux.CoordinateSystems
{
  /// <summary>The HECS coordinate system.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Hexagonal_Efficient_Coordinate_System"/>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct HecsCoordinate
    : System.IFormattable
  {
    public static HecsCoordinate Zero { get; }

    public readonly int m_a;
    public readonly int m_r;
    public readonly int m_c;

    public HecsCoordinate(int a, int r, int c)
    {
      m_a = a;
      m_r = r;
      m_c = c;
    }

    public void Deconstruct(out int a, out int r, out int c) { a = m_a; r = m_r; c = m_c; }

    public int A { get => m_a; init => m_a = value; }
    public int R { get => m_r; init => m_r = value; }
    public int C { get => m_c; init => m_c = value; }

    public HecsCoordinate[] NearestNeighborsCcw()
    {
      var cPa = m_c + m_a;
      var rPa = m_r + m_a;
      var uMa = 1 - m_a;
      var cMu = m_c - uMa;
      var rMu = m_r - uMa;

      return new HecsCoordinate[]
      {
        new(m_a, m_r, m_c + 1),
        new(uMa, rMu, cPa),
        new(uMa, rMu, cMu),
        new(m_a, m_r, m_c - 1),
        new(uMa, rPa, cMu),
        new(uMa, rPa, cPa),
      };
    }

    public HecsCoordinate[] NearestNeighborsCw()
    {
      var cPa = m_c + m_a;
      var rPa = m_r + m_a;
      var uMa = 1 - m_a;
      var cMu = m_c - uMa;
      var rMu = m_r - uMa;

      return new HecsCoordinate[]
      {
        new(uMa, rMu, cPa),
        new(m_a, m_r, m_c + 1),
        new(uMa, rPa, cPa),
        new(uMa, rPa, cMu),
        new(m_a, m_r, m_c - 1),
        new(uMa, rMu, cMu),
      };
    }

    public (double x, double y) ToCartesian2()
      => (m_a / 2 + m_c, double.Sqrt(3) * (m_a / 2 + m_r));

    //public CartesianCoordinate<double> ToCartesianCoordinate()
    //  => (CartesianCoordinate<double>)ToCartesian2();

    public System.Numerics.Vector2 ToVector2()
    {
      var (x, y) = ToCartesian2();

      return new((float)x, (float)y);
    }

    #region Overloaded operators

    /// <summary>
    /// <para>HECS addition.</para>
    /// <see href="https://en.wikipedia.org/wiki/Hexagonal_Efficient_Coordinate_System#Addition"/>
    /// </summary>
    public static HecsCoordinate operator +(HecsCoordinate a, HecsCoordinate b)
      => new(a.m_a ^ b.m_a, a.m_r + b.m_r + (a.m_a & b.m_a), a.m_c + b.m_c + (a.m_a & b.m_a));

    /// <summary>
    /// <para>HECS negation.</para>
    /// <see href="https://en.wikipedia.org/wiki/Hexagonal_Efficient_Coordinate_System#Negation"/>
    /// </summary>
    public static HecsCoordinate operator -(HecsCoordinate h)
      => new(h.m_a, -h.m_r - h.m_a, -h.m_c - h.m_a);

    /// <summary>
    /// <para>HECS subtraction.</para>
    /// <see href="https://en.wikipedia.org/wiki/Hexagonal_Efficient_Coordinate_System#Subtraction"/>
    /// </summary>
    public static HecsCoordinate operator -(HecsCoordinate a, HecsCoordinate b)
      => a + -b;

    /// <summary>
    /// <para>HECS scalar multiplication.</para>
    /// <see href="https://en.wikipedia.org/wiki/Hexagonal_Efficient_Coordinate_System#Scalar_multiplication"/>
    /// </summary>
    public static HecsCoordinate operator *(HecsCoordinate h, int k)
    {
      if (int.IsNegative(k))
        h = -h; // If k is negative, then negate h before scalar multiplication.

      var ak2 = h.m_a * (k / 2); // This occurs twice, so we can optimize somewhat.

      return new((h.m_a * k) % 2, k * h.m_r + ak2, k * h.m_c + ak2);
    }

    #endregion // Overloaded operators

    #region Implemented interfaces

    string System.IFormattable.ToString(string? format, System.IFormatProvider? formatProvider)
    {
      format ??= "N6";

      var sm = new SpanMaker<char>();
      sm = sm.Append('<');
      sm = sm.Append(m_a.ToString(format, formatProvider));
      sm = sm.Append(',');
      sm = sm.Append(' ');
      sm = sm.Append(m_r.ToString(format, formatProvider));
      sm = sm.Append(',');
      sm = sm.Append(' ');
      sm = sm.Append(m_c.ToString(format, formatProvider));
      sm = sm.Append('>');
      return sm.ToString();
    }

    #endregion // Implemented interfaces

    public override string ToString() => ((System.IFormattable)this).ToString(null, null);
  }
}
