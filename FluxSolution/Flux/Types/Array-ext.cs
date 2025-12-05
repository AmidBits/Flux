namespace Flux
{
  // <summary>
  // <para>Two-dimensional arrays are arbitrary in terms of rows and columns, we simply choose the "native storage orientation".</para>
  // <para>The native storage orientation is row-major order (see link). I.e. the array consists of elements by row then by column, as opposed to first by column then by row.</para>
  // <para>This is the order in which .NET yield elements using the IEnumerate interface of such an array.</para>
  // <see href="https://en.wikipedia.org/wiki/Row-_and_column-major_order"/>
  // </summary>
  public enum TwoDimensionalArrayAxis
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
    extension(System.Array)
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

      /// <summary>
      /// <para>Concatenates the source and the <paramref name="other"/> array in a <paramref name="dimension"/>-major-order.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="other">The "second" array.</param>
      /// <param name="dimension">The direction of the concatenation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static T[,] ConcatToCopy<T>(TwoDimensionalArrayAxis dimension, T[,] source, T[,] other)
      {
        System.Array.AssertRank(source, 2);
        System.Array.AssertRank(other, 2);

        var sourceRows = source.GetLength(0);
        var sourceCols = source.GetLength(1);

        var targetRows = other.GetLength(0);
        var targetCols = other.GetLength(1);

        T[,] concat;

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            concat = new T[(sourceRows + targetRows), int.Max(sourceCols, targetCols)];

            Copy(source, concat, 0, 0, 0, 0, sourceRows, sourceCols);
            Copy(other, concat, 0, 0, sourceRows, 0, targetRows, targetCols);
            break;
          case TwoDimensionalArrayAxis.Column:
            concat = new T[int.Max(sourceRows, targetRows), (sourceCols + targetCols)];

            Copy(source, concat, 0, 0, 0, 0, sourceRows, sourceCols);
            Copy(other, concat, 0, 0, 0, sourceCols, targetRows, targetCols);
            break;
          default:
            throw new System.NotImplementedException();
        }

        return concat;
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="sourceIndex"></param>
      /// <param name="target"></param>
      /// <param name="targetIndex"></param>
      /// <param name="length"></param>
      /// <param name="copySelector"></param>
      public static void Copy<T>(T[] source, int sourceIndex, T[] target, int targetIndex, int length, System.Func<T, T> copySelector)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(target);

        System.Range.AssertInRange(sourceIndex, length, source.Length);
        System.Range.AssertInRange(targetIndex, length, target.Length);

        for (var i = length - 1; i >= 0; i--)
          target[targetIndex++] = copySelector(source[sourceIndex++]);
      }

      /// <summary>
      /// <para>Allocates a new array, based on the size of <paramref name="source"/>. No data is involved. Dimension-1 (i.e. rows) are added/removed with <paramref name="addOrRemoveRows"/> and dimension-1 (i.e. columns) are added/removed with <paramref name="addOrRemoveColumns"/>.</para>
      /// <para>A negative value will remove and a positive value will add, as many column as the number represents.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="addOrRemoveRows">A negative value removes and a positive value adds, as many rows (dimension-0) as the number represents.</param>
      /// <param name="addOrRemoveColumns">A negative value removes and a positive value adds, as many columns (dimension-1) as the number represents.</param>
      /// <returns></returns>
      public static T[,] CreateNew<T>(T[,] source, int addOrRemoveRows, int addOrRemoveColumns)
      {
        System.Array.AssertRank(source, 2);

        return new T[source.GetLength(0) + addOrRemoveRows, source.GetLength(1) + addOrRemoveColumns];
      }

      #region ..DimensionalSymmetry (Assert.., Has.., TryHas..)

      /// <summary>
      /// <para>Asserts that the <paramref name="source"/> array has symmetrical dimensions, i.e. all dimensions are the same length.</para>
      /// </summary>
      /// <param name="source">The array.</param>
      /// <param name="symmetricalLength"></param>
      /// <param name="paramName"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentException"></exception>
      public static System.Array AssertDimensionalSymmetry(System.Array source, out int symmetricalLength, string? paramName = null)
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
      public static bool HasDimensionalSymmetry(System.Array source, out int symmetricalLength)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        symmetricalLength = source.GetLength(0); // Load the first dimensional length.

        if (source.ArrayType == ArrayType.JaggedArray)
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
      public static bool TryHasDimensionalSymmetry(System.Array source, out int symmetricalLength)
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

      #region ..Rank (Assert.., Is..)

      /// <summary>
      /// <para>Asserts that the <paramref name="source"/> rank is equal to <paramref name="rank"/> and throws an exception if not.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="rank"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Array AssertRank(System.Array source, int rank)
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
      public static bool IsRank(System.Array source, int rank)
        => source is not null && rank >= 1 && source.Rank == rank;

      #endregion

      #region Copy

      /// <summary>
      /// <para>Copies <paramref name="length0"/> rows (dimension-0 elements) by <paramref name="length1"/> columns (dimension-1 elements), i.e. a block, from <paramref name="source"/> starting at [<paramref name="sourceIndex0"/>, <paramref name="sourceIndex1"/>] into <paramref name="target"/> starting at [<paramref name="targetIndex0"/>, <paramref name="targetIndex1"/>].</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public static void Copy<T>(T[,] source, T[,] target, int sourceIndex0, int sourceIndex1, int targetIndex0, int targetIndex1, int length0, int length1)
      {
        System.Array.AssertRank(source, 2);
        System.Array.AssertRank(target, 2);

        for (var r = length0 - 1; r >= 0; r--)
          for (var c = length1 - 1; c >= 0; c--)
            target[targetIndex0 + r, targetIndex1 + c] = source[sourceIndex0 + r, sourceIndex1 + c];
      }

      /// <summary>
      /// <para>Copies <paramref name="length"/> rows (dimension-0 strands) from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> starting at <paramref name="targetIndex"/>.</para>
      /// <para>Copies <paramref name="length"/> columns (dimension-1 strands) from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> starting at <paramref name="targetIndex"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="sourceIndex"></param>
      /// <param name="target"></param>
      /// <param name="targetIndex"></param>
      /// <param name="length"></param>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public static void Copy<T>(TwoDimensionalArrayAxis dimension, T[,] source, int sourceIndex, T[,] target, int targetIndex, int length)
      {
        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            Copy(source, target, sourceIndex, 0, targetIndex, 0, length, source.GetLength(1));
            break;
          case TwoDimensionalArrayAxis.Column:
            Copy(source, target, 0, sourceIndex, 0, targetIndex, source.GetLength(0), length);
            break;
          default:
            throw new System.NotImplementedException();
        }
      }

      #endregion

      #region Fill

      /// <summary>
      /// <para>Fill <paramref name="length"/> elements in <paramref name="source"/> from <paramref name="pattern"/> (repeatingly if necessary) at <paramref name="index"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="pattern"></param>
      /// <returns></returns>
      public static T[] Fill<T>(T[] source, int index, int length, params System.ReadOnlySpan<T> pattern)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Range.AssertInRange(index, length, source.Length);

        for (var i = 0; i < length; i++)
          source[index++] = pattern[i % pattern.Length];

        return source;
      }

      /// <summary>
      /// <para>Fill <paramref name="source"/> with the specified <paramref name="pattern"/>, at <paramref name="rowIndex"/>, <paramref name="columnIndex"/> and <paramref name="rowLength"/> and <paramref name="columnLength"/>. Using a sort of continuous flow or flood fill.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="rowIndex"></param>
      /// <param name="columnIndex"></param>
      /// <param name="rowLength"></param>
      /// <param name="columnLength"></param>
      /// <param name="predicate"></param>
      /// <param name="pattern"></param>
      public static void Fill<T>(T[,] source, int rowIndex, int columnIndex, int rowLength, int columnLength, System.Func<T, bool> predicate, params System.ReadOnlySpan<T> pattern)
      {
        System.Array.AssertRank(source, 2);
        System.ArgumentNullException.ThrowIfNull(predicate);
        System.ArgumentOutOfRangeException.ThrowIfZero(pattern.Length);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        System.Range.AssertInRange(rowIndex, rowLength, rows);
        System.Range.AssertInRange(columnIndex, columnLength, cols);

        var index = 0;

        for (var i = 0; i < rowLength; i++)
          for (var j = 0; j < columnLength; j++)
            if (predicate(source[rowIndex + i, columnIndex + j]))
              source[rowIndex + i, columnIndex + j] = pattern[index++ % pattern.Length];
      }

      /// <summary>
      /// <para>Fill <paramref name="length"/> strands (in a <paramref name="dimension"/>) from <paramref name="index"/> in <paramref name="source"/> with a <paramref name="pattern"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="dimension"></param>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="pattern"></param>
      /// <exception cref="NotImplementedException"></exception>
      public static void Fill<T>(TwoDimensionalArrayAxis dimension, T[,] source, int index, int length, System.Func<T, bool> predicate, params System.ReadOnlySpan<T> pattern)
      {
        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            Fill(source, index, 0, length, source.GetLength(1), predicate, pattern);
            break;
          case TwoDimensionalArrayAxis.Column:
            Fill(source, 0, index, source.GetLength(0), length, predicate, pattern);
            break;
          default:
            throw new NotImplementedException();
        }
      }

      #endregion

      #region Flip

      /// <summary>
      /// <para>Flip the order of all dimension-0 strands (i.e. rows) from <paramref name="source"/> into <paramref name="target"/>.</para>
      /// <para>Flip the order of all dimension-1 strands (i.e. columns) from <paramref name="source"/> into <paramref name="target"/>.</para>
      /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <exception cref="System.ArgumentException"></exception>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public static void Flip<T>(TwoDimensionalArrayAxis dimension, T[,] source, T[,]? target = null)
      {
        System.Array.AssertRank(source, 2);

        target ??= source;

        System.Array.AssertRank(target, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            var rowsM1 = rows - 1;
            var rowsD2 = rows / 2;

            for (var c = 0; c < cols; c++)
              for (var r = 0; r < rowsD2; r++)
              {
                var tmp = source[r, c];
                target[r, c] = source[rowsM1 - r, c];
                target[rowsM1 - r, c] = tmp;
              }
            break;
          case TwoDimensionalArrayAxis.Column:
            var colsM1 = cols - 1;
            var colsD2 = cols / 2;

            for (var r = 0; r < rows; r++)
              for (var c = 0; c < colsD2; c++)
              {
                var tmp = source[r, c];
                target[r, c] = source[r, colsM1 - c];
                target[r, colsM1 - c] = tmp;
              }
            break;
          default:
            throw new System.NotImplementedException();
        }
      }

      #endregion

      #region Insert..

      /// <summary>
      /// <para>Insert by skipping strands, in a specified <paramref name="dimension"/>, controlled by a <paramref name="predicate"/>. No dimensional alterations are done to either <paramref name="source"/> or <paramref name="target"/>.</para>
      /// <para>If <c><paramref name="source"/> == <paramref name="target"/></c>, or <c><paramref name="target"/> <see langword="is"/> <see langword="null"/></c>, an in-place insert-by-skipping is performed in <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="dimension"></param>
      /// <param name="predicate"></param>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static T[,] InsertBySkipping<T>(TwoDimensionalArrayAxis dimension, System.Func<int, bool> predicate, T[,] source, T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        System.Array.AssertRank(source, 2);

        target ??= source;

        System.Array.AssertRank(target, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            for (int r = 0, k = 0; r < rows; r++)
            {
              if (predicate(r + k))
                k++;

              for (var c = 0; c < cols; c++) // All dimension 1 elements are always copied.
                target[r + k, c] = source[r, c];
            }
            break;
          case TwoDimensionalArrayAxis.Column:
            for (var r = 0; r < rows; r++) // All dimension 0 elements are always copied.
            {
              for (int c = 0, k = 0; c < cols; c++)
              {
                if (predicate(c + k))
                  k++;

                target[r, c + k] = source[r, c];
              }
            }
            break;
          default:
            throw new System.NotImplementedException();
        }

        return target;
      }

      /// <summary>
      /// <para>Modifies a single dimensional array by inserting <paramref name="length"/> of <typeparamref name="T"/> at <paramref name="index"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static void InsertInPlace<T>(ref T[] source, int index, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Index.AssertInRange(index, source.Length);

        System.Array.Resize(ref source, source.Length + length);

        if (source.Length - index - length is var overlapLength && overlapLength > 0) // Move needed?
        {
          System.Array.Copy(source, index, source, index + length, overlapLength); // Copy overlapping elements to the right of insert segment.
          System.Array.Clear(source, index, length); // Clear all overlapped slots.
        }
      }

      /// <summary>
      /// <para>Modifies a single dimensional array by inserting the elements of <paramref name="pattern"/> at <paramref name="index"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <param name="pattern"></param>
      public static void InsertInPlace<T>(ref T[] source, int index, int length, params T[] pattern)
      {
        InsertInPlace(ref source, index, length);

        var patternIndex = 0;

        while (--length >= 0)
          source[index++] = pattern[patternIndex++ % pattern.Length];
      }

      /// <summary>
      /// <para>Create a new array with all elements from <paramref name="source"/> and <paramref name="length"/> elements inserted at <paramref name="index"/>.</para>
      /// </summary>
      public static T[] InsertToCopy<T>(T[] source, int index, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        System.Index.AssertInRange(index, source.Length);

        var target = new T[source.Length + length];

        if (index > 0) // Any left side (of the requested insert range) elements to move?
          System.Array.Copy(source, 0, target, 0, index); // Copy left side elements into the copy.

        if (target.Length - index - length is var overlapLength && overlapLength > 0) // Any right side (of the requested insert range) elements to move?
          System.Array.Copy(source, index, target, index + length, overlapLength); // Copy "overlapping" elements to the right of insert segment into the copy.

        return target;
      }

      /// <summary>
      /// <para>Create a new array with all elements from <paramref name="source"/> and <paramref name="length"/> instances of <paramref name="pattern"/> inserted at <paramref name="index"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="pattern"></param>
      /// <returns></returns>
      public static T[] InsertToCopy<T>(T[] source, int index, int length, params T[] pattern)
      {
        var target = InsertToCopy(source, index, length);

        var patternIndex = 0;

        while (--length >= 0)
          target[index++] = pattern[patternIndex++ % pattern.Length];

        return target;
      }

      /// <summary>
      /// <para>Creates a new array with all elements from <paramref name="source"/> and additionally inserting <paramref name="length"/> new contiguous strands (of rows or colums) in the specified <paramref name="dimension"/> at the <paramref name="index"/>. All values from the <paramref name="source"/> are copied.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="dimension"></param>
      /// <param name="source"></param>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      /// <exception cref="System.NotImplementedException"></exception>
      public static T[,] InsertToCopy<T>(TwoDimensionalArrayAxis dimension, T[,] source, int index, int length)
      {
        System.Array.AssertRank(source, 2);

        System.ArgumentOutOfRangeException.ThrowIfNegative(index);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        T[,] target;

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            target = new T[rows + length, cols];
            for (var r = 0; r < rows; r++)
              for (var c = 0; c < cols; c++)
                target[r + (r >= index ? length : 0), c] = source[r, c];
            break;
          case TwoDimensionalArrayAxis.Column:
            target = new T[rows, cols + length];
            for (var r = 0; r < rows; r++)
              for (var c = 0; c < cols; c++)
                target[r, c + (c >= index ? length : 0)] = source[r, c];
            break;
          default:
            throw new System.NotImplementedException();
        }

        return target;
      }

      #endregion

      #region Remove..

      /// <summary>
      /// <para>Remove by skipping strands, in a specified <paramref name="dimension"/>, controlled by a <paramref name="predicate"/>. No dimensional alterations are done to either <paramref name="source"/> or <paramref name="target"/>, i.e. only data is moved.</para>
      /// <para>If <c><paramref name="source"/> == <paramref name="target"/></c>, or <c><paramref name="target"/> <see langword="is"/> <see langword="null"/></c>, an in-place remove-by-skipping is performed in <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="dimension"></param>
      /// <param name="predicate"></param>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static T[,] RemoveBySkipping<T>(TwoDimensionalArrayAxis dimension, System.Func<int, bool> predicate, T[,] source, T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);

        System.Array.AssertRank(source, 2);

        target ??= source;

        System.Array.AssertRank(target, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            for (int r = 0, k = 0; r < rows; r++)
              if (!predicate(r))
              {
                for (var c = 0; c < cols; c++) // All dimension 1 elements are always copied.
                  target[k, c] = source[r, c];

                k++;
              }
            break;
          case TwoDimensionalArrayAxis.Column:
            for (var r = 0; r < rows; r++) // All dimension 0 elements are always copied.
              for (int c = 0, k = 0; c < cols; c++)
                if (!predicate(c))
                  target[r, k++] = source[r, c];
            break;
          default:
            throw new System.NotImplementedException();
        }

        return target;
      }

      /// <summary>
      /// <para>Modify <paramref name="source"/> by removing <paramref name="length"/> elements starting at <paramref name="index"/>.</para>
      /// </summary>
      public static void RemoveInPlace<T>(ref T[] source, int index, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        if (endIndex < source.Length) // Copy right-side, if needed.
          System.Array.Copy(source, endIndex, source, index, source.Length - endIndex);

        System.Array.Resize(ref source, source.Length - length);
      }

      /// <summary>
      /// <para>Create a new array with <paramref name="length"/> elements removed from the <paramref name="source"/> starting at <paramref name="index"/>.</para>
      /// </summary>
      public static T[] RemoveToCopy<T>(T[] source, int index, int length)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        var target = new T[source.Length - length];

        if (index > 0) // Copy left-side, if needed.
          System.Array.Copy(source, 0, target, 0, index);

        if (endIndex < source.Length) // Copy right-side, if needed.
          System.Array.Copy(source, endIndex, target, index, source.Length - endIndex);

        return target;
      }

      public static T[,] RemoveToCopy<T>(TwoDimensionalArrayAxis dimension, T[,] source, params int[] indices)
      {
        System.Array.AssertRank(source, 2);

        var hs = new System.Collections.Generic.HashSet<int>(indices);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        T[,] target;

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            target = new T[rows - hs.Count, cols];
            RemoveBySkipping(dimension, i => indices.Contains(i), source, target);
            break;
          case TwoDimensionalArrayAxis.Column:
            target = new T[rows, cols - hs.Count];
            RemoveBySkipping(dimension, i => indices.Contains(i), source, target);
            break;
          default:
            throw new System.NotImplementedException();
        }

        return target;
      }

      #endregion

      #region Rotate

      /// <summary>
      /// <para>Rotate any two-dimensional array, square/rectangular, in-place/copy-to, counter-/clock-wise direction.</para>
      /// <para>For square arrays: Forming cycles - O(n^2) Time and O(1) Space. Without any extra space, rotate the array in form of cycles. For example, a 4 X 4 matrix will have 2 cycles. The first cycle is formed by its 1st row, last column, last row, and 1st column. The second cycle is formed by the 2nd row, second-last column, second-last row, and 2nd column. The idea is for each square cycle, to swap the elements involved with the corresponding cell in the matrix in an anticlockwise direction i.e. from top to left, left to bottom, bottom to right, and from right to top one at a time using nothing but a temporary variable to achieve this.</para>
      /// <para>For rectangular arrays: naive approach.</para>
      /// </summary>
      /// <remarks>
      /// <para>If <c><paramref name="target"/> == <paramref name="source"/></c> or <c><paramref name="target"/> <see langword="is"/> <see langword="null"/></c>, the result is in-place. If <c><paramref name="target"/> != <paramref name="source"/></c>, the result is in <paramref name="target"/> and <paramref name="source"/> remains unchanged. The <paramref name="target"/> is always returned.</para>
      /// <para>A two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. <c>dimension-0 = row</c>, and <c>dimension-1 = column</c>.</para>
      /// </remarks>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <exception cref="System.ArgumentException"></exception>
      public static T[,] Rotate<T>(RotationalDirection rotationalDirection, T[,] source, T[,]? target = null)
      {
        System.Array.AssertRank(source, 2);

        target ??= source;

        System.Array.AssertRank(target, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        System.ArgumentOutOfRangeException.ThrowIfLessThan(target.GetLength(0), cols);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(target.GetLength(1), rows);

        if (rows == cols) // Square 2D array.
          switch (rotationalDirection)
          {
            // For speed, the switch statement is outside the loops, even though it could be inside the loop.

            case RotationalDirection.ClockWise:
              {
                var rowsM1 = rows - 1;

                for (var i = 0; i < rows / 2; i++) // Consider all cycles one by one.
                {
                  var rowsM1Mi = rowsM1 - i;

                  for (var j = i; j < rowsM1Mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
                  {
                    var rowsM1Mj = rowsM1 - j;

                    // P1 = [i, j]
                    // P2 = [n-1-j, i]
                    // P3 = [n-1-i, n-1-j]
                    // P4 = [j, n-1-i]

                    var pt = source[i, j];                          // Move P1 to pt

                    target[i, j] = source[rowsM1Mj, i];              // Move P2 to P1
                    target[rowsM1Mj, i] = source[rowsM1Mi, rowsM1Mj];  // Move P3 to P4
                    target[rowsM1Mi, rowsM1Mj] = source[j, rowsM1Mi];  // Move P4 to P3
                    target[j, rowsM1Mi] = pt;                        // Move pt to P4
                  }
                }
              }
              break;
            case RotationalDirection.CounterClockWise:
              {
                var rowsM1 = rows - 1;

                for (var i = 0; i < rows / 2; i++) // Consider all cycles one by one.
                {
                  var rowsM1Mi = rowsM1 - i;

                  for (var j = i; j < rowsM1Mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
                  {
                    var rowsM1Mj = rowsM1 - j;

                    // P1 = [i, j]
                    // P2 = [j, n-1-i]
                    // P3 = [n-1-i, n-1-j]
                    // P4 = [n-1-j, i]

                    var pt = source[i, j];                          // Move P1 to pt

                    target[i, j] = source[j, rowsM1Mi];              // Move P2 to P1
                    target[j, rowsM1Mi] = source[rowsM1Mi, rowsM1Mj];  // Move P3 to P4
                    target[rowsM1Mi, rowsM1Mj] = source[rowsM1Mj, i];  // Move P4 to P3
                    target[rowsM1Mj, i] = pt;                        // Move pt to P4
                  }
                }
              }
              break;
            default:
              throw new System.NotImplementedException();
          }
        else // Rectangular 2D array.
          switch (rotationalDirection)
          {
            case RotationalDirection.ClockWise:
              for (var r = 0; r < rows; r++)
                for (var c = 0; c < cols; c++)
                  target[c, rows - 1 - r] = source[r, c];
              break;
            case RotationalDirection.CounterClockWise:
              for (var r = 0; r < rows; r++)
                for (var c = 0; c < cols; c++)
                  target[cols - 1 - c, r] = source[r, c];
              break;
            default:
              throw new System.NotImplementedException();
          }

        return target;
      }

      #endregion

      /// <summary>
      /// <para>Swap two values, [<paramref name="a0"/>, <paramref name="a1"/>] and [<paramref name="b0"/>, <paramref name="b1"/>], in <paramref name="source"/>.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public static void Swap<T>(T[,] source, int a0, int a1, int b0, int b1)
        => (source[b0, b1], source[a0, a1]) = (source[a0, a1], source[b0, b1]);

      #region To..

      /// <summary>
      /// <para>Create a new <see cref="System.Data.DataTable"/> from <paramref name="source"/>.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public static System.Data.DataTable ToDataTable<T>(T[,] source, bool sourceHasColumnNames, params string[] customColumnNames)
      {
        AssertRank(source, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        var dt = new System.Data.DataTable();

        for (var c = 0; c < cols; c++)
        {
          var columnName = default(string);

          if (sourceHasColumnNames) // First choice, if sourceHasColumnNames is true, use source value.
            columnName = source[0, c]?.ToString();
          else if (c < customColumnNames.Length) // Second choice, if sourceColumnNames is false, use custom column name, if present.
            columnName = customColumnNames[c];

          dt.Columns.Add(columnName ?? c.ToSingleOrdinalColumnName()); // Third choice, if columnName is still null (string default), use ToSingleOrdinalColumnName(), e.g. "Column1".
        }

        for (var r = sourceHasColumnNames ? 1 : 0; r < rows; r++)
        {
          var array = new object[cols];

          for (var c = 0; c < cols; c++)
            array[c] = source[r, c]!;

          dt.Rows.Add(array);
        }

        return dt;
      }

      /// <summary>
      /// <para>Creates a new array with <paramref name="count"/> elements from <paramref name="source"/> starting at <paramref name="index"/>. Use <paramref name="preLength"/> and <paramref name="postLength"/> arguments to add surrounding element space in the new array.</para>
      /// </summary>
      /// <param name="source">W</param>
      /// <param name="index"></param>
      /// <param name="count"></param>
      /// <param name="preLength"></param>
      /// <param name="postLength"></param>
      /// <returns></returns>
      public static T[] ToCopy<T>(T[] source, int index, int length, int preLength = 0, int postLength = 0)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new T[preLength + length + postLength];
        System.Array.Copy(source, index, target, preLength, length);
        return target;
      }

      #endregion

      #region Transpose

      /// <summary>
      /// <para>Transpose all elements in <paramref name="source"/> into target.</para>
      /// <para>If <c><paramref name="target"/> == <paramref name="source"/></c> or <c><paramref name="target"/> <see langword="is"/> <see langword="null"/></c>, the result is in-place. If <c><paramref name="target"/> != <paramref name="source"/></c>, the result is in <paramref name="target"/> and <paramref name="source"/> remains unchanged. The <paramref name="target"/> is always returned.</para>
      /// <see href="https://en.wikipedia.org/wiki/Transpose"/>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public static T[,] Transpose<T>(T[,] source, T[,]? target = null)
      {
        System.Array.AssertRank(source, 2);

        target ??= source;

        System.Array.AssertRank(target, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        System.ArgumentOutOfRangeException.ThrowIfLessThan(target.GetLength(0), cols);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(target.GetLength(1), rows);

        if (target == source) // If target and source are the same
          for (var r = 0; r < rows; r++)
            for (var c = r + 1; c < cols; c++)
            {
              var temp = source[r, c];
              target[r, c] = source[c, r];
              target[c, r] = temp;
            }
        else // If target is a copy (i.e. not source).
          for (var r = 0; r < rows; r++)
            for (var c = 0; c < cols; c++)
              target[r, c] = source[c, r];

        return target;
      }

      #endregion

      /// <summary>
      /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public static string JaggedArrayToConsoleString<T>(T[][] source, ConsoleFormatOptions? options = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        options ??= ConsoleFormatOptions.Default;

        var rows = source.GetLength(0);
        var cols = source.Max(a => a.Length); // Here, length1 = the MAX number of elements found in all sub-arrays of the jagged array.

        #region MaxWidths

        var maxWidths = new int[cols]; // Create an array to hold the max widths of all elements in the largest sub-array.

        for (var r = rows - 1; r >= 0; r--)
          for (var c = source[r].Length - 1; c >= 0; c--)
            maxWidths[c] = int.Max(maxWidths[c], (source[r][c]?.ToString() ?? string.Empty).Length); // Find the max width for each column from all sub-arrays.

        #endregion // MaxWidths

        var sb = new System.Text.StringBuilder();

        var verticalString = options.CreateVerticalString(maxWidths);

        for (var r = 0; r < rows; r++)
        {
          if (r > 0)
          {
            sb.AppendLine();

            if (verticalString is not null)
              sb.AppendLine(verticalString);
          }

          var horizontalString = options.CreateHorizontalString(source[r], maxWidths);

          sb.Append(horizontalString);
        }

        return sb.ToString();
      }

      /// <summary>
      /// <para>Creates a new two-dimensional array from the jagged array (i.e. an array of arrays). The outer array becomes dimension-0 (rows) and the inner arrays make up each dimension-1 (columns).</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public static T[,] JaggedArrayToRank2<T>(T[][] source)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new T[source.Length, source.Max(t => t.Length)];

        for (var i = source.Length - 1; i >= 0; i--)
          for (var j = source[i].Length - 1; j >= 0; j--)
            target[i, j] = source[i][j];

        return target;
      }

      /// <summary>
      /// <para>Returns the one-dimensional array as a string-builder, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public static string Rank1ToConsoleString<T>(T[] source, ConsoleFormatOptions? options = null)
        => JaggedArrayToConsoleString([source], options);

      /// <summary>
      /// <para>Creates a new jagged array with either a one-element dimension-0 array and the array as dimension-1, i.e. <c>T[][source-elements]</c> (horizontal), or the array pivoted as dimension-0 and zero length subarrays, i.e. <c>T[source-elements][]</c> (vertical).</para>
      /// </summary>
      public static T[][] Rank1ToJaggedArray<T>(T[] source, bool pivot = false)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (pivot)
          return [source];
        else
        {
          var ja = new T[1][];
          ja[0] = source;
          return ja;
        }
      }

      /// <summary>
      /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public static string Rank2ToConsoleString<T>(T[,] source, ConsoleFormatOptions? options = null)
      {
        System.Array.AssertRank(source, 2);

        options ??= ConsoleFormatOptions.Default;

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        #region MaxWidths

        var maxWidths = new int[cols];

        for (var r = rows - 1; r >= 0; r--)
          for (var c = cols - 1; c >= 0; c--)
            maxWidths[c] = int.Max(maxWidths[c], (source[r, c]?.ToString() ?? string.Empty).Length);

        #endregion

        var sb = new System.Text.StringBuilder();

        var verticalString = options.CreateVerticalString(maxWidths);

        var horizontalFormat = options.CreateHorizontalFormat(maxWidths);

        for (var r = 0; r < rows; r++) // Consider row as dimension 0.
        {
          if (r > 0)
          {
            sb.AppendLine();

            if (!string.IsNullOrEmpty(verticalString) && r > 0)
              sb.AppendLine(verticalString);
          }

          var horizontalString = options.CreateHorizontalString(Rank2StrandToRank1(TwoDimensionalArrayAxis.Row, source, r), maxWidths, horizontalFormat);

          sb.Append(horizontalString);
        }

        return sb.ToString();
      }

      /// <summary>
      /// <para>Create a new jagged array with all elements in source in a <paramref name="dimension"/>-major-order, i.e. by row or by column first (then the other).</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public static T[][] Rank2ToJaggedArray<T>(T[,] source, bool pivot)
      {
        System.Array.AssertRank(source, 2);

        var target = new T[source.GetLength(pivot ? 1 : 0)][];

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        if (pivot)
          for (var c = 0; c < cols; c++)
          {
            var jaggedDimension = new T[rows];

            for (var r = 0; r < rows; r++)
              jaggedDimension[r] = source[r, c];

            target[c] = jaggedDimension;
          }
        else
          for (var r = 0; r < rows; r++)
          {
            var jaggedDimension = new T[cols];

            for (var c = 0; c < cols; c++)
              jaggedDimension[c] = source[r, c];

            target[r] = jaggedDimension;
          }

        return target;
      }

      /// <summary>
      /// <para>Create a new sequence with elements in <paramref name="source"/> from the specified <paramref name="dimension"/> and <paramref name="index"/> (within the <paramref name="dimension"/>).</para>
      /// <example>One can interpret the parameters as all elements of the fourth (<paramref name="index"/> = 3, zero-based) "row" (<paramref name="dimension"/> = 0), or all elements of the fourth (<paramref name="index"/> = 3, zero-based) "column" (<paramref name="dimension"/> = 1).</example>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      /// <param name="dimension"></param>
      /// <param name="index"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static T[] Rank2StrandToRank1<T>(TwoDimensionalArrayAxis dimension, T[,] source, int index)
      {
        System.Array.AssertRank(source, 2);

        System.Index.AssertInRange(index, source.GetLength((int)dimension));

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        T[] target;

        switch (dimension)
        {
          case TwoDimensionalArrayAxis.Row:
            target = new T[cols];
            for (var c = 0; c < cols; c++)
              target[c] = source[index, c];
            break;
          case TwoDimensionalArrayAxis.Column:
            target = new T[rows];
            for (var r = 0; r < rows; r++)
              target[r] = source[r, index];
            break;
          default:
            throw new System.NotImplementedException();
        }

        return target;
      }

      /// <summary>
      /// <para>Create a new array with all elements in source with the specified <paramref name="dimension"/>-major-order, i.e. by row or by column first (then the other).</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      /// <param name="dimension"></param>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static T[] Rank2ToRank1<T>(TwoDimensionalArrayAxis dimension, T[,] source)
      {
        System.Array.AssertRank(source, 2);

        var rows = source.GetLength(0);
        var cols = source.GetLength(1);

        var target = new T[rows * cols];

        if (dimension is TwoDimensionalArrayAxis.Column) // If not row first..
          (rows, cols) = (cols, rows); // ..then column first.

        var targetIndex = 0;

        for (var i = 0; i < rows; i++)
          for (var j = 0; j < cols; j++)
            target[targetIndex++] = source[i, j];

        return target;
      }
    }

    extension(System.Array source)
    {
      /// <summary>
      /// <para>Identifies the type of array.</para>
      /// </summary>
      /// <returns></returns>
      public ArrayType ArrayType
      {
        get
        {
          if (source.GetType() is var type && type.IsArray)
          {
            if (source.Rank == 1)
              return (type.GetElementType()?.IsArray ?? false) ? ArrayType.JaggedArray : ArrayType.OneDimensionalArray;
            else if (source.Rank == 2)
              return ArrayType.TwoDimensionalArray;
            else if (source.Rank > 2)
              return ArrayType.MultiDimensionalArray;
          }

          return ArrayType.NotAnArray;
        }
      }

      /// <summary>
      /// <para>Determines whether the <see cref="System.Array"/> is a jagged-array, i.e. an array of arrays.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      [System.Obsolete("Use property ArrayType instead.")]
      public bool IsJaggedArray()
        => source.GetType() is var type && type.IsArray && (type.GetElementType()?.IsArray ?? false);

      /// <summary>
      /// <para>Returns a one/two-dimensional or jagged array as a new <see cref="SpanMaker{T}"/>, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public string ToConsoleString<T>(ConsoleFormatOptions? options = null)
      {
        return source.ArrayType switch
        {
          ArrayType.NotAnArray => "Not an array.",
          ArrayType.OneDimensionalArray => System.Array.Rank1ToConsoleString((T[])source, options),
          ArrayType.TwoDimensionalArray => System.Array.Rank2ToConsoleString((T[,])source, options),
          ArrayType.MultiDimensionalArray => "Multi-dimensional array.",
          ArrayType.JaggedArray => System.Array.JaggedArrayToConsoleString((T[][])source, options),
          _ => throw new NotImplementedException(),
        };
      }

      public string ToConsoleString(ConsoleFormatOptions? options = null)
        => ToConsoleString<object>(source, options);
    }
  }
}
