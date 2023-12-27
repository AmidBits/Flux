namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> from <paramref name="source"/> and optionally selected <paramref name="indices"/>.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, params int[] indices)
    {
      var target = new System.Collections.Generic.List<T>(source.Length);

      for (var index = 0; index < source.Length; index++)
      {
        if (indices is null || indices.Length == 0)
          target.Add(source[index]);
        else
        {
          var sourceIndex = indices[index];

          target.Add(source[sourceIndex]);
        }
      }

      return target;
    }
  }
}
