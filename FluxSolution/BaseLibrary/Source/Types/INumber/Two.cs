namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TSelf Two<TSelf>(this System.Numerics.INumber<TSelf> value)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.One + TSelf.One;
  }
}
