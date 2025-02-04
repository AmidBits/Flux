namespace Flux
{
  public static partial class Fx
  {
    public static void RemoveAdjacentDuplicates<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source.Count < 2) return;

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var removeIndex = 1;

      for (var index = 1; index < source.Count; index++)
        if (!equalityComparer.Equals(source[removeIndex - 1], source[index]))
          source[removeIndex++] = source[index];

      source.RemoveRange(removeIndex, source.Count - removeIndex);
    }
  }
}
