namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct HexOrientation
    : System.IEquatable<HexOrientation>
  {
    public static readonly HexOrientation FlatTopped = new(3.0 / 2.0, 0.0, System.Math.Sqrt(3.0) / 2.0, System.Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, System.Math.Sqrt(3.0) / 3.0, 0.0);
    public static readonly HexOrientation PointyTopped = new(System.Math.Sqrt(3.0), System.Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, System.Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);

    public readonly double m_f0;
    public readonly double m_f1;
    public readonly double m_f2;
    public readonly double m_f3;
    public readonly double m_b0;
    public readonly double m_b1;
    public readonly double m_b2;
    public readonly double m_b3;
    public readonly double m_startAngle; // in multiples of 60°

    private HexOrientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)
    {
      m_f0 = f0;
      m_f1 = f1;
      m_f2 = f2;
      m_f3 = f3;
      m_b0 = b0;
      m_b1 = b1;
      m_b2 = b2;
      m_b3 = b3;
      m_startAngle = startAngle;
    }

    public double F0 => m_f0;
    public double F1 => m_f1;
    public double F2 => m_f2;
    public double F3 => m_f3;
    public double B0 => m_b0;
    public double B1 => m_b1;
    public double B2 => m_b2;
    public double B3 => m_b3;
    public double StartAngle => m_startAngle; // in multiples of 60°

    #region Overloaded operators
    public static bool operator ==(HexOrientation a, HexOrientation b)
      => a.Equals(b);
    public static bool operator !=(HexOrientation a, HexOrientation b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(HexOrientation other)
      => m_f0 == other.m_f0 && m_f1 == other.m_f1 && m_f2 == other.m_f2 && m_f3 == other.m_f3 && m_b0 == other.m_b0 && m_b1 == other.m_b1 && m_b2 == other.m_b2 && m_b3 == other.m_b3 && m_startAngle == other.m_startAngle;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is HexOrientation o && Equals(o);
    public override int GetHashCode()
    {
      var hc = new System.HashCode();
      hc.Add(m_f0);
      hc.Add(m_f1);
      hc.Add(m_f2);
      hc.Add(m_f3);
      hc.Add(m_b0);
      hc.Add(m_b1);
      hc.Add(m_b2);
      hc.Add(m_b3);
      hc.Add(m_startAngle);
      return hc.ToHashCode();
    }
    public override string? ToString()
      => $"{GetType().Name} {{ F = [{m_f0}, {m_f1}, {m_f2}, {m_f3}], B = [{m_b0}, {m_b1}, {m_b2}, {m_b3}], StartAngle = {m_startAngle}° }}";
    #endregion Object overrides
  }
}
