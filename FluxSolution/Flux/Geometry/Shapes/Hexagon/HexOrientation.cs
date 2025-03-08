namespace Flux.Geometry.Shapes.Hexagon
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct HexOrientation
  {
    public static readonly HexOrientation FlatTopped = new(3.0 / 2.0, 0.0, double.Sqrt(3.0) / 2.0, double.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, double.Sqrt(3.0) / 3.0, 0.0);
    public static readonly HexOrientation PointyTopped = new(double.Sqrt(3.0), double.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, double.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);

    public readonly double m_f0;
    public readonly double m_f1;
    public readonly double m_f2;
    public readonly double m_f3;
    public readonly double m_b0;
    public readonly double m_b1;
    public readonly double m_b2;
    public readonly double m_b3;
    public readonly double m_startAngle; // in multiples of 60°

    public HexOrientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)
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

    public double F0 { get => m_f0; init => m_f0 = value; }
    public double F1 { get => m_f1; init => m_f1 = value; }
    public double F2 { get => m_f2; init => m_f2 = value; }
    public double F3 { get => m_f3; init => m_f3 = value; }
    public double B0 { get => m_b0; init => m_b0 = value; }
    public double B1 { get => m_b1; init => m_b1 = value; }
    public double B2 { get => m_b2; init => m_b2 = value; }
    public double B3 { get => m_b3; init => m_b3 = value; }

    /// <summary>In multiples of 60°.</summary>
    public double StartAngle { get => m_startAngle; init => m_startAngle = value; }
  }
}
