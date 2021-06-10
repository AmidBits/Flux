namespace Flux.Model.Hexagon
{
  public class HexOrientation
  {
    public static readonly HexOrientation FlatTopped = new HexOrientation(3.0 / 2.0, 0.0, System.Math.Sqrt(3.0) / 2.0, System.Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, System.Math.Sqrt(3.0) / 3.0, 0.0);
    public static readonly HexOrientation PointyTopped = new HexOrientation(System.Math.Sqrt(3.0), System.Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, System.Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);

    private readonly double m_f0, m_f1, m_f2, m_f3;
    private readonly double m_b0, m_b1, m_b2, m_b3;
    private readonly double m_startAngle; // in multiples of 60°

    public double F0 => m_f0;
    public double F1 => m_f1;
    public double F2 => m_f2;
    public double F3 => m_f3;
    public double B0 => m_b0;
    public double B1 => m_b1;
    public double B2 => m_b2;
    public double B3 => m_b3;
    public double StartAngle => m_startAngle;

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
  };
}
