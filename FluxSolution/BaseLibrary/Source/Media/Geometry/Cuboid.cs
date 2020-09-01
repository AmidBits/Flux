namespace Flux.Media.Geometry
{
  public struct Cuboid
    : System.IEquatable<Cuboid>, System.IFormattable
  {
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Depth { get; set; }

    public System.Numerics.Vector3 Center()
      => new System.Numerics.Vector3(X + Width / 2, Y + Height / 2, Z + Depth / 2);

    // Operators
    public static bool operator ==(Cuboid a, Cuboid b)
      => a.Equals(b);
    public static bool operator !=(Cuboid a, Cuboid b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(Cuboid other)
      => X == other.X && Y == other.Y && Z == other.Z && Width == other.Width && Height == other.Height && Depth == other.Depth;
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<Cuboid {X}, {Y}, {Z} : {Width}, {Height}, {Depth}>";
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is Cuboid s && Equals(s);
    public override int GetHashCode()
      => Flux.HashCode.CombineCore(0);
    public override string? ToString()
      => base.ToString();
  }
}
