namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record struct LineSlope
  {
    public static readonly LineSlope Zero;

    private readonly double m_slope;

    public LineSlope(double slope)
      => m_slope = slope;
    public LineSlope(double x1, double y1, double x2, double y2)
      => m_slope = (y2 - y1) / (x2 - x1);

    public double Slope { get => m_slope; init => m_slope = value; }

    #region Static methods
    #endregion Static methods
  }
}
