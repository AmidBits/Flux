namespace Flux
{
  public static partial class GenericMath
  {
    public static int UnitSign<TValue>(this TValue number)
      where TValue : System.Numerics.INumber<TValue>
      => TValue.IsNegative(number) ? -1 : +1;
  }
}
