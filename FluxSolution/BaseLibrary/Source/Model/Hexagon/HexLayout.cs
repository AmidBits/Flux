namespace Flux.Model.Hexagon
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct HexLayout
    : System.IEquatable<HexLayout>
  {
    public static readonly HexLayout Empty;
    public bool IsEmpty => Equals(Empty);

    [System.Runtime.InteropServices.FieldOffset(0)] public readonly Media.Geometry.Size2 Size;
    [System.Runtime.InteropServices.FieldOffset(8)] public readonly Media.Geometry.Point2 Origin;
    [System.Runtime.InteropServices.FieldOffset(16)] public readonly HexOrientation Orientation;

    public HexLayout(HexOrientation orientation, Media.Geometry.Size2 size, Media.Geometry.Point2 origin)
    {
      Orientation = orientation;
      Size = size;
      Origin = origin;
    }

    public void HexToPixel(Hex h, out double x, out double y)
    {
      x = (Orientation.F0 * h.Q + Orientation.F1 * h.R) * Size.Width;
      y = (Orientation.F2 * h.Q + Orientation.F3 * h.R) * Size.Height;

      x += Origin.X;
      y += Origin.Y;
    }

    public HexF PixelToHex(double x, double y)
    {
      var dx = (x - Origin.X) / Size.Width;
      var dy = (y - Origin.Y) / Size.Height;

      var q = Orientation.B0 * dx + Orientation.B1 * dy;
      var r = Orientation.B2 * dx + Orientation.B3 * dy;

      return new HexF(q, r);
    }

    public void HexCornerOffset(int corner, out double x, out double y)
    {
      var angle = 2.0 * System.Math.PI * (Orientation.StartAngle - corner) / 6.0;

      x = Size.Width * (float)System.Math.Cos(angle);
      y = Size.Height * (float)System.Math.Sin(angle);
    }

    public System.Collections.Generic.List<(double x, double y)> PolygonCorners(Hex h)
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

    // Operators
    public static bool operator ==(HexLayout a, HexLayout b)
      => a.Equals(b);
    public static bool operator !=(HexLayout a, HexLayout b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(HexLayout other)
      => Orientation == other.Orientation && Size == other.Size && Origin == other.Origin;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is HexLayout o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(Orientation, Size, Origin);
    public override string? ToString()
      => $"<{GetType().Name}: {Orientation}, {Size}, {Origin}>";
  }
}
