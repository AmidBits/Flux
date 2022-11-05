namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public record class HexLayout
  {
    private readonly Size2 m_size;
    private readonly CartesianCoordinate2I m_origin;
    private readonly HexOrientation m_orientation;

    public HexLayout(HexOrientation orientation, Size2 size, CartesianCoordinate2I origin)
    {
      m_orientation = orientation;
      m_size = size;
      m_origin = origin;
    }

    public Size2 Size { get => m_size; init => m_size = value; }
    public CartesianCoordinate2I Origin { get => m_origin; init => m_origin = value; }
    public HexOrientation Orientation { get => m_orientation; init => m_orientation = value; }

    public void HexToPixel(HexCoordinateI h, out double x, out double y)
    {
      x = (m_orientation.F0 * h.Q + m_orientation.F1 * h.R) * m_size.Width;
      y = (m_orientation.F2 * h.Q + m_orientation.F3 * h.R) * m_size.Height;

      x += m_origin.X;
      y += m_origin.Y;
    }

    public HexCoordinateR PixelToHex(double x, double y)
    {
      var dx = (x - m_origin.X) / m_size.Width;
      var dy = (y - m_origin.Y) / m_size.Height;

      var q = m_orientation.B0 * dx + m_orientation.B1 * dy;
      var r = m_orientation.B2 * dx + m_orientation.B3 * dy;

      return new HexCoordinateR(q, r);
    }

    public void HexCornerOffset(int corner, out double x, out double y)
    {
      var angle = Maths.PiX2 * (m_orientation.StartAngle - corner) / 6.0;

      x = m_size.Width * System.Math.Cos(angle);
      y = m_size.Height * System.Math.Sin(angle);
    }

    public System.Collections.Generic.List<(double x, double y)> PolygonCorners(HexCoordinateI h)
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
