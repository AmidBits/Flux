//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    #region SkipEvery

//    /// <summary>Creates a new sequence by skipping the <paramref name="option"/> at every <paramref name="interval"/> from <paramref name="source"/>.</summary>
//    /// <exception cref="System.ArgumentNullException"/>
//    public static System.Collections.Generic.IEnumerable<T> SkipEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int index, int interval)
//    {
//      System.ArgumentOutOfRangeException.ThrowIfNegative(index);
//      System.ArgumentOutOfRangeException.ThrowIfNegative(interval);

//      System.ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, interval);

//      return source.Where((e, i) => i % interval != index);
//    }

//    #endregion
//  }
//}
