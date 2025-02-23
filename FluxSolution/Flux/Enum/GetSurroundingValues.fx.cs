//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    /// <typeparam name="T"></typeparam>
//    /// <param name="source"></param>
//    /// <returns></returns>
//    public static (T below, T above) GetSurroundingValues<T>(this T source)
//      where T : struct, System.Enum
//    {
//      var (TowardZeroIndex, TowardZeroItem, TowardZeroValue, AwayFromZeroIndex, AwayFromZeroItem, AwayFromValue) = System.Enum.GetValues<T>().Select(t => (int)(object)t).GetInfimumAndSupremum(e => e, (int)(object)source, true);

//      return ((T)(object)TowardZeroValue, (T)(object)AwayFromValue);
//    }
//  }
//}
