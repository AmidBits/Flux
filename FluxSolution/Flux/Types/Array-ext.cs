namespace Flux
{
  // <summary>
  // <para>Two-dimensional arrays are arbitrary in terms of rows and columns, we simply choose the "native storage orientation".</para>
  // <para>The native storage orientation is row-major order (see link). I.e. the array consists of elements by row then by column, as opposed to first by column then by row.</para>
  // <para>This is the order in which .NET yield elements using the IEnumerate interface of such an array.</para>
  // <see href="https://en.wikipedia.org/wiki/Row-_and_column-major_order"/>
  // </summary>
  public enum ArrayDimensionLabel
  {
    // <summary>
    // <para>The row is represented as dimension 0.</para>
    // </summary>
    // <remark>The concept of dimension 0 as the row is entirely a matter of choice, and simply a matter of adoption.</remark>
    Row = 0,
    // <summary>
    // <para>The column is represented as dimension 1.</para>
    // </summary>
    // <remark>The concept of dimension 1 as the column is entirely a matter of choice, and simply a matter of adoption.</remark>
    Column = 1
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

  public static partial class ArrayExtensions
  {
    extension(System.Array source)
    {
      /// <summary>
      /// <para>For any array of structures larger than a single byte.</para>
      /// <para>The array size is limited to a total of 4 billion elements, and to a maximum index of 0X7FEFFFFF in any given dimension (0X7FFFFFC7 for byte arrays and arrays of single-byte structures).</para>
      /// <para><see href="https://learn.microsoft.com/en-us/dotnet/api/system.array#remarks"/></para>
      /// </summary>
      public static int MaxIndexArrayOfMultiByteStructures => 0x7FEFFFFF;

      /// <summary>
      /// <para>For byte arrays and arrays of single byte structures.</para>
      /// <para>The array size is limited to a total of 4 billion elements, and to a maximum index of 0X7FEFFFFF in any given dimension (0X7FFFFFC7 for byte arrays and arrays of single-byte structures).</para>
      /// <para><see href="https://learn.microsoft.com/en-us/dotnet/api/system.array#remarks"/></para>
      /// </summary>
      public static int MaxIndexArrayOfSingleByteStructures => 0x7FFFFFC7;

      #region DimensionalSymmetry (Assert.., Has.., TryHas..)


      /// <summary>
      /// <para>Asserts that the <paramref name="source"/> array has symmetrical dimensions, i.e. all dimensions are the same length.</para>
      /// </summary>
      /// <param name="source">The array.</param>
      /// <param name="symmetricalLength"></param>
      /// <param name="paramName"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentException"></exception>
      public System.Array AssertDimensionalSymmetry(out int symmetricalLength, string? paramName = null)
      {
        if (!HasDimensionalSymmetry(source, out symmetricalLength))
          throw new System.ArgumentOutOfRangeException(nameof(source));

        return source;
      }

      /// <summary>
      /// <para>Determines whether the <paramref name="source"/> array has symmetrical dimensions, i.e. all dimensions are the same length.</para>
      /// </summary>
      /// <param name="source">The array.</param>
      /// <param name="symmetricalLength"></param>
      /// <returns></returns>
      public bool HasDimensionalSymmetry(out int symmetricalLength)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        symmetricalLength = source.GetLength(0); // Load the first dimensional length.

        if (IsJaggedArray(source))
        {
          for (var index = symmetricalLength - 1; index > 0; index--)
            if (source.GetValue(index) is System.Array array && (array is null || array.GetLength(0) != symmetricalLength))
              return false;
        }
        else
        {
          for (var index = source.Rank - 1; index > 0; index--)
            if (source.GetLength(index) != symmetricalLength)
              return false;
        }

        return true;
      }

      /// <summary>
      /// <para>Measures all dimensions, if all equal in length sets the out argument and returns whether they are equal.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      /// <param name="source"></param>
      /// <param name="symmetricalLength"></param>
      /// <returns></returns>
      public bool TryHasDimensionalSymmetry(out int symmetricalLength)
      {
        try
        {
          if (HasDimensionalSymmetry(source, out symmetricalLength))
            return true;
        }
        catch { }

        symmetricalLength = -1;
        return false;
      }

      #endregion

      /// <summary>
      /// <para>Identifies the type of array.</para>
      /// </summary>
      /// <returns></returns>
      public ArrayType GetArrayType()
      {
        if (source.GetType().IsArray)
        {
          var elementTypeIsArray = source.GetType().GetElementType()?.IsArray ?? false;

          if (source.Rank == 1 && !elementTypeIsArray)
            return ArrayType.OneDimensionalArray;
          else if (source.Rank == 2)
            return ArrayType.TwoDimensionalArray;
          else if (source.Rank > 2)
            return ArrayType.MultiDimensionalArray;
          else if (elementTypeIsArray)
            return ArrayType.JaggedArray;
        }

        return ArrayType.NotAnArray;
      }

      /// <summary>
      /// <para>Determines whether the <see cref="System.Array"/> is a jagged-array, i.e. an array of arrays.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsJaggedArray()
        => source.GetType().GetElementType()?.IsArray ?? false;

      #region Rank (Assert.., Is..)

      /// <summary>
      /// <para>Asserts that the <paramref name="source"/> rank is equal to <paramref name="rank"/> and throws an exception if not.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="rank"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Array AssertRank(int rank)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfNotEqual(rank, source.Rank);

        return source;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="source"/> is not null and <paramref name="source"/>.Rank is equal to <paramref name="rank"/>.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="rank"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public bool IsRank(int rank)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(rank, 1);

        return source.Rank == rank;
      }

      #endregion

      /// <summary>
      /// <para>Returns a one/two-dimensional or jagged array as a new <see cref="SpanMaker{T}"/>, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public string ToConsoleString<T>(ConsoleFormatOptions? options = null)
      {
        return source.GetArrayType() switch
        {
          ArrayType.NotAnArray => "Not an array.",
          ArrayType.OneDimensionalArray => ((T[])source).Rank1ToConsoleString(options),
          ArrayType.TwoDimensionalArray => ((T[,])source).Rank2ToConsoleString(options),
          ArrayType.MultiDimensionalArray => "Multi-dimensional array.",
          ArrayType.JaggedArray => ((T[][])source).JaggedToConsoleString(options),
          _ => throw new NotImplementedException(),
        };
      }

      public string ToConsoleString(ConsoleFormatOptions? options = null)
        => ToConsoleString<object>(source, options);
    }
  }
}
