#if !NETCOREAPP
namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ExtensionsArray
  {
    /// <summary>Fill the array with the specified value pattern, at the offset and count.</summary>
    public static T[] Fill<T>(this T[] array, int offset, int count, params T[] valuePattern)
    {
      if (array is null) throw new System.ArgumentNullException(nameof(array));

      var copyLength = valuePattern.Length < count ? valuePattern.Length : count;

      System.Array.Copy(valuePattern, 0, array, offset, copyLength);

      while ((copyLength << 1) < count)
      {
        System.Array.Copy(array, offset, array, offset + copyLength, copyLength);

        copyLength <<= 1;
      }

      System.Array.Copy(array, offset, array, offset + copyLength, count - copyLength);

      return array;
    }
    /// <summary>Fill the array with the specified value pattern.</summary>
    public static T[] Fill<T>(this T[] array, params T[] valuePattern) => array.ToArray(0, array.Length).Fill(0, array.Length, valuePattern);
  }
}
#endif
