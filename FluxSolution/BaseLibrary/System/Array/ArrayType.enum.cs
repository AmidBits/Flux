namespace Flux
{
  public static partial class Em
  {
    public static ArrayType GetArrayType(this System.Array array)
    {
      if (array.GetType().IsArray)
      {
        var elementType = array.GetType().GetElementType()?.IsArray ?? false;

        if (array.Rank == 1 && !elementType)
          return ArrayType.OneDimensionalArray;
        else if (array.Rank == 2)
          return ArrayType.TwoDimensionalArray;
        else if (array.Rank > 2)
          return ArrayType.MultiDimensionalArray;
        else if (elementType)
          return ArrayType.JaggedArray;
      }

      return ArrayType.NotAnArray;
    }
  }

  /// <summary>
  /// <para></para>
  /// </summary>
  public enum ArrayType
  {
    NotAnArray,
    OneDimensionalArray,
    TwoDimensionalArray,
    MultiDimensionalArray,
    JaggedArray,
  }
}