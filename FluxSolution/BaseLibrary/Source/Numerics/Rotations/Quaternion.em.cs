namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static Quaternion<TSelf> ToQuaternion<TSelf>(this System.Numerics.Quaternion source)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => new(
        TSelf.CreateChecked(source.X),
        TSelf.CreateChecked(source.Y),
        TSelf.CreateChecked(source.Z),
        TSelf.CreateChecked(source.W)
      );
  }
}
