namespace Flux
{
  public static partial class Enums
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static (int ExtremumMinEnumIndex, int ExtremumMinEnumItem, T ExtremumMinEnumValue, int ExtremumMaxEnumIndex, int ExtremumMaxEnumItem, T ExtremumMaxEnumValue) GetExtremum<T>()
      where T : struct, System.Enum
    {
      var (ExtremumMinIndex, ExtremumMinItem, ExtremumMinValue, ExtremumMaxIndex, ExtremumMaxItem, ExtremumMaxValue) = System.Enum.GetValues<T>().Select(t => (int)(object)t).GetExtremum(e => e);

      return (ExtremumMinIndex, ExtremumMinItem, (T)(object)ExtremumMinValue, ExtremumMaxIndex, ExtremumMaxItem, (T)(object)ExtremumMaxValue);
    }
  }
}
