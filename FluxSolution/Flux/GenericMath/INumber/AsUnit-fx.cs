namespace Flux
{
  public static Angle AsAngle<TNumber>(this TNumber value)
    where TNumber : System.Numerics.INumber<TNumber>
    => new (double.CreateChecked(value));
}
