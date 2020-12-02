namespace Flux
{
  public static partial class ArrayEm
  {
    // Should be a resource!
    private static string DefaultIndexedColumnFormat = @"Column_{0}";

    /// <summary>Returns a column name from the array as if it were an array of column names, substituting if not enough column names are specified.</summary>
    public static string AsColumnName(this string[] source, int index)
      => index < 0 ? throw new System.IndexOutOfRangeException(nameof(index)) : index < (source ?? throw new System.ArgumentNullException(nameof(source))).Length ? source[index] : string.Format(System.Globalization.CultureInfo.CurrentCulture, DefaultIndexedColumnFormat, index + 1);
  }
}
