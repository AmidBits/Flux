namespace Flux
{
  /// <summary>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</summary>
  public static partial class ExtensionMethods
  {
    public static bool IsArraySymmetrical(this System.Array source)
    {
      var length = source.GetLength(0);

      if (IsJaggedArray(source))
      {
        for (var index = length - 1; index > 0; index--)
          if (source.GetValue(index) is System.Array array && (array is null || array.GetLength(0) != length))
            return false;
      }
      else
      {
        for (var index = source.Rank - 1; index > 0; index--)
          if (source.GetLength(index) != length)
            return false;
      }

      return true;
    }
  }
}
