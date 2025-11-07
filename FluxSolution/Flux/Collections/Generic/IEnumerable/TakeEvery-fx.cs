//namespace Flux
//{
//  public static partial class IEnumerables
//  {
//    #region TakeEvery

//    /// <summary>Creates a new sequence by taking the <paramref name="option"/> at every <paramref name="interval"/> from <paramref name="source"/>.</summary>
//    /// <exception cref="System.ArgumentNullException"/>
//    public static System.Collections.Generic.IEnumerable<T> TakeEvery<T>(this System.Collections.Generic.IEnumerable<T> source, int index, int interval)
//    {
//      if (interval <= 0) throw new System.ArgumentOutOfRangeException(nameof(interval));

//      return source.Where((e, i) => i % interval == index);
//    }

//    #endregion
//  }
//}
