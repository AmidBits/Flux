namespace Flux
{
  public static partial class Xtensions
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
  }
}
