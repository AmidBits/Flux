namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Gets the infimum (the largest value that is less than <paramref name="value"/>).</para>
    /// </summary>
    //public static TSelf GetInfimum<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //  => TSelf.BitDecrement(value);
    public static TSource GetInfimum<TSource>(this TSource value)
      where TSource : System.Numerics.INumber<TSource>
      => value switch
      {
        System.Double v => TSource.CreateChecked(System.Double.BitDecrement(v)),
        System.Single v => TSource.CreateChecked(System.Single.BitDecrement(v)),
        System.Half v => TSource.CreateChecked(System.Half.BitDecrement(v)),
        System.Runtime.InteropServices.NFloat v => TSource.CreateChecked(System.Runtime.InteropServices.NFloat.BitDecrement(v)),
        _ => value - TSource.One,
      };

    /// <summary>
    /// <para>Gets the infimum (the largest value that is less than <paramref name="value"/>) and supremum (the smallest value that is greater than <paramref name="value"/>).</para>
    /// </summary>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (TSelf Infimum, TSelf Supremum) GetInfimumAndSupremum<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => (value.GetInfimum(), value.GetSupremum());

    /// <summary>
    /// <para>Gets the supremum (the smallest value that is greater than <paramref name="value"/>).</para>
    /// </summary>
    //public static TSelf GetSupremum<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    //  => TSelf.BitIncrement(value);
    public static TSelf GetSupremum<TSelf>(this TSelf value)
      where TSelf : System.Numerics.INumber<TSelf>
      => value switch
      {
        System.Double v => TSelf.CreateChecked(System.Double.BitIncrement(v)),
        System.Single v => TSelf.CreateChecked(System.Single.BitIncrement(v)),
        System.Half v => TSelf.CreateChecked(System.Half.BitIncrement(v)),
        System.Runtime.InteropServices.NFloat v => TSelf.CreateChecked(System.Runtime.InteropServices.NFloat.BitIncrement(v)),
        _ => value + TSelf.One,
      };
  }
}
