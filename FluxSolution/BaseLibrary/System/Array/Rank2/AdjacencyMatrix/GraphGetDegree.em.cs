namespace Flux
{
  public static partial class ArrayRank2
  {
    public static int GraphGetDegree<T>(this T[,] source, int key)
      where T : System.IEquatable<T>
    {
      GraphAssertProperty(source, out var length);

      if (key < 0 || key >= length) throw new System.ArgumentOutOfRangeException(nameof(key));

      var count = 0;

      for (var index = length - 1; index >= 0; index--)
      {
        if (index == key) count += GraphGetStateImpl(source, index, key);
        else
        {
          count += GraphGetStateImpl(source, index, key);
          count += GraphGetStateImpl(source, key, index);
        }
      }

      return count;
    }
  }
}
