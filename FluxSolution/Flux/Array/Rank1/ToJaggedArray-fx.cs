namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new jagged array with, either <paramref name="source"/> as the one sub-array (horizontal), or pivoted where each element in <paramref name="source"/> becomes one-element sub-arrays (vertical).</para>
    /// </summary>
    public static T[][] ToJaggedArray<T>(this T[] source, bool pivot = false)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var array = new T[pivot ? source.Length : 1][];

      if (pivot)
        for (var i = 0; i < source.Length; i++)
          array[i] = [source[i]];
      else
        array[0] = source;

      return array;
    }
  }
}
