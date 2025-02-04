namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the <paramref name="number"/> is considered plural in terms of writing.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool IsConsideredPlural<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => number != TNumber.One;
  }
}
