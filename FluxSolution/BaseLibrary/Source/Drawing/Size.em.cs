namespace Flux
{
  public static partial class SystemDrawingEm
  {
    /// <summary>Convert an index to a 2D vector, based on the size of the axes.</summary>
    public static System.Drawing.Point FromUniqueIndex(this in System.Drawing.Size source, int index)
      => new System.Drawing.Point(index % source.Width, index / source.Width);

    /// <summary>Converts the vector to an index, based on the size of the axes.</summary>
    public static int ToUniqueIndex(this in System.Drawing.Size size, in System.Drawing.Point point)
      => point.X + size.Width * point.Y;
    /// <summary>Converts the vector to an index, based on the size of the axes.</summary>
    public static int ToUniqueIndex(this in System.Drawing.Size size, int x, int y)
      => x + size.Width * y;

    /// <summary>Convert a 2D geometry size to a 2D drawing size.</summary>
    public static System.Drawing.Size ToPoint(this in Geometry.Size2 source)
      => new System.Drawing.Size(source.Width, source.Height);
    /// <summary>Convert a 2D drawing size to a 2D geometry size.</summary>
    public static Geometry.Size2 ToPoint2(this in System.Drawing.Size source)
      => new Geometry.Size2(source.Width, source.Height);
  }
}
