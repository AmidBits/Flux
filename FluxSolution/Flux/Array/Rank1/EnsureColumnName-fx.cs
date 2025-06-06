namespace Flux
{
  public static partial class Arrays
  {
    /// <summary>
    /// <para>Returns a column name of the <paramref name="index"/>ed column from the array as if it were an array of column names, substituting if not enough column names are specified.</para>
    /// </summary>
    public static string EnsureColumnName(this string[] source, int index)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentOutOfRangeException.ThrowIfNegative(index, nameof(index));

      return index < source.Length && source[index] is var value && !string.IsNullOrWhiteSpace(value)
        ? source[index]
        : index.ToSingleOrdinalColumnName();
    }
  }
}
