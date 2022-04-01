namespace Flux.Geometry
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct HexOrientation
    : System.IEquatable<HexOrientation>
  {
    public static readonly HexOrientation FlatTopped = new(3.0 / 2.0, 0.0, System.Math.Sqrt(3.0) / 2.0, System.Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, System.Math.Sqrt(3.0) / 3.0, 0.0);
    public static readonly HexOrientation PointyTopped = new(System.Math.Sqrt(3.0), System.Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, System.Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);

    public readonly double F0;
    public readonly double F1;
    public readonly double F2;
    public readonly double F3;
    public readonly double B0;
    public readonly double B1;
    public readonly double B2;
    public readonly double B3;
    public readonly double StartAngle; // in multiples of 60°

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

    #region Overloaded operators
    public static bool operator ==(HexOrientation a, HexOrientation b)
      => a.Equals(b);
    public static bool operator !=(HexOrientation a, HexOrientation b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(HexOrientation other)
      => F0 == other.F0 && F1 == other.F1 && F2 == other.F2 && F3 == other.F3 && B0 == other.B0 && B1 == other.B1 && B2 == other.B2 && B3 == other.B3 && StartAngle == other.StartAngle;
    #endregion Implemented interfaces

    #region Object overrides
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
      => $"{GetType().Name} {{ F = [{F0}, {F1}, {F2}, {F3}], B = [{B0}, {B1}, {B2}, {B3}], StartAngle = {StartAngle}° }}";
    #endregion Object overrides
  }
}
