namespace Flux
{
  public static partial class Number
  {
    public static Units.Angle AsAngle<TNumber>(this TNumber value)
    where TNumber : System.Numerics.INumber<TNumber>
    => new(double.CreateChecked(value));
  }
}
