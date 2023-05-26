namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension, i.e. so called row-major.</summary>
  public static partial class ArrayRank1
  {
    /// <summary>Creates a new jagged array with, either <paramref name="source"/> as the one sub-array (horizontal), or pivoted where each element in <paramref name="source"/> becomes one-element sub-arrays (vertical).</summary>
    public static T[][] ToJaggedArray<T>(this T[] source, bool pivot = false)
      => source is null
      ? throw new System.ArgumentNullException(nameof(source))
      : pivot
      ? source.Select(e => new T[] { e }).ToArray()
      : new T[][] { source };
  }
}
