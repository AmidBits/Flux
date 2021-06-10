namespace Flux.Model.Hexagon
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
  public struct HexOrientation
    : System.IEquatable<HexOrientation>
  {
    public static readonly HexOrientation FlatTopped = new HexOrientation(3.0 / 2.0, 0.0, System.Math.Sqrt(3.0) / 2.0, System.Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, System.Math.Sqrt(3.0) / 3.0, 0.0);
    public static readonly HexOrientation PointyTopped = new HexOrientation(System.Math.Sqrt(3.0), System.Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, System.Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);

    [System.Runtime.InteropServices.FieldOffset(0)] public readonly double F0;
    [System.Runtime.InteropServices.FieldOffset(8)] public readonly double F1;
    [System.Runtime.InteropServices.FieldOffset(16)] public readonly double F2;
    [System.Runtime.InteropServices.FieldOffset(24)] public readonly double F3;
    [System.Runtime.InteropServices.FieldOffset(32)] public readonly double B0;
    [System.Runtime.InteropServices.FieldOffset(40)] public readonly double B1;
    [System.Runtime.InteropServices.FieldOffset(48)] public readonly double B2;
    [System.Runtime.InteropServices.FieldOffset(56)] public readonly double B3;
    [System.Runtime.InteropServices.FieldOffset(64)] public readonly double StartAngle; // in multiples of 60°

    private HexOrientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)
    {
      F0 = f0;
      F1 = f1;
      F2 = f2;
      F3 = f3;
      B0 = b0;
      B1 = b1;
      B2 = b2;
      B3 = b3;
      StartAngle = startAngle;
    }

    // Operators
    public static bool operator ==(HexOrientation a, HexOrientation b)
      => a.Equals(b);
    public static bool operator !=(HexOrientation a, HexOrientation b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(HexOrientation other)
      => F0 == other.F0 && F1 == other.F1 && F2 == other.F2 && F3 == other.F3 && B0 == other.B0 && B1 == other.B1 && B2 == other.B2 && B3 == other.B3 && StartAngle == other.StartAngle;

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is HexOrientation o && Equals(o);
    public override int GetHashCode()
    {
      var hc = new System.HashCode();
      hc.Add(F0);
      hc.Add(F1);
      hc.Add(F2);
      hc.Add(F3);
      hc.Add(B0);
      hc.Add(B1);
      hc.Add(B2);
      hc.Add(B3);
      hc.Add(StartAngle);
      return hc.ToHashCode();
    }
    public override string? ToString()
      => $"<{GetType().Name}: [{F0}, {F1}, {F2}, {F3}], [{B0}, {B1}, {B2}, {B3}], {StartAngle}°>";
  }
}
