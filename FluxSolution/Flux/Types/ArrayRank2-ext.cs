namespace Flux
{
  public static partial class XtensionArrayRank2
  {
    extension<T>(T[,] source)
    {
      /// <summary>
      /// <para>Concatenates the source and the <paramref name="other"/> array in a <paramref name="dimension"/>-major-order.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="other">The "second" array.</param>
      /// <param name="dimension">The direction of the concatenation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public T[,] ConcatToCopy(T[,] other, int dimension)
      {
        source.AssertRank(2);
        other.AssertRank(2);

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);
        var tl0 = other.GetLength(0);
        var tl1 = other.GetLength(1);

        T[,] concat;

        switch (dimension)
        {
          case 0:
            concat = new T[(sl0 + tl0), int.Max(sl1, tl1)];
            source.Copy(concat, 0, 0, 0, 0, sl0, sl1);
            other.Copy(concat, 0, 0, sl0, 0, tl0, tl1);
            break;
          case 1:
            concat = new T[int.Max(sl0, tl0), (sl1 + tl1)];
            source.Copy(concat, 0, 0, 0, 0, sl0, sl1);
            other.Copy(concat, 0, 0, 0, sl1, tl0, tl1);
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(dimension));
        }

        return concat;
      }

      /// <summary>
      /// <para>Concatenates the source and <paramref name="other"/> array in a <paramref name="dimension"/>-major-order.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source">The "first" array.</param>
      /// <param name="other">The "second" array.</param>
      /// <param name="dimension">The direction of the concatenation.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public T[,] ConcatToCopy(T[,] other, ArrayDimensionLabel dimension) => ConcatToCopy(source, other, (int)dimension);

      /// <summary>
      /// <para>Copies <paramref name="count0"/> rows (dimension-0 elements) by <paramref name="count1"/> columns (dimension-1 elements), i.e. a block, from <paramref name="source"/> starting at [<paramref name="sourceIndex0"/>, <paramref name="sourceIndex1"/>] into <paramref name="target"/> starting at [<paramref name="targetIndex0"/>, <paramref name="targetIndex1"/>].</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void Copy(T[,] target, int sourceIndex0, int sourceIndex1, int targetIndex0, int targetIndex1, int count0, int count1)
      {
        for (var i = count0 - 1; i >= 0; i--)
          for (var j = count1 - 1; j >= 0; j--)
            target[targetIndex0 + i, targetIndex1 + j] = source[sourceIndex0 + i, sourceIndex1 + j];
      }

      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public T[,] Copy0Insert(T[,]? target = null, params int[] indices0)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        var hs = indices0.Where(r => r < sl0).ToHashSet();

        for (int i = 0, k = 0; i < sl0; i++)
        {
          if (indices0.Contains(i + k))
            k++;

          for (var j = 0; j < sl1; j++) // All dimension 1 elements are always copied.
            target[i + k, j] = source[i, j];
        }

        return target;
      }

      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public T[,] Copy1Insert(T[,]? target = null, params int[] indices1)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        var hs = indices1.Where(r => r < sl1).ToHashSet();

        for (var i = 0; i < sl0; i++) // All dimension 0 elements are always copied.
        {
          for (int j = 0, k = 0; j < sl1; j++)
          {
            if (indices1.Contains(j + k))
              k++;

            target[i, j + k] = source[i, j];
          }
        }

        return target;
      }

      /// <summary>
      /// <para>Copy all dimension-0 elements (i.e. rows) from <paramref name="source"/> into <paramref name="target"/> except the specified <paramref name="indices0"/> (rows).</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public T[,] Copy0Remove(T[,]? target = null, params int[] indices0)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        var hs = indices0.Where(r => r < sl0).ToHashSet();

        for (int i = 0, k = 0; i < sl0; i++)
          if (!indices0.Contains(i))
          {
            for (var j = 0; j < sl1; j++) // All dimension 1 elements are always copied.
              target[k, j] = source[i, j];

            k++;
          }

        return target;
      }

      /// <summary>
      /// <para>Copy all dimension-1 elements (i.e. columns) from <paramref name="source"/> into <paramref name="target"/> except the specified <paramref name="indices1"/> (columns).</para>
      /// <para></para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public T[,] Copy1Remove(T[,]? target = null, params int[] indices1)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        var hs = indices1.Where(r => r < sl1).ToHashSet();

        for (var i = 0; i < sl0; i++) // All dimension 0 elements are always copied.
          for (int j = 0, k = 0; j < sl1; j++)
            if (!indices1.Contains(j))
              target[i, k++] = source[i, j];

        return target;
      }

      /// <summary>
      /// <para>Copies <paramref name="count"/> rows (dimension-0 strands) from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> starting at <paramref name="targetIndex"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="sourceIndex"></param>
      /// <param name="target"></param>
      /// <param name="targetIndex"></param>
      /// <param name="count"></param>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void Copy0Range(int sourceIndex, T[,] target, int targetIndex, int count)
        => source.Copy(target, sourceIndex, 0, targetIndex, 0, count, source.GetLength(1));

      /// <summary>
      /// <para>Copies <paramref name="count"/> columns (dimension-1 strands) from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> starting at <paramref name="targetIndex"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="sourceIndex"></param>
      /// <param name="target"></param>
      /// <param name="targetIndex"></param>
      /// <param name="count"></param>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void Copy1Range(int sourceIndex, T[,] target, int targetIndex, int count)
        => source.Copy(target, 0, sourceIndex, 0, targetIndex, source.GetLength(0), count);

      /// <summary>
      /// <para>Fill <paramref name="source"/> with the specified <paramref name="pattern"/>, at <paramref name="index0"/>, <paramref name="index1"/> and <paramref name="count0"/> and <paramref name="count1"/>. Using a sort of continuous flow fill.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public void Fill(int index0, int index1, int count0, int count1, params System.ReadOnlySpan<T> pattern)
      {
        source.AssertRank(2);

        var length0 = source.GetLength(0);
        if (index0 < 0 || index0 > length0) throw new System.ArgumentOutOfRangeException(nameof(index0));

        var length1 = source.GetLength(1);
        if (index1 < 0 || index1 > length1) throw new System.ArgumentOutOfRangeException(nameof(index1));

        if (index0 + count0 > length0) throw new System.ArgumentOutOfRangeException(nameof(count0));
        if (index1 + count1 > length1) throw new System.ArgumentOutOfRangeException(nameof(count1));

        var index = 0;

        for (var i = 0; i < count0; i++)
          for (var j = 0; j < count1; j++)
            source[index0 + i, index1 + j] = pattern[index++ % pattern.Length];
      }

      public void Fill0(int index, int count, params System.ReadOnlySpan<T> pattern)
        => source.Fill(index, 0, count, source.GetLength(1), pattern);

      public void Fill1(int index, int count, params System.ReadOnlySpan<T> pattern)
        => source.Fill(0, index, source.GetLength(0), count, pattern);

      /// <summary>
      /// <para>Flip the order of all dimension-0 strands (i.e. rows) from <paramref name="source"/> into <paramref name="target"/>.</para>
      /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <exception cref="System.ArgumentException"></exception>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void Flip0(T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
          throw new System.ArgumentException($"Array flip requires target minimum dimensional symmetry.");

        var sl0sub1 = sl0 - 1;
        var sl0half = sl0 / 2;

        for (var i = 0; i < sl1; i++)
          for (var j = 0; j < sl0half; j++)
          {
            var tmp = source[j, i];
            target[j, i] = source[sl0sub1 - j, i];
            target[sl0sub1 - j, i] = tmp;
          }
      }

      /// <summary>
      /// <para>Flip the order of all dimension-1 strands (i.e. columns) from <paramref name="source"/> into <paramref name="target"/>.</para>
      /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <exception cref="System.ArgumentException"></exception>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void Flip1(T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
          throw new System.ArgumentException($"Array flip requires target minimum dimensional symmetry.");

        var sl1sub1 = sl1 - 1;
        var sl1half = sl1 / 2;

        for (var i = 0; i < sl0; i++)
          for (var j = 0; j < sl1half; j++)
          {
            var tmp = source[i, j];
            target[i, j] = source[i, sl1sub1 - j];
            target[i, sl1sub1 - j] = tmp;
          }
      }

      /// <summary>
      /// <para>Create a new array with all elements in source with the specified <paramref name="dimension"/>-major-order, i.e. by row or by column first (then the other).</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[] GetAllElements(int dimension)
      {
        source.AssertRank(2);

        var sourceLength0 = source.GetLength(0);
        var sourceLength1 = source.GetLength(1);

        var target = new T[sourceLength0 * sourceLength1];

        var targetIndex = 0;

        switch (dimension)
        {
          case 0:
            for (var s0 = 0; s0 < sourceLength0; s0++)
              for (var s1 = 0; s1 < sourceLength1; s1++)
                target[targetIndex++] = source[s0, s1];
            break;
          case 1:
            for (var s1 = 0; s1 < sourceLength1; s1++)
              for (var s0 = 0; s0 < sourceLength0; s0++)
                target[targetIndex++] = source[s0, s1];
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(dimension));
        }

        return target;
      }

      /// <summary>
      /// <para>Create a new array with all elements in source with the specified <paramref name="dimension"/>-major-order, i.e. by row or by column first (then the other).</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[] GetAllElements(ArrayDimensionLabel dimension) => GetAllElements(source, (int)dimension);

      /// <summary>
      /// <para>Create a new sequence with elements in <paramref name="source"/> from the specified <paramref name="dimension"/> and <paramref name="index"/> (within the <paramref name="dimension"/>).</para>
      /// <example>One can interpret the parameters as all elements of the fourth (<paramref name="index"/> = 3, zero-based) "row" (<paramref name="dimension"/> = 0), or all elements of the fourth (<paramref name="index"/> = 3, zero-based) "column" (<paramref name="dimension"/> = 1).</example>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[] GetElements(int dimension, int index)
      {
        source.AssertRank(2);

        var sourceLength0 = source.GetLength(0);
        var sourceLength1 = source.GetLength(1);

        T[] target;

        switch (dimension)
        {
          case 0:
            target = new T[sourceLength1];
            for (var s1 = 0; s1 < sourceLength1; s1++)
              target[s1] = source[index, s1];
            break;
          case 1:
            target = new T[sourceLength0];
            for (int s0 = 0; s0 < sourceLength0; s0++)
              target[s0] = source[s0, index];
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(dimension));
        }

        return target;
      }

      /// <summary>
      /// <para>Create a new sequence with elements in <paramref name="source"/> from the specified <paramref name="dimension"/> and <paramref name="index"/> (within the <paramref name="dimension"/>).</para>
      /// <example>One can interpret the parameters as all elements of the fourth (<paramref name="index"/> = 3, zero-based) "row" (<paramref name="dimension"/> = 0), or all elements of the fourth (<paramref name="index"/> = 3, zero-based) "column" (<paramref name="dimension"/> = 1).</example>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[] GetElements(ArrayDimensionLabel dimension, int index) => GetElements(source, (int)dimension, index);

      /// <summary>
      /// <para>Create a new array with the elements from <paramref name="source"/> and by inserting <paramref name="count"/> new contiguous strands (of rows or colums) in the specified <paramref name="dimension"/> at the <paramref name="index"/>. All values from the <paramref name="source"/> are copied.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[,] InsertToCopy(int dimension, int index, int count)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (index < 0 || index > source.GetLength(dimension)) throw new System.ArgumentOutOfRangeException(nameof(index));
        if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

        var sourceLength0 = source.GetLength(0);
        var sourceLength1 = source.GetLength(1);

        T[,] target;

        switch (dimension)
        {
          case 0:
            target = new T[sourceLength0 + count, sourceLength1];
            for (var s0 = 0; s0 < sourceLength0; s0++)
              for (var s1 = 0; s1 < sourceLength1; s1++)
                target[s0 + (s0 >= index ? count : 0), s1] = source[s0, s1];
            break;
          case 1:
            target = new T[sourceLength0, sourceLength1 + count];
            for (var s0 = 0; s0 < sourceLength0; s0++)
              for (var s1 = 0; s1 < sourceLength1; s1++)
                target[s0, s1 + (s1 >= index ? count : 0)] = source[s0, s1];
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(dimension));
        }

        return target;
      }

      /// <summary>Create a new array from the existing array, copy all elements and insert the specified items at the specified dimension and index.</summary>
      /// <param name="source">The source array from where the new array as is based.</param>
      /// <param name="dimension">The dimension to resize.</param>
      /// <param name="index">The index in the dimension where the strands should be inserted, e.g. which row or column to fill.</param>
      /// <param name="count">The number of strands to add for the dimension.</param>
      /// <param name="pattern">The items to fill at index. Using a sort of continuous flow fill.</param>
      public T[,] InsertToCopy(int dimension, int index, int count, params T[] pattern)
      {
        var target = InsertToCopy(source, dimension, index, count);

        switch (dimension)
        {
          case 0:
            if (pattern.Length > 0)
              Fill(target, index, 0, count, target.GetLength(1), pattern);
            break;
          case 1:
            if (pattern.Length > 0)
              Fill(target, 0, index, target.GetLength(0), count, pattern);
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(dimension));
        }

        return target;
      }

      public T[,] InsertToCopy0(int index, int count, params T[] pattern)
      {
        var target = InsertToCopy(source, 0, index, count);

        if (pattern.Length > 0)
          Fill0(target, index, count, pattern);

        return target;
      }

      public T[,] InsertToCopy1(int index, int count, params T[] pattern)
      {
        var target = InsertToCopy(source, 1, index, count);

        if (pattern.Length > 0)
          Fill1(target, index, count, pattern);

        return target;
      }

      /// <summary>
      /// <para>Allocates a new array, the same size as <paramref name="source"/>.</para>
      /// <para>No data is copied.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public T[,] New()
        => new T[source.GetLength(0), source.GetLength(1)];

      /// <summary>
      /// <para>Allocates a new array, based on the size of <paramref name="source"/>. Dimension-1 (i.e. rows) are added/removed with <paramref name="addOrRemove0"/> and dimension-1 (i.e. columns) are added/removed with <paramref name="addOrRemove1"/>.</para>
      /// <para>A negative value will remove and a positive value will add, as many column as the number represents.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="addOrRemove0">A negative value removes and a positive value adds, as many rows (dimension-0) as the number represents.</param>
      /// <param name="addOrRemove1">A negative value removes and a positive value adds, as many columns (dimension-1) as the number represents.</param>
      /// <returns></returns>
      public T[,] NewResize(int addOrRemove0, int addOrRemove1)
        => new T[source.GetLength(0) + addOrRemove0, source.GetLength(1) + addOrRemove1];

      /// <summary>
      /// <para>Returns the two-dimensional array as a new sequence of grid-like formatted strings, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public string Rank2ToConsoleString(ConsoleFormatOptions? options = null)
      {
        source.AssertRank(2);

        options ??= ConsoleFormatOptions.Default;

        var length0 = source.GetLength(0);
        var length1 = source.GetLength(1);

        #region MaxWidths

        var maxWidths = new int[length1];

        for (var r = length0 - 1; r >= 0; r--)
          for (var c = length1 - 1; c >= 0; c--)
            maxWidths[c] = int.Max(maxWidths[c], (source[r, c]?.ToString() ?? string.Empty).Length);

        #endregion

        var sb = new System.Text.StringBuilder();

        var verticalString = options.CreateVerticalString(maxWidths);

        var horizontalFormat = options.CreateHorizontalFormat(maxWidths);

        for (var r = 0; r < length0; r++) // Consider row as dimension 0.
        {
          if (r > 0)
          {
            sb.AppendLine();

            if (!string.IsNullOrEmpty(verticalString) && r > 0)
              sb.AppendLine(verticalString);
          }

          var horizontalString = options.CreateHorizontalString(source.GetElements(0, r), maxWidths, horizontalFormat);

          sb.Append(horizontalString);
        }

        return sb.ToString();
      }

      public T[,] Remove0ToCopy(params int[] indices)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var hs = new System.Collections.Generic.HashSet<int>(indices);

        var sourceLength0 = source.GetLength(0);
        var sourceLength1 = source.GetLength(1);

        var target = new T[sourceLength0 - hs.Count, sourceLength1];

        source.Copy0Remove(target, indices);

        return target;
      }

      public T[,] Remove1ToCopy(params int[] indices)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var hs = new System.Collections.Generic.HashSet<int>(indices);

        var sourceLength0 = source.GetLength(0);
        var sourceLength1 = source.GetLength(1);

        var target = new T[sourceLength0, sourceLength1 - hs.Count];

        source.Copy1Remove(target, indices);

        return target;
      }

      ///// <summary>
      ///// <para>Create a new array from <paramref name="source"/> with the <paramref name="indices"/> of strands (rows or columns) removed from the specified <paramref name="dimension"/>.</para>
      ///// </summary>
      ///// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      //public static T[,] RemoveToCopy<T>(this T[,] source, int dimension, params int[] indices)
      //{
      //  source.AssertRank(2);

      //  var hs = new System.Collections.Generic.HashSet<int>(indices);

      //  var sourceLength0 = source.GetLength(0);
      //  var sourceLength1 = source.GetLength(1);

      //  T[,] target;

      //  switch (dimension)
      //  {
      //    case 0:
      //      target = new T[sourceLength0 - hs.Count, sourceLength1];
      //      source.Copy0Except(target, indices);
      //      //for (int s0 = 0, t0 = 0; s0 < sourceLength0; s0++)
      //      //  if (!hs.Contains(s0))
      //      //  {
      //      //    for (var s1 = 0; s1 < sourceLength1; s1++) // All dimension 1 elements are always copied.
      //      //      target[t0, s1] = source[s0, s1];

      //      //    t0++;
      //      //  }
      //      break;
      //    case 1:
      //      target = new T[sourceLength0, sourceLength1 - hs.Count];
      //      source.Copy1Except(target, indices);
      //      //for (var s0 = 0; s0 < sourceLength0; s0++) // All dimension 0 elements are always copied.
      //      //  for (int s1 = 0, t1 = 0; s1 < sourceLength1; s1++)
      //      //    if (!hs.Contains(s1))
      //      //    {
      //      //      target[s0, t1] = source[s0, s1];

      //      //      t1++;
      //      //    }
      //      break;
      //    default:
      //      throw new System.ArgumentOutOfRangeException(nameof(dimension));
      //  }

      //  return target;
      //}

      ///// <summary>
      ///// <para>Create a new array from <paramref name="source"/> with the <paramref name="indices"/> of strands (rows or columns) removed from the specified <paramref name="dimension"/>.</para>
      ///// </summary>
      ///// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      //public static T[,] RemoveToCopy<T>(this T[,] source, ArrayDimensionLabel dimension, params int[] indices)
      //  => source.RemoveToCopy((int)dimension, indices);

      /// <summary>
      /// <para>Rotate a two-dimensional array in-place in a clock-wise direction.</para>
      /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
      /// <para>Forming Cycles: O(n^2) Time and O(1) Space. Without any extra space, rotate the array in form of cycles. For example, a 4 X 4 matrix will have 2 cycles. The first cycle is formed by its 1st row, last column, last row, and 1st column. The second cycle is formed by the 2nd row, second-last column, second-last row, and 2nd column. The idea is for each square cycle, to swap the elements involved with the corresponding cell in the matrix in an anticlockwise direction i.e. from top to left, left to bottom, bottom to right, and from right to top one at a time using nothing but a temporary variable to achieve this.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <exception cref="System.ArgumentException"></exception>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void RotateCcw(T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
          throw new System.ArgumentException($"Array rotation counter-clockwise requires target minimum dimensional symmetry.");

        var sl0m1 = sl0 - 1;

        for (var i = 0; i < sl0 / 2; i++) // Consider all cycles one by one.
        {
          var sl0m1mi = sl0m1 - i;

          for (var j = i; j < sl0m1mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
          {
            var sl0m1mj = sl0m1 - j;

            // P1 = [i, j]
            // P2 = [j, n-1-i]
            // P3 = [n-1-i, n-1-j]
            // P4 = [n-1-j, i]

            var pt = source[i, j];                          // Move P1 to pt

            target[i, j] = source[j, sl0m1mi];              // Move P2 to P1
            target[j, sl0m1mi] = source[sl0m1mi, sl0m1mj];  // Move P3 to P4
            target[sl0m1mi, sl0m1mj] = source[sl0m1mj, i];  // Move P4 to P3
            target[sl0m1mj, i] = pt;                        // Move pt to P4
          }
        }
      }

      /// <summary>
      /// <para>Rotate a two-dimensional array in-place in a counter-clock-wise direction.</para>
      /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
      /// <para>Forming Cycles: O(n^2) Time and O(1) Space. Without any extra space, rotate the array in form of cycles. For example, a 4 X 4 matrix will have 2 cycles. The first cycle is formed by its 1st row, last column, last row, and 1st column. The second cycle is formed by the 2nd row, second-last column, second-last row, and 2nd column. The idea is for each square cycle, to swap the elements involved with the corresponding cell in the matrix in an anticlockwise direction i.e. from top to left, left to bottom, bottom to right, and from right to top one at a time using nothing but a temporary variable to achieve this.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <exception cref="System.ArgumentException"></exception>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void RotateCw(T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
          throw new System.ArgumentException($"Array rotation clockwise requires target minimum dimensional symmetry.");

        var sl0m1 = sl0 - 1;

        for (var i = 0; i < sl0 / 2; i++) // Consider all cycles one by one.
        {
          var sl0m1mi = sl0m1 - i;

          for (var j = i; j < sl0m1mi; j++) // Consider elements in group of 4 as P1, P2, P3 & P4 in current square.
          {
            var sl0m1mj = sl0m1 - j;

            // P1 = [i, j]
            // P2 = [n-1-j, i]
            // P3 = [n-1-i, n-1-j]
            // P4 = [j, n-1-i]

            var pt = source[i, j];                          // Move P1 to pt

            target[i, j] = source[sl0m1mj, i];              // Move P2 to P1
            target[sl0m1mj, i] = source[sl0m1mi, sl0m1mj];  // Move P3 to P4
            target[sl0m1mi, sl0m1mj] = source[j, sl0m1mi];  // Move P4 to P3
            target[j, sl0m1mi] = pt;                        // Move pt to P4
          }
        }
      }

      /// <summary>
      /// <para>Swap two values, [<paramref name="a0"/>, <paramref name="a1"/>] and [<paramref name="b0"/>, <paramref name="b1"/>], in <paramref name="source"/>.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public void Swap(int a0, int a1, int b0, int b1)
        => (source[b0, b1], source[a0, a1]) = (source[a0, a1], source[b0, b1]);

      /// <summary>
      /// <para>Creates a new two-dimensional array by copying a selection of (<paramref name="count0"/> by <paramref name="count1"/>) elements from <paramref name="source"/> starting at [<paramref name="index0"/>, <paramref name="index1"/>]. Use (<paramref name="preCount0"/> by <paramref name="preCount1"/>) and (<paramref name="postCount0"/> by <paramref name="postCount1"/>) to add surrounding element space in the new array.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[,] ToCopy(int index0, int index1, int count0, int count1, int preCount0, int preCount1, int postCount0, int postCount1)
      {
        source.AssertRank(2);

        var target = new T[preCount0 + count0 + postCount0, preCount1 + count1 + postCount1];
        source.Copy(target, index0, index1, preCount0, preCount1, count0, count1);
        return target;
      }

      /// <summary>
      /// <para>Create a new <see cref="System.Data.DataTable"/> from <paramref name="source"/>.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public System.Data.DataTable ToDataTable(bool sourceHasColumnNames, params string[] customColumnNames)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        var dt = new System.Data.DataTable();

        for (var i1 = 0; i1 < sl1; i1++)
        {
          var columnName = default(string);

          if (sourceHasColumnNames) // First choice, if sourceHasColumnNames is true, use source value.
            columnName = source[0, i1]?.ToString();
          else if (i1 < customColumnNames.Length) // Second choice, if sourceColumnNames is false, use custom column name, if present.
            columnName = customColumnNames[i1];

          dt.Columns.Add(columnName ?? i1.ToSingleOrdinalColumnName()); // Third choice, if columnName is still null (string default), use ToSingleOrdinalColumnName(), e.g. "Column1".
        }

        for (var i0 = sourceHasColumnNames ? 1 : 0; i0 < sl0; i0++)
        {
          var array = new object[sl1];

          for (var i1 = 0; i1 < sl1; i1++)
            array[i1] = source[i0, i1]!;

          dt.Rows.Add(array);
        }

        return dt;
      }

      /// <summary>
      /// <para>Create a new jagged array with all elements in source in a <paramref name="dimension"/>-major-order, i.e. by row or by column first (then the other).</para>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public T[][] Rank2ToJaggedArray(int dimension)
      {
        source.AssertRank(2);

        var target = new T[source.GetLength(dimension)][];

        var sourceLength0 = source.GetLength(0);
        var sourceLength1 = source.GetLength(1);

        switch (dimension)
        {
          case 0:
            for (int s0 = 0; s0 < sourceLength0; s0++)
            {
              var jaggedDimension = new T[sourceLength1];
              for (var s1 = 0; s1 < sourceLength1; s1++)
                jaggedDimension[s1] = source[s0, s1];
              target[s0] = jaggedDimension;
            }
            break;
          case 1:
            for (var s1 = 0; s1 < sourceLength1; s1++)
            {
              var jaggedDimension = new T[sourceLength0];
              for (var s0 = 0; s0 < sourceLength0; s0++)
                jaggedDimension[s0] = source[s0, s1];
              target[s1] = jaggedDimension;
            }
            break;
          default:
            throw new System.ArgumentOutOfRangeException(nameof(dimension));
        }

        return target;
      }

      /// <summary>
      /// <para>Create a new jagged array with all elements in source in a <paramref name="dimension"/>-major-order, i.e. by row or by column first (then the other).</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public T[][] Rank2ToJaggedArray(ArrayDimensionLabel dimension) => Rank2ToJaggedArray(source, (int)dimension);

      /// <summary>
      /// <para>Transpose all elements in <paramref name="source"/> into target.</para>
      /// <para>If <paramref name="target"/> is different than <paramref name="source"/>, a copy to <paramref name="target"/> is performed. If <paramref name="target"/> is the same as <paramref name="source"/>, or <see langword="null"/>, a <paramref name="source"/> in-place is performed.</para>
      /// <see href="https://en.wikipedia.org/wiki/Transpose"/>
      /// </summary>
      /// <remarks>Since an two-dimensional array is arbitrary in terms of its dimensions (e.g. rows and columns) and .NET is row-major order, the concept of [row, column] is adopted, i.e. dimension-0 = row, and dimension-1 = column.</remarks>
      public void Transpose(T[,]? target = null)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        target ??= source;

        var sl0 = source.GetLength(0);
        var sl1 = source.GetLength(1);

        if (target.GetLength(0) < sl1 || target.GetLength(1) < sl0)
          throw new System.ArgumentException($"Array transposition requires target minimum dimensional symmetry.");

        for (var i = 0; i < sl0; i++)
          for (var j = i; j < sl1; j++)
          {
            var tmp = source[i, j];
            target[i, j] = source[j, i];
            target[j, i] = tmp;
          }
      }

      /// <summary>
      /// <para>Writes a two-dimensional array as URGF (Unicode tabular data) to the <paramref name="target"/>.</para>
      /// </summary>
      /// <param name="target"></param>
      /// <param name="source"></param>
      public void WriteRank2ToUrgf(System.IO.TextWriter target)
      {
        var length0 = source.GetLength(0);
        var length1 = source.GetLength(1);

        for (var r = 0; r < length0; r++)
        {
          if (r > 0) target.Write((char)UnicodeInformationSeparator.RecordSeparator);

          for (var c = 0; c < length1; c++)
          {
            if (c > 0) target.Write((char)UnicodeInformationSeparator.UnitSeparator);

            target.Write(source[r, c]);
          }
        }
      }
    }

    extension<T>(T[,] source)
      where T : System.IEquatable<T>
    {
      /// <summary>
      /// <para>Asserts the adjacency matrix graph property: two dimensions of symmetrical length.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="length"></param>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentException"></exception>
      public void GraphAssertProperty(out int length)
      {
        if (!source.TryHasDimensionalSymmetry(out length) || !source.IsRank(2))
        {
          System.ArgumentNullException.ThrowIfNull(source);

          throw new System.ArgumentException($"An asymmetrical array does not represent an adjacency matrix.");
        }
      }


      /// <summary>Returns the maximum flow/minimum cost using the Bellman-Ford algorithm.</summary>
      /// <param name="y"></param>
      /// <param name="x"></param>
      /// <param name="capacitySelector"></param>
      /// <param name="costSelector"></param>
      public (double totalFlow, double totalCost) GraphBellmanFordMaxFlowMinCost(int x, int y, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector)
      {
        GraphAssertProperty(source, out var length);

        System.ArgumentNullException.ThrowIfNull(capacitySelector);
        System.ArgumentNullException.ThrowIfNull(costSelector);

        var vertexCount = length;

        var found = new bool[vertexCount];
        var flow = new double[vertexCount, vertexCount];
        var distance = new double[vertexCount + 1];
        var dad = new int[vertexCount];
        var pi = new double[vertexCount];

        return GraphBellmanFordGetMaxFlow(source, x, y, capacitySelector, costSelector, vertexCount, found, flow, distance, dad, pi);

        /*
    function BellmanFord(list vertices, list edges, vertex source) is

        // This implementation takes in a graph, represented as
        // lists of vertices (represented as integers [0..n-1]) and edges,
        // and fills two arrays (distance and predecessor) holding
        // the shortest path from the source to each vertex

        distance := list of size n
        predecessor := list of size n

        // Step 1: initialize graph
        for each vertex v in vertices do
            // Initialize the distance to all vertices to infinity
            distance[v] := inf
            // And having a null predecessor
            predecessor[v] := null

        // The distance from the source to itself is zero
        distance[source] := 0

        // Step 2: relax edges repeatedly
        repeat |V|-1 times:
            for each edge (u, v) with weight w in edges do
                if distance[u] + w < distance[v] then
                    distance[v] := distance[u] + w
                    predecessor[v] := u

        // Step 3: check for negative-weight cycles
        for each edge (u, v) with weight w in edges do
            if distance[u] + w < distance[v] then
                predecessor[v] := u
                // A negative cycle exists; find a vertex on the cycle 
                visited := list of size n initialized with false
                visited[v] := true
                while not visited[u] do
                    visited[u] := true
                    u := predecessor[u]
                // u is a vertex in a negative cycle, find the cycle itself
                ncycle := [u]
                v := predecessor[u]
                while v != u do
                    ncycle := concatenate([v], ncycle)
                    v := predecessor[v]
                error "Graph contains a negative-weight cycle", ncycle
        return distance, predecessor     
         */
      }

      // BellmanFord helper. Determine if it is possible to have a flow from the source to target.
      private bool GraphBellmanFordSearch(int y, int x, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector, int vertexCount, bool[] found, double[,] flow, double[] distance, int[] dad, double[] pi)
      {
        System.Array.Fill(found!, false); // Initialise found[] to false.

        System.Array.Fill(distance!, double.PositiveInfinity); // Initialise the dist[] to INF.

        distance![y] = 0; // Distance from the source node.

        while (y != vertexCount) // Iterate until source reaches the number of vertices.
        {
          var best = vertexCount;

          found![y] = true;

          for (var i = 0; i < vertexCount; i++)
          {
            if (found![i]) // If already found, continue.
              continue;

            if (flow![i, y] != 0) // Evaluate while flow is still in supply.
            {
              var minValue = distance[y] + pi![y] - pi[i] - costSelector(GraphGetState(source, i, y)); // Obtain the total value.
              if (minValue < distance[i])// If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = y;
              }
            }

            if (flow[y, i] < capacitySelector(GraphGetState(source, y, i)))
            {
              var minValue = distance[y] + pi![y] - pi[i] + costSelector(GraphGetState(source, y, i));
              if (minValue < distance[i]) // If dist[k] is > minimum value, update.
              {
                distance[i] = minValue;
                dad![i] = y;
              }
            }

            if (distance[i] < distance[best])
              best = i;
          }

          y = best; // Update src to best for next iteration.
        }

        for (var i = 0; i < vertexCount; i++)
          pi![i] = double.Min(pi[i] + distance[i], double.PositiveInfinity);

        return found![x]; // Return the value obtained at target.
      }

      // BellmanFord helper. Obtain the maximum Flow.
      private (double totalFlow, double totalCost) GraphBellmanFordGetMaxFlow(int y, int x, System.Func<object, double> capacitySelector, System.Func<object, double> costSelector, int vertexCount, bool[] found, double[,] flow, double[] distance, int[] dad, double[] pi)
      {
        var totalFlow = 0d;
        var totalCost = 0d;

        while (GraphBellmanFordSearch(source, y, x, capacitySelector, costSelector, vertexCount, found, flow, distance, dad, pi)) // If a path exist from source to target.
        {
          var amt = double.PositiveInfinity; // Set the default amount.

          for (var i = x; i != y; i = dad[i])
            amt = double.Min(amt, flow[i, dad[i]] != 0 ? flow[i, dad[i]] : capacitySelector(GraphGetState(source, dad[i], i)) - flow[dad[i], i]);

          for (var i = x; i != y; i = dad[i])
          {
            if (flow[i, dad[i]] != 0)
            {
              flow[i, dad[i]] -= amt;
              totalCost -= amt * costSelector(GraphGetState(source, i, dad[i]));
            }
            else
            {
              flow[dad[i], i] += amt;
              totalCost += amt * costSelector(GraphGetState(source, dad[i], i));
            }
          }

          totalFlow += amt;
        }

        return (totalFlow, totalCost); // Return pair total flow and cost.
      }

      /// <summary>Creates a new sequence with the shortest path tree, i.e. the shortest paths from the specified origin vertex to all reachable vertices.</summary>
      /// <param name="distanceSelector">Selects the length of the edge (i.e. the distance between the endpoints).</param>
      public System.Collections.Generic.IEnumerable<(int destination, double distance)> GraphDijkstraShortestPathTree(int origin, System.Func<object, double> distanceSelector, System.Func<int, int, T, bool>? isEdgePredicate = null)
      {
        GraphAssertProperty(source, out var length);

        System.ArgumentNullException.ThrowIfNull(distanceSelector);

        isEdgePredicate ??= (r, c, v) => !v.Equals(default);

        var vertices = GraphGetVertices(source).ToList();

        var distances = vertices.ToDictionary(v => v, v => v.Equals(origin) ? 0 : double.PositiveInfinity);

        var edges = GraphGetEdges(source, isEdgePredicate).ToList(); // Cache edges, because we need it while there are available distances.

        while (distances.Count != 0) // As long as there are nodes available.
        {
          var shortest = distances.OrderBy(v => v.Value).First(); // Get the node with the shortest distance.

          if (shortest.Value < double.PositiveInfinity) // If the distance to the node is less than infinity, it was reachable so it should be returned.
            yield return (shortest.Key, shortest.Value);

          distances.Remove(shortest.Key); // This node is now final, so remove it.

          foreach (var (x, y, v) in edges.Where(e => e.value.Equals(shortest.Key))) // Updates all nodes reachable from the vertex.
          {
            if (distances.TryGetValue(y, out var distanceToEdgeTarget))
            {
              var distanceViaShortest = shortest.Value + distanceSelector(v); // Distance via the current node.

              if (distanceViaShortest < distanceToEdgeTarget) // If the distance via the current node is shorter than the currently recorded distance, replace it.
                distances[y] = distanceViaShortest;
            }
          }
        }
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="isEdgePredicate"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<(int keySource, int keyTarget, T value)> GraphGetEdges(System.Func<int, int, T, bool> isEdgePredicate)
      {
        GraphAssertProperty(source, out var length);

        for (var r = 0; r < length; r++)
          for (var c = 0; c < length; c++)
            if (source[r, c] is var m && isEdgePredicate(r, c, m))
              yield return (r, c, m);
      }

      ///// <summary>If the adjacency matrix value yields the default(T) value, state = 0, otherwise, if the from/to indices are the same. state = 2. and if they are different, state = 1. This "Impl" version bypass argument checks.</summary>
      //private int GraphGetStateImpl(int keyFrom, int keyTo)
      //  => source[keyFrom, keyTo] is var m && m.Equals(default!) ? 0 : keyFrom == keyTo ? 2 : 1;

      /// <summary>If the adjacency matrix value yields the default(T) value, state = 0, otherwise, if the from/to indices are the same. state = 2. and if they are different, state = 1. This version performs all argument checks.</summary>
      public int GraphGetState(int keySource, int keyTarget)
      {
        GraphAssertProperty(source, out var length);

        if (keySource < 0 || keySource >= length) throw new System.ArgumentOutOfRangeException(nameof(keySource));
        if (keyTarget < 0 || keyTarget >= length) throw new System.ArgumentOutOfRangeException(nameof(keyTarget));

        return source[keySource, keyTarget] is var m && m.Equals(default!) ? 0 : keySource == keyTarget ? 2 : 1;

        //return GraphGetStateImpl(source, keySource, keyTarget);
      }

      public int[] GraphGetVertices()
      {
        GraphAssertProperty(source, out var length);

        return System.Linq.Enumerable.Range(0, length).ToArray();
      }
    }
  }
}

#region Create sample file
/*

using var sw = System.IO.File.CreateText(fileName);

var data = new string[][] { new string[] { "A", "B" }, new string[] { "1", "2" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Write((char)UnicodeInformationSeparator.FileSeparator);

data = new string[][] { new string[] { "C", "D" }, new string[] { "3", "4" } };

Flux.Fx.WriteUrgf(sw, data);

sw.Flush();
sw.Close();

*/
#endregion
