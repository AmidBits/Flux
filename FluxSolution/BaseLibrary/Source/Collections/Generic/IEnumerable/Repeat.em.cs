namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<T> Repeat<T>(this System.Collections.Generic.IEnumerable<T> source, int count)
    {
      var list = new System.Collections.Generic.List<T>(source);

      if (count < 0)
        count = 0;

      while (count-- >= 0)
        foreach (var item in list)
          yield return item;
    }
  }
}
