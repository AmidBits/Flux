namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a column name from the array as if it were an array of column names, substituting if not enough column names are specified.</para>
    /// </summary>
    public static string EnsureColumnName(this string[] source, int index)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source is null
        ? throw new System.ArgumentNullException(nameof(source))
        : index < 0
        ? throw new System.IndexOutOfRangeException(nameof(index))
        : index < source.Length && source[index] is var value && !string.IsNullOrWhiteSpace(value)
        ? source[index]
        : index.ToColumnName();
    }
  }
}
