namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> from <paramref name="source"/>.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source)
    {
      var target = new System.Collections.Generic.List<T>(source.Length);

      for (var index = 0; index < source.Length; index++)
        target.Add(source[index]);

      return target;
    }
  }
}
