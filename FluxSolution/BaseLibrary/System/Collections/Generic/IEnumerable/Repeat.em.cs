namespace Flux
{
  public static partial class Fx
  {
    /// <summary></summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<T> Repeat<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

      var list = new System.Collections.Generic.List<T>(source);

      while (count-- >= 0)
        foreach (var item in list)
          yield return item;
    }
  }
}
