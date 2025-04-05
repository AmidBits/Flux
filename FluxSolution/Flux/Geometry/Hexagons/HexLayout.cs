namespace Flux.Geometry.Hexagons
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct HexLayout
  {
    private readonly System.Drawing.Point m_size;
    private readonly System.Drawing.Point m_origin;
    private readonly HexOrientation m_orientation;

    public HexLayout(HexOrientation orientation, System.Drawing.Point size, System.Drawing.Point origin)
    {
      m_orientation = orientation;
      m_size = size;
      m_origin = origin;
    }

    public System.Drawing.Point Size { get => m_size; init => m_size = value; }
    public System.Drawing.Point Origin { get => m_origin; init => m_origin = value; }
    public HexOrientation Orientation { get => m_orientation; init => m_orientation = value; }

    public void HexToPixel(CoordinateSystems.HexCoordinate<int> h, out double x, out double y)
    {
      x = (m_orientation.F0 * h.Q + m_orientation.F1 * h.R) * m_size.X;
      y = (m_orientation.F2 * h.Q + m_orientation.F3 * h.R) * m_size.Y;

      x += m_origin.X;
      y += m_origin.Y;
    }

    public CoordinateSystems.HexCoordinate<double> PixelToHex(double x, double y)
    {
      var dx = (x - m_origin.X) / m_size.X;
      var dy = (y - m_origin.Y) / m_size.Y;

      var q = m_orientation.B0 * dx + m_orientation.B1 * dy;
      var r = m_orientation.B2 * dx + m_orientation.B3 * dy;

      return new(q, r);
    }

    public void HexCornerOffset(int corner, out double x, out double y)
    {
      var angle = double.Tau * (m_orientation.StartAngle - corner) / 6.0;

      x = m_size.X * double.Cos(angle);
      y = m_size.Y * double.Sin(angle);
    }

    public System.Collections.Generic.List<(double x, double y)> PolygonCorners(CoordinateSystems.HexCoordinate<int> h)
    {
      var corners = new System.Collections.Generic.List<(double x, double y)>();

      HexToPixel(h, out var centerX, out var centerY);

      for (int i = 0; i < 6; i++)
      {
        HexCornerOffset(i, out var offsetX, out var offsetY);

        corners.Add((centerX + offsetX, centerY + offsetY));
      }

      return corners;
    }
  }
}
