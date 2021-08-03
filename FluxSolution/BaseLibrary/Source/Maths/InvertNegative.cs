namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Invert the sign of the value only if the sign is negative.</summary>
    public static double InvertNegative(double value, double sign)
      => sign < 0 ? -value : value;
  }
}
