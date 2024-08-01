namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The centre of the System.Numerics.IMinMaxValue in <paramref name="source"/>.</para>
    /// </summary>
    public static TSelf MinMaxIntervalCentre<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => (source.MinValue + source.MaxValue) / TSelf.CreateChecked(2);

    /// <summary>
    /// <para>The length (a.k.a. diameter, range, size) of the System.Numerics.IMinMaxValue in <paramref name="source"/>.</para>
    /// </summary>
    public static TSelf MinMaxIntervalLength<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => TSelf.Abs(source.MinValue - source.MaxValue);

    /// <summary>
    /// <para>The radius of the System.Numerics.IMinMaxValue in <paramref name="source"/>.</para>
    /// </summary>
    public static TSelf MinMaxIntervalRadius<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => TSelf.Abs(source.MinValue - source.MaxValue) / TSelf.CreateChecked(2);
  }
}
