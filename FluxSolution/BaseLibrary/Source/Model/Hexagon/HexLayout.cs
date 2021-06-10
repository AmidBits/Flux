namespace Flux.Model.Hexagon
{
  public struct HexLayout
  {
    public readonly HexOrientation Orientation;
    public readonly Maui.Graphics.Size Size;
    public readonly Maui.Graphics.Point Origin;

    public HexLayout(HexOrientation orientation, Maui.Graphics.Size size, Maui.Graphics.Point origin)
    {
      Orientation = orientation;
      Size = size;
      Origin = origin;
    }

    public Flux.Maui.Graphics.Point HexToPixel(Hex h)
    {
      var x = (Orientation.F0 * h.Q + Orientation.F1 * h.R) * Size.Width;
      var y = (Orientation.F2 * h.Q + Orientation.F3 * h.R) * Size.Height;
     
      return new Maui.Graphics.Point(x + Origin.X, y + Origin.Y);
    }

    public HexF PixelToHex(Flux.Maui.Graphics.Point p)
    {
      var pt = new Flux.Maui.Graphics.Point((p.X - Origin.X) / Size.Width, (p.Y - Origin.Y) / Size.Height);

      var q = Orientation.B0 * pt.X + Orientation.B1 * pt.Y;
      var r = Orientation.B2 * pt.X + Orientation.B3 * pt.Y;
     
      return new HexF(q, r);
    }

    public Flux.Maui.Graphics.Point HexCornerOffset(int corner)
    {
      var angle = 2.0 * System.Math.PI * (Orientation.StartAngle - corner) / 6.0;
      
      return new Flux.Maui.Graphics.Point(Size.Width * System.Math.Cos(angle), Size.Height * System.Math.Sin(angle));
    }

    public System.Collections.Generic.List<Flux.Maui.Graphics.Point> PolygonCorners(Hex h)
    {
      var corners = new System.Collections.Generic.List<Flux.Maui.Graphics.Point>();
      
      var center = HexToPixel(h);

      for (int i = 0; i < 6; i++)
      {
        var offset = HexCornerOffset(i);

        corners.Add(new Flux.Maui.Graphics.Point(center.X + offset.X, center.Y + offset.Y));
      }

      return corners;
    }
  }
}
