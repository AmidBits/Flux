namespace Flux
{
  public static partial class XtensionArrayRank1
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

    extension<T>(T[] source)
    {
      /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan() => source;

      /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan(int start) => new(source, start, source.Length - start);

      /// <summary>Non-allocating conversion (casting) from <paramref name="source"/> T[] to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan(int start, int length) => new(source, start, length);

      /// <summary>
      /// <para>Fill <paramref name="length"/> elements in <paramref name="source"/> from <paramref name="pattern"/> (repeatingly if necessary) at <paramref name="index"/>.</para>
      /// </summary>
      public T[] FillWith(int index, int length, params System.ReadOnlySpan<T> pattern)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var copyLength = int.Min(pattern.Length, length);

        pattern.Slice(0, copyLength).CopyTo(source.AsSpan(index, copyLength));

        while ((copyLength << 1) < length)
        {
          System.Array.Copy(source, index, source, index + copyLength, copyLength);

          copyLength <<= 1;
        }

        System.Array.Copy(source, index, source, index + copyLength, length - copyLength);

        return source;
      }

      /// <summary>
      /// <para>Create a new array with all elements from <paramref name="source"/> and <paramref name="count"/> elements inserted at <paramref name="index"/>.</para>
      /// </summary>
      private T[] InsertToCopy(int index, int count)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

        var targetLength = source.Length + count; // Pre-compute for multi-use.

        var target = new T[targetLength];

        if (index > 0) // Copy left-side, if needed.
          System.Array.Copy(source, 0, target, 0, index);

        var endIndex = index + count;

        if (endIndex < targetLength) // Copy right-side, if needed.
          System.Array.Copy(source, index, target, index + count, targetLength - endIndex);

        return target;
      }

      /// <summary>
      /// <para>Create a new array with all elements from <paramref name="source"/> and <paramref name="count"/> instances of <paramref name="value"/> inserted at <paramref name="index"/>.</para>
      /// </summary>
      public T[] InsertToCopy(int index, int count, T value)
      {
        var target = source.InsertToCopy(index, count);
        System.Array.Fill(target, value, index, count);
        return target;
      }

      /// <summary>
      /// <para>Create a new array with all elements from <paramref name="source"/> and the elements of <paramref name="values"/> inserted at <paramref name="index"/>.</para>
      /// </summary>
      public T[] InsertToCopy(int index, params T[] values)
      {
        System.ArgumentNullException.ThrowIfNull(values);

        var target = source.InsertToCopy(index, values.Length);
        System.Array.Copy(values, 0, target, index, values.Length);
        return target;
      }

      /// <summary>
      /// <para>Returns the one-dimensional array as a string-builder, that can be printed in the console.</para>
      /// </summary>
      /// <remarks>Since an array is arbitrary in terms of e.g. rows and columns, we just adopt a this view, so we'll consider dimension 0 as the row dimension and dimension 1 as the column dimension.</remarks>
      public string Rank1ToConsoleString(ConsoleFormatOptions? options = null)
        => new T[][] { source }.JaggedToConsoleString(options);

      /// <summary>
      /// <para>Create a new array with <paramref name="count"/> elements removed from the <paramref name="source"/> starting at <paramref name="index"/>.</para>
      /// </summary>
      public T[] RemoveCopy(int index, int count)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var sourceLength = source.Length;

        if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));

        var endIndex = index + count;

        if (count < 0 || endIndex > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(count));

        var target = new T[sourceLength - count];

        if (index > 0) // Copy left-side, if needed.
          System.Array.Copy(source, 0, target, 0, index);

        if (endIndex < sourceLength) // Copy right-side, if needed.
          System.Array.Copy(source, endIndex, target, index, sourceLength - endIndex);

        return target;
      }

      /// <summary>
      /// <para>Creates a new array with <paramref name="count"/> elements from <paramref name="source"/> starting at <paramref name="index"/>. Use <paramref name="preCount"/> and <paramref name="postCount"/> arguments to add surrounding element space in the new array.</para>
      /// </summary>
      /// <param name="source">W</param>
      /// <param name="index"></param>
      /// <param name="count"></param>
      /// <param name="preCount"></param>
      /// <param name="postCount"></param>
      /// <returns></returns>
      public T[] ToCopy(int index, int count, int preCount = 0, int postCount = 0)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var target = new T[preCount + count + postCount];
        System.Array.Copy(source, index, target, preCount, count);
        return target;
      }

      /// <summary>
      /// <para>Creates a new jagged array with, either <paramref name="source"/> as the one sub-array (horizontal), or pivoted where each element in <paramref name="source"/> becomes one-element sub-arrays (vertical).</para>
      /// </summary>
      public T[][] ToJaggedArray(bool pivot = false)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var array = new T[pivot ? source.Length : 1][];

        if (pivot)
          for (var i = 0; i < source.Length; i++)
            array[i] = [source[i]];
        else
          array[0] = source;

        return array;
      }
    }

    /// <summary>
    /// <para>Modifies <paramref name="source"/> by inserting <paramref name="count"/> of <typeparamref name="T"/> at <paramref name="index"/>.</para>
    /// </summary>
    private static void InsertInPlace<T>(ref T[] source, int index, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (index < 0 || index > source.Length) throw new System.ArgumentOutOfRangeException(nameof(index));

      var sourceLength = source.Length;

      System.Array.Resize(ref source, sourceLength + count);

      if (sourceLength - index is var moveRight && moveRight > 0) // Move right-side, if needed.
        System.Array.Copy(source, index, source, index + count, moveRight);
    }

    /// <summary>
    /// <para>Modifies <paramref name="source"/> by inserting <paramref name="count"/> of <paramref name="value"/> at <paramref name="index"/>.</para>
    /// </summary>
    public static void InsertInPlace<T>(ref T[] source, int index, int count, T value)
    {
      InsertInPlace(ref source, index, count);

      System.Array.Fill(source, value, index, count);
    }

    /// <summary>
    /// <para>Modifies <paramref name="source"/> by inserting the elements of <paramref name="values"/> at <paramref name="index"/>.</para>
    /// </summary>
    public static void InsertInPlace<T>(ref T[] source, int index, params T[] values)
    {
      InsertInPlace(ref source, index, values.Length);

      System.Array.Copy(values, 0, source, index, values.Length);
    }

    /// <summary>
    /// <para>Modify <paramref name="source"/> by removing <paramref name="count"/> elements starting at <paramref name="index"/>.</para>
    /// </summary>
    public static void RemoveInPlace<T>(ref T[] source, int index, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var sourceLength = source.Length;

      if (index < 0 || index >= sourceLength) throw new System.ArgumentOutOfRangeException(nameof(source));

      var endIndex = index + count;

      if (count < 0 || endIndex > sourceLength) throw new System.ArgumentOutOfRangeException(nameof(count));

      if (endIndex < sourceLength) // Copy right-side, if needed.
        System.Array.Copy(source, endIndex, source, index, sourceLength - endIndex);

      System.Array.Resize(ref source, sourceLength - count);
    }
  }
}
