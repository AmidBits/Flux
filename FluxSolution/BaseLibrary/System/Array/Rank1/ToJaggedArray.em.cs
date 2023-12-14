namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new jagged array with, either <paramref name="source"/> as the one sub-array (horizontal), or pivoted where each element in <paramref name="source"/> becomes one-element sub-arrays (vertical).</para>
    /// </summary>
    public static T[][] ToJaggedArray<T>(this T[] source, bool pivot = false)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : pivot
      ? source.Select(e => new T[] { e }).ToArray()
      : new T[][] { source };
  }
}
