namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a column name from the array as if it were an array of column names, substituting if not enough column names are specified.</para>
    /// </summary>
    public static string AsColumnName(this string[] source, int index)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (index < 0) throw new System.IndexOutOfRangeException(nameof(index));

      return index < source.Length ? source[index] : string.Format(@"Column_{0}", index + 1);
    }
  }
}
