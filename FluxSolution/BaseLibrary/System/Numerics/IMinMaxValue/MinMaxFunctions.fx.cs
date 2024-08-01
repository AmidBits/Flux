namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>The centre of the System.Numerics.IMinMaxValue in <paramref name="source"/>.</para>
    /// </summary>
    public static TSelf MinMaxCentre<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => (source.MinValue + source.MaxValue) / TSelf.CreateChecked(2);

    /// <summary>
    /// <para>The diameter of the System.Numerics.IMinMaxValue in <paramref name="source"/>.</para>
    /// </summary>
    public static TSelf MinMaxDiameter<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => TSelf.Abs(source.MinValue - source.MaxValue);

    /// <summary>
    /// <para>The radius of the System.Numerics.IMinMaxValue in <paramref name="source"/>.</para>
    /// </summary>
    public static TSelf MinMaxRadius<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => TSelf.Abs(source.MinValue - source.MaxValue) / TSelf.CreateChecked(2);
  }
}
