namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Asserts that the <paramref name="value"/> is a member of the System.Numerics.IMinMaxValue in <paramref name="source"/> constrained by IntervalNotation.Closed enum value. If not, it throws an exception.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static TSelf MinMaxRadius<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IMinMaxValue<TSelf>
      => TSelf.Abs(source.MinValue - source.MaxValue) / TSelf.CreateChecked(2);
  }
}
