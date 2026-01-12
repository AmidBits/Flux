namespace Flux
{
  public enum HeapSortType
  {
    BasicDown,
    FloydDown
  }

  public enum MergeSortType
  {
    BottomUp,
    TopDown
  }

  public static partial class SpanExtensions
  {
    extension<T>(System.Span<T> source)
    {
      #region AsReadOnlySpan

      /// <summary>
      /// <para>Creates a new non-allocating <see cref="System.ReadOnlySpan{T}"/> over a <see cref="System.Span{T}"/>.</para>
      /// </summary>
      /// <returns></returns>
      public System.ReadOnlySpan<T> AsReadOnlySpan()
        => source;

      #endregion

      #region QuickSelect

      /// <summary>Find the Kth smallest element in an unordered list, between left and right index.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Quickselect"/>
      public T QuickSelect(int leftIndex, int rightIndex, int kth, System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        if (leftIndex == rightIndex)
          return source[leftIndex];

        while (true)
        {
          var pivotIndex = QuickSelectPartition(source, kth, leftIndex, rightIndex, comparer);

          if (kth < pivotIndex) rightIndex = pivotIndex - 1;
          else if (kth > pivotIndex) leftIndex = pivotIndex + 1;
          else return source[kth];
        }

      }

      #endregion

      #region Replace..

      /// <summary>Replace all values in <paramref name="source"/> using the specified <paramref name="replacementSelector"/>.</summary>
      public System.Span<T> Replace(System.Func<T, int, bool> predicate, System.Func<T, int, T> replacementSelector)
      {
        System.ArgumentNullException.ThrowIfNull(predicate);
        System.ArgumentNullException.ThrowIfNull(replacementSelector);

        for (var index = source.Length - 1; index >= 0; index--)
          if (source[index] is var item && predicate(item, index))
            source[index] = replacementSelector(item, index);

        return source;
      }

      public System.Span<T> Replace(System.Func<T, bool> predicate, System.Func<T, T> replacementSelector)
        => Replace(source, (e, i) => predicate(e), (e, i) => replacementSelector(e));

      public System.Span<T> ReplaceAny(System.Collections.Generic.IEnumerable<T> any, System.Func<T, T> replacementSelector, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return Replace(source, (e, i) => any.Contains(e, equalityComparer), (e, i) => replacementSelector(e));
      }

      public System.Span<T> ReplaceAny(System.Collections.Generic.IEnumerable<T> any, T replacement, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => Replace(source, (e, i) => any.Contains(e, equalityComparer), (e, i) => replacement);

      #endregion

      #region Rotate

      /// <summary>
      /// <para>Rotate all items in a <see cref="System.Span{T}"/> one step to the left (down or in the direction of least-significant position).</para>
      /// </summary>
      public void RotateLeft1()
      {
        var tmp = source[0];
        source[1..].CopyTo(source[..^1]);
        source[^1] = tmp;
      }

      /// <summary>
      /// <para>Rotate all items in a <see cref="System.Span{T}"/> one step to the right (up or in the direction of most-significant position).</para>
      /// </summary>
      public void RotateRight1()
      {
        var tmp = source[^1];
        source[0..^1].CopyTo(source[1..]);
        source[0] = tmp;
      }

      /// <summary>
      /// <para>Rotates all items in a <see cref="System.Span{T}"/> a specified <paramref name="count"/> number of steps.</para>
      /// <para>When count is positive, rotate right (up or in the direction of most-significant position).</para>
      /// <para>When count is negative, rotate left (down or in the direction of least-significant position).</para>
      /// </summary>
      /// <param name="count"></param>
      public void Rotate(int count)
      {
        count %= source.Length;

        if (count < 0)
          count = source.Length + count;

        if (count > 0)
        {
          source.Reverse();
          source[..count].Reverse();
          source[count..].Reverse();
        }
      }

      #endregion

      #region ..Sort

      /// <summary>
      /// <para>Sorts the content of the sequence using bingo sort which is a variant of selection sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bingo_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void BingoSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        var max = source.Length - 1;

        var nextValue = source[max];

        for (var i = max - 1; i >= 0; i--)
          if (comparer.Compare(source[i], nextValue) > 0)
            nextValue = source[i];

        while (max > 0 && comparer.Compare(source[max], nextValue) == 0)
          max--;

        while (max > 0)
        {
          var value = nextValue;

          nextValue = source[max];

          for (var i = max - 1; i >= 0; i--)
          {
            if (comparer.Compare(source[i], value) == 0)
            {
              source.Swap(i, max);

              max--;
            }
            else if (comparer.Compare(source[i], nextValue) > 0)
              nextValue = source[i];
          }

          while (max > 0 && comparer.Compare(source[max], nextValue) == 0)
            max--;
        }
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using an optimized version.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bubble_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void BubbleSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        var length = source.Length;

        do
        {
          var newLength = 0;

          for (var i = 1; i < length; i++)
          {
            if (comparer.Compare(source[i - 1], source[i]) > 0)
            {
              source.Swap(i - 1, i);

              newLength = i;
            }
          }

          length = newLength;
        }
        while (length > 1);
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using essentially an improved bubble sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Comb_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void CombSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        var gap = source.Length;
        var shrinkFactor = 1.3;
        var isSorted = false;

        while (isSorted == false)
        {
          gap = (int)double.Floor(gap / shrinkFactor);

          if (gap <= 1)
          {
            gap = 1;

            isSorted = true; // No swaps this pass, we are done.
          }

          // A single "comb" over the input list
          for (var i = 0; i + gap < source.Length; i++) // See Shell sort for a similar idea
          {
            if (comparer.Compare(source[i], source[i + gap]) > 0)
            {
              source.Swap(i, i + gap);

              isSorted = false; // If this assignment never happens within the loop, then there have been no swaps and the list is sorted.
            }
          }
        }
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using a heap sort, which is more or less an improved selection sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Heap_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="type"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void HeapSort(HeapSortType type, System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        switch (type)
        {
          case HeapSortType.BasicDown:
            HeapSort_BasicHeapSort(source, source.Length, comparer);
            break;
          case HeapSortType.FloydDown:
            HeapSort_FloydHeapSort(source, source.Length, comparer);
            break;
        }
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using insertion sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Insertion_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void InsertionSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        for (var i = 1; i < source.Length; i++)
        {
          var tmp = source[i];

          var j = i - 1;

          while (j >= 0 && comparer.Compare(source[j], tmp) > 0)
          {
            source[j + 1] = source[j];
            j--;
          }

          source[j + 1] = tmp;
        }
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using merge sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Merge_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void MergeSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        MergeSort_Sort(source, 0, source.Length - 1, comparer);

      }

      /// <summary>
      /// <para>Sorts the content of the sequence using merge sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Merge_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="type"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      /// <returns></returns>
      /// <exception cref="System.Exception"></exception>
      public void MergeSortToCopy(System.Span<T> target, MergeSortType type, System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        switch (type)
        {
          case MergeSortType.BottomUp:
            MergeSortToCopy_BottomUpMergeSort(source, target, source.Length, comparer);
            break;
          case MergeSortType.TopDown:
            MergeSortToCopy_TopDownMergeSort(source, target, source.Length, comparer);
            break;
          default:
            throw new System.Exception(nameof(type));
        }
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using quick sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Quick_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void QuickSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        QuickSort_Impl(source, 0, source.Length - 1, comparer);
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using selection sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Selection_sort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      public void SelectionSort(System.Collections.Generic.IComparer<T>? comparer = null)
      {
        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        for (var i = 0; i < source.Length - 1; i++)
        {
          var min = i;
          for (var j = i + 1; j < source.Length; j++)
            if (comparer.Compare(source[j], source[min]) < 0)
              min = j;

          var x = source[min];
          for (var j = min; j > i; j--)
            source[j] = source[j - 1];
          source[i] = x;
        }
      }

      /// <summary>
      /// <para>Sorts the content of the sequence using Marcin Ciura's gap sequence, with an inner insertion sort.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Shellsort"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="comparer">Set to <see cref="System.Collections.Generic.Comparer{T}.Default"/> if null.</param>
      /// <param name="gapSequence">Set to <see cref="ShellSort_MarcinCiuraGapSequence"/> if null.</param>
      public void ShellSort(System.Collections.Generic.IComparer<T>? comparer = null, int[]? gapSequence = null)
      {
        gapSequence ??= ShellSort_MarcinCiuraGapSequence;

        comparer ??= System.Collections.Generic.Comparer<T>.Default;

        var sourceLength = source.Length;

        foreach (var gap in gapSequence)
        {
          for (var i = gap; i < sourceLength; i++)
          {
            var j = i;

            var tmp = source[j];

            while (j >= gap && comparer.Compare(source[j - gap], tmp) > 0)
            {
              source[j] = source[j - gap];

              j -= gap;
            }

            source[j] = tmp;
          }
        }
      }

      #endregion

      #region Swap

      /// <summary>
      /// <para>Performs an in-place swap of two elements at a specified pair of indices.</para>
      /// </summary>
      /// <param name="indexA"></param>
      /// <param name="indexB"></param>
      /// <returns>Whether a swap actually took place.</returns>
      public bool Swap(int indexA, int indexB)
      {
        if ((indexA != indexB) is var isUnequal && isUnequal) // No need to actually swap if the indices are the same.
          (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

        return isUnequal;
      }

      /// <summary>
      /// <para>Perform a swap of two elements at a specified index and the first element.</para>
      /// </summary>
      public bool SwapWithFirst(int index)
        => Swap(source, 0, index);

      /// <summary>
      /// <para>Perform a swap of two elements at a specified index and the last element.</para>
      /// </summary>
      public bool SwapWithLast(int index)
        => Swap(source, index, source.Length - 1);

      #endregion
    }

    extension(System.Span<char> source)
    {
      #region Un/CapitalizeWords

      /// <summary>
      /// <para>Capitalize any lower-case char in a <see cref="System.Span{T}"/>, at the beginning, or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="culture">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> CapitalizeWords(int index, int length, System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = index; i <= maxIndex; i++)
        {
          var c = source[i]; // Avoid multiple indexers.

          if (
            (!char.IsLower(c)) // If, c is not lower-case, advance.
            || (i > 0 && !char.IsWhiteSpace(source[i - 1])) // If, (ensure left char exists) left is not white-space, advance.
            || (i < maxIndex && !char.IsLower(source[i + 1])) // If, (ensure right char exists) right is not lower-case, advance.
          )
            continue;

          source[i] = char.ToUpper(c, culture);
        }

        return source;
      }

      /// <summary>
      /// <para>Capitalize any lower-case char in a <see cref="System.Span{T}"/>, at the beginning, or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="culture">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> CapitalizeWords(System.Range range, System.Globalization.CultureInfo? culture = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return CapitalizeWords(source, offset, length, culture);
      }

      /// <summary>
      /// <para>Capitalize any lower-case char in a <see cref="System.Span{T}"/>, at the beginning, or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="culture">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> CapitalizeWords(System.Globalization.CultureInfo? culture = null)
        => CapitalizeWords(source, 0, source.Length, culture);

      /// <summary>
      /// <para>Uncapitalize any upper-case char in a <see cref="System.Span{T}"/>, at the beginning, or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="culture">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> UncapitalizeWords(int index, int length, System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = index; i <= maxIndex; i++)
        {
          var c = source[i]; // Avoid multiple indexers.

          if (
            (!char.IsUpper(c)) // If, c is not upper-case, advance.
            || (i > 0 && !char.IsWhiteSpace(source[i - 1])) // If, (ensure left char exists) left is not white-space, advance.
            || (i < maxIndex && !char.IsLower(source[i + 1])) // If, (ensure right char exists) right is not lower-case, advance.
          )
            continue;

          source[i] = char.ToLower(c, culture);
        }

        return source;
      }

      /// <summary>
      /// <para>Uncapitalize any upper-case char in a <see cref="System.Span{T}"/>, at the beginning, or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="culture">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> UncapitalizeWords(System.Range range, System.Globalization.CultureInfo? culture = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return UncapitalizeWords(source, offset, length, culture);
      }

      /// <summary>
      /// <para>Uncapitalize any upper-case char in a <see cref="System.Span{T}"/>, at the beginning, or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="culture">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> UncapitalizeWords(System.Globalization.CultureInfo? culture = null)
        => UncapitalizeWords(source, 0, source.Length, culture);

      #endregion

      #region Regex

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="pattern"></param>
      /// <returns></returns>
      public System.Span<char> RemoveRegex(string pattern)
      {
        var removed = 0;

        var ranges = source.AsReadOnlySpan().GetRegexMatches(pattern);

        for (var i = ranges.Count - 1; i >= 0; i--)
        {
          var (startIndex, length) = ranges[i].GetOffsetAndLength(source.Length);

          var endIndex = startIndex + length;

          var move = source.Slice(endIndex, source.Length - endIndex - removed);

          move.CopyTo(source.Slice(startIndex, move.Length));

          removed += length;
        }

        return source[..^removed];
      }

      /// <summary>
      /// <para></para>
      /// </summary>
      /// <param name="pattern"></param>
      /// <param name="replacement"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentException"></exception>
      public System.Span<char> ReplaceRegex(string pattern, string replacement)
      {
        var removed = 0;

        var ranges = source.AsReadOnlySpan().GetRegexMatches(pattern);

        for (var i = ranges.Count - 1; i >= 0; i--)
        {
          var (startIndex, length) = ranges[i].GetOffsetAndLength(source.Length);

          var endIndex = startIndex + length;

          if (replacement.Length > length) throw new System.ArgumentException("The replacement length cannot be longer than what it replaces.");

          var move = source.Slice(endIndex, source.Length - endIndex - removed);

          if (move.Length > 0)
          {
            move.CopyTo(source.Slice(startIndex + replacement.Length, move.Length));

            replacement.CopyTo(source.Slice(startIndex, replacement.Length));

            removed += length - replacement.Length;
          }
        }

        return source[..^removed];
      }

      #endregion

      #region ToLower/Upper

      /// <summary>
      /// <para>Convert characters in a <see cref="System.Span{T}"/> to lower-case.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> ToLower(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var i = index; i <= endIndex; i++)
          if (source[i] is var sourceChar && char.IsUpper(sourceChar) && char.ToLower(sourceChar, cultureInfo) is var targetChar && sourceChar != targetChar)
            source[i] = targetChar;

        return source;
      }

      /// <summary>
      /// <para>Convert characters in a <see cref="System.Span{T}"/> to lower-case.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> ToLower(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return ToLower(source, offset, length, cultureInfo);
      }

      /// <summary>
      /// <para>Convert characters in a <see cref="System.Span{T}"/> to lower-case.</para>
      /// </summary>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> ToLower(System.Globalization.CultureInfo? cultureInfo = null)
        => ToLower(source, 0, source.Length, cultureInfo);

      /// <summary>
      /// <para>Converts characters in a <see cref="System.Span{T}"/> to upper-case.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> ToUpper(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var i = index; i <= endIndex; i++)
          if (source[i] is var sourceChar && char.IsLower(sourceChar) && char.ToUpper(sourceChar, cultureInfo) is var targetChar && sourceChar != targetChar)
            source[i] = targetChar;

        return source;
      }

      /// <summary>
      /// <para>Converts characters in a <see cref="System.Span{T}"/> to upper-case.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<char> ToUpper(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return ToUpper(source, offset, length, cultureInfo);
      }

      /// <summary>
      /// <para>Converts characters in a <see cref="System.Span{T}"/> to upper-case.</para>
      /// </summary>
      /// <param name="cultureInfo"></param>
      /// <returns></returns>
      public System.Span<char> ToUpper(System.Globalization.CultureInfo? cultureInfo = null)
        => ToUpper(source, 0, source.Length, cultureInfo);

      #endregion
    }

    extension(System.Span<System.Text.Rune> source)
    {
      #region Un/CapitalizeWords

      /// <summary>
      /// <para>Capitalize runes in a <see cref="System.Span{T}"/>, at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> CapitalizeWords(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = index; i <= endIndex; i++)
        {
          var r = source[i]; // Avoid multiple indexers.

          if (
            (!System.Text.Rune.IsLower(r)) // If, r is not lower-case, advance.
            || (i > 0 && !System.Text.Rune.IsWhiteSpace(source[i - 1])) // If, (ensure left char exists) left is not white-space, advance.
            || (i < endIndex && !System.Text.Rune.IsLower(source[i + 1])) // If, (ensure right char exists) right is not lower-case, advance.
          )
            continue;

          source[i] = System.Text.Rune.ToUpper(r, cultureInfo);
        }

        return source;
      }

      /// <summary>
      /// <para>Capitalize runes in a <see cref="System.Span{T}"/>, at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> CapitalizeWords(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return CapitalizeWords(source, offset, length, cultureInfo);
      }

      /// <summary>
      /// <para>Capitalize runes in a <see cref="System.Span{T}"/>, at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> Capitalize(System.Globalization.CultureInfo? cultureInfo = null)
        => CapitalizeWords(source, 0, source.Length, cultureInfo);

      /// <summary>
      /// <para>Uncapitalize runes in a <see cref="System.Span{T}"/>, at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> UncapitalizeWords(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = 0; i <= endIndex; i++)
        {
          var r = source[i]; // Avoid multiple indexers.

          if (
            (!System.Text.Rune.IsUpper(r)) // If, r is not upper-case, advance.
            || (i > 0 && !System.Text.Rune.IsWhiteSpace(source[i - 1])) // If, (ensure left char exists) left is not white-space, advance.
            || (i < endIndex && !System.Text.Rune.IsLower(source[i + 1])) // If, (ensure right char exists) right is not lower-case, advance.
          )
            continue;

          source[i] = System.Text.Rune.ToLower(r, cultureInfo);
        }

        return source;
      }

      /// <summary>
      /// <para>Uncapitalize runes in a <see cref="System.Span{T}"/>, at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> UncapitalizeWords(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return UncapitalizeWords(source, offset, length, cultureInfo);
      }

      /// <summary>
      /// <para>Uncapitalize runes in a <see cref="System.Span{T}"/>, at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> UncapitalizeWords(System.Globalization.CultureInfo? cultureInfo = null)
        => UncapitalizeWords(source, 0, source.Length, cultureInfo);

      #endregion

      #region ToLower/Upper

      /// <summary>
      /// <para>Convert runes in a <see cref="System.Span{T}"/> to lower-case.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToLower(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = 0; i <= endIndex; i++)
          if (source[i] is var sourceRune && System.Text.Rune.IsUpper(sourceRune) && System.Text.Rune.ToLower(sourceRune, cultureInfo) is var targetRune && sourceRune != targetRune)
            source[i] = targetRune;

        return source;
      }

      /// <summary>
      /// <para>Convert runes in a <see cref="System.Span{T}"/> to lower-case.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToLower(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return ToLower(source, offset, length, cultureInfo);
      }

      /// <summary>
      /// <para>Convert runes in a <see cref="System.Span{T}"/> to lower-case.</para>
      /// </summary>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToLower(System.Globalization.CultureInfo? cultureInfo = null)
        => ToLower(source, 0, source.Length, cultureInfo);

      /// <summary>
      /// <para>Convert runes in a <see cref="System.Span{T}"/> to upper-case.</para>
      /// </summary>
      /// <param name="index"></param>
      /// <param name="length"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToUpper(int index, int length, System.Globalization.CultureInfo? cultureInfo = null)
      {
        cultureInfo ??= System.Globalization.CultureInfo.CurrentCulture;

        var endIndex = System.Range.AssertInRange(index, length, source.Length);

        for (var i = 0; i <= endIndex; i++)
          if (source[i] is var sourceRune && System.Text.Rune.IsLower(sourceRune) && System.Text.Rune.ToUpper(sourceRune, cultureInfo) is var targetRune && sourceRune != targetRune)
            source[i] = targetRune;

        return source;
      }

      /// <summary>
      /// <para>Convert runes in a <see cref="System.Span{T}"/> to upper-case.</para>
      /// </summary>
      /// <param name="range"></param>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToUpper(System.Range range, System.Globalization.CultureInfo? cultureInfo = null)
      {
        var (offset, length) = range.GetOffsetAndLength(source.Length);

        return ToUpper(source, offset, length, cultureInfo);
      }

      /// <summary>
      /// <para>Convert runes in a <see cref="System.Span{T}"/> to upper-case.</para>
      /// </summary>
      /// <param name="cultureInfo">If null, then use <see cref="System.Globalization.CultureInfo.CurrentCulture"/>.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToUpper(System.Globalization.CultureInfo? cultureInfo = null)
        => ToUpper(source, 0, source.Length, cultureInfo);

      #endregion
    }

    #region HeapSort helpers

    private static int HeapSort_GetParent(int index)
       => (index - 1) / 2;

    private static int HeapSort_GetLeftChild(int index)
      => 2 * index + 1;

    private static int HeapSort_GetRightChild(int index)
      => 2 * index + 2;

    private static void HeapSort_BasicHeapSort<T>(System.Span<T> source, int count, System.Collections.Generic.IComparer<T> comparer)
    {
      HeapSort_BasicHeapifyDown(source, count, comparer); // Build the heap in array a so that largest value is at the root.

      // The loop maintains the invariants that source[0:end] is a heap and every element beyond end is greater than everything before it (so source[end:count] is in sorted order).

      var end = count - 1;

      while (end > 0)
      {
        source.Swap(end, 0); // source[0] is the root and largest value.The swap moves it in front of the sorted elements.

        end--; // The heap size is reduced by one.

        HeapSort_BasicSiftDown(source, 0, end, comparer); // The swap ruined the heap property, so restore it.
      }
    }

    // (Put elements of 'a' in heap order, in-place)
    private static void HeapSort_BasicHeapifyDown<T>(System.Span<T> source, int count, System.Collections.Generic.IComparer<T> comparer)
    {
      var start = HeapSort_GetParent(count - 1); // Start is assigned the index in 'source' of the last parent node. The last element in a 0-based array is at [count - 1]; find the parent of that element.

      while (start >= 0)
      {
        // (sift down the node at index 'start' to the proper place such that all nodes below the start index are in heap order)
        HeapSort_BasicSiftDown(source, start, count - 1, comparer);
        // (go to the next parent node)
        start--;
        // (after sifting down the root all nodes / elements are in heap order)

        // (Repair the heap whose root element is at index 'start', assuming the heaps rooted at its children are valid)
      }
    }

    private static void HeapSort_BasicSiftDown<T>(System.Span<T> source, int start, int end, System.Collections.Generic.IComparer<T> comparer)
    {
      var root = start;

      while (HeapSort_GetLeftChild(root) <= end) // While the root has at least one child.
      {
        var child = HeapSort_GetLeftChild(root); // Left child of root.

        var swap = root; // Keeps track of child to swap with.

        if (comparer.Compare(source[swap], source[child]) < 0)
          swap = child;

        if (child + 1 <= end && comparer.Compare(source[swap], source[child + 1]) < 0) // If there is a right child and that child is greater.
          swap = child + 1;

        if (swap == root)
        {
          return; // The root holds the largest element. Since we assume the heaps rooted at the children are valid, this means that we are done.
        }
        else
        {
          source.Swap(root, swap);

          root = swap; // Repeat to continue sifting down the child now.
        }
      }
    }

    private static void HeapSort_FloydHeapSort<T>(System.Span<T> source, int count, System.Collections.Generic.IComparer<T> comparer)
    {
      HeapSort_FloydHeapifyDown(source, count, comparer); // Build the heap in array a so that largest value is at the root.

      // The loop maintains the invariants that source[0:end] is a heap and every element beyond end is greater than everything before it(so source[end:count] is in sorted order).

      var end = count - 1;

      while (end > 0)
      {
        source.Swap(end, 0); // source[0] is the root and largest value.The swap moves it in front of the sorted elements.

        end--; // The heap size is reduced by one.

        HeapSort_FloydSiftDown(source, 0, end, comparer); // The swap ruined the heap property, so restore it.
      }
    }

    // (Put elements of 'a' in heap order, in-place)
    private static void HeapSort_FloydHeapifyDown<T>(System.Span<T> source, int count, System.Collections.Generic.IComparer<T> comparer)
    {
      // Start is assigned the index in source of the last parent node. The last element in a 0-based array is [count-1]; find the parent of that element.
      var start = HeapSort_GetParent(count - 1);

      while (start >= 0)
      {
        // (sift down the node at index 'start' to the proper place such that all nodes below the start index are in heap order)
        HeapSort_FloydSiftDown(source, start, count - 1, comparer);
        // (go to the next parent node)
        start--;
        // (after sifting down the root all nodes / elements are in heap order)

        // (Repair the heap whose root element is at index 'start', assuming the heaps rooted at its children are valid)
      }
    }

    private static int HeapSort_FloydLeafSearch<T>(System.Span<T> source, int i, int end, System.Collections.Generic.IComparer<T> comparer)
    {
      var j = i;

      while (HeapSort_GetRightChild(j) <= end)
      {
        var rightIndex = HeapSort_GetRightChild(j);
        var leftIndex = HeapSort_GetLeftChild(j);

        j = (comparer.Compare(source[rightIndex], source[leftIndex]) > 0) ? rightIndex : leftIndex; // Determine which of j's two children is the greater.
      }

      if (HeapSort_GetLeftChild(j) is var leftChildIndex && leftChildIndex <= end) // At the last level, there might be only one child.
        j = leftChildIndex;

      return j;
    }

    private static void HeapSort_FloydSiftDown<T>(System.Span<T> source, int i, int end, System.Collections.Generic.IComparer<T> comparer)
    {
      var j = HeapSort_FloydLeafSearch(source, i, end, comparer);

      while (comparer.Compare(source[i], source[j]) > 0)
        j = HeapSort_GetParent(j);

      var tmp = source[j];

      source[j] = source[i];

      while (j > i)
      {
        var p = HeapSort_GetParent(j);
        (source[p], tmp) = (tmp, source[p]);
        j = p;
      }
    }

    #endregion

    #region MergeSort helpers

    // Sort the elements between min and max index (inclusive).
    private static void MergeSort_Sort<T>(System.Span<T> source, int minIndex, int maxIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      if (minIndex < maxIndex)
      {
        var centerIndex = minIndex + (maxIndex - minIndex) / 2;

        MergeSort_Sort(source, minIndex, centerIndex, comparer);
        MergeSort_Sort(source, centerIndex + 1, maxIndex, comparer);

        MergeSort_Merge(source, minIndex, centerIndex, maxIndex, comparer);
      }
    }

    private static void MergeSort_Merge<T>(System.Span<T> source, int minIndex, int centerIndex, int maxIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      var minIndex2 = centerIndex + 1;

      if (comparer.Compare(source[centerIndex], source[minIndex2]) <= 0)
        return;

      while (minIndex <= centerIndex && minIndex2 <= maxIndex)
      {
        if (comparer.Compare(source[minIndex], source[minIndex2]) <= 0)
          minIndex++;
        else
        {
          var value = source[minIndex2];
          var index = minIndex2;

          while (index != minIndex) // Shift all the elements between element 1 element 2, right by 1.
          {
            source[index] = source[index - 1];
            index--;
          }

          source[minIndex] = value;

          minIndex++;
          centerIndex++;
          minIndex2++;
        }
      }
    }

    #endregion

    #region MergeSortToCopy helpers

    /// <summary>The <paramref name="source"/> array has the items to sort, and the <paramref name="target"/> is the work array.</summary>
    private static void MergeSortToCopy_BottomUpMergeSort<T>(System.Span<T> source, System.Span<T> target, int length, System.Collections.Generic.IComparer<T> comparer)
    {
      for (var width = 1; width < length; width = 2 * width) // Each 1-element run in A is already "sorted". Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted.
      {
        for (var i = 0; i < length; i += 2 * width) // Array A is full of runs of length width.
        {
          MergeSortToCopy_BottomUpMerge(source, target, i, int.Min(i + width, length), int.Min(i + 2 * width, length), comparer); // Merge two runs: source[i:i+width-1] and target[i+width:i+2*width-1] to B[] // or copy A[i:n-1] to B[] ( if(i+width >= n) )
        }

        MergeSortToCopy_BottomUpCopyArray(target, source, length); // Now work array target is full of runs of length 2*width. Copy array target to array source for next iteration. A more efficient implementation would swap the roles of A and B. Now array source is full of runs of length 2*width.
      }
    }

    /// <summary>Left run is source[leftIndex:rightIndex-1] and right run is source[rightIndex:endIndex-1].</summary>
    private static void MergeSortToCopy_BottomUpMerge<T>(System.Span<T> source, System.Span<T> target, int leftIndex, int rightIndex, int endIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      int l = leftIndex, r = rightIndex, k = leftIndex;
      while (k < endIndex) // While there are elements in either runs...
      {
        if (l < rightIndex && (r >= endIndex || comparer.Compare(source[l], source[r]) <= 0)) // If left run head exists and is <= existing right run head.
          target[k++] = source[l++];
        else
          target[k++] = source[r++];
      }
    }

    private static void MergeSortToCopy_BottomUpCopyArray<T>(System.Span<T> source, System.Span<T> target, int length)
    {
      for (var index = 0; index < length; index++)
        target[index] = source[index];
    }

    private static void MergeSortToCopy_TopDownMergeSort<T>(System.Span<T> source, System.Span<T> target, int length, System.Collections.Generic.IComparer<T> comparer)
    {
      MergeSortToCopy_TopDownCopyArrayByIndexing(source, target, 0, length); // One time copy of source[] to target[].

      MergeSortToCopy_TopDownSplitMerge(target, source, 0, length, comparer); // Sort data from target[] into source[].
    }

    /// <summary>Sort the given run of array <paramref name="source"/> using array <paramref name="target"/> as source; beginIndex is inclusive; endIndex is exclusive (.e. source[endIndex] is not in the set).</summary>
    private static void MergeSortToCopy_TopDownSplitMerge<T>(System.Span<T> target, System.Span<T> source, int beginIndex, int endIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      if (endIndex - beginIndex <= 1) // Already sorted?
        return;

      var splitIndex = (endIndex + beginIndex) / 2; // Split the run longer than 1 item into halves, where iMiddle = mid point.

      MergeSortToCopy_TopDownSplitMerge(source, target, beginIndex, splitIndex, comparer);  // Recursively sort the left run
      MergeSortToCopy_TopDownSplitMerge(source, target, splitIndex, endIndex, comparer);  // Recursively sort the right run

      MergeSortToCopy_TopDownMerge(target, source, beginIndex, splitIndex, endIndex, comparer); // merge the resulting runs from array B[] into A[]
    }

    /// <summary>Left half is source[begin:middle-1], right half is source[middle:end-1], result is target[begin:end-1].</summary>
    private static void MergeSortToCopy_TopDownMerge<T>(System.Span<T> source, System.Span<T> target, int beginIndex, int middleIndex, int endIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      int i = beginIndex, j = middleIndex, k = beginIndex;
      while (k < endIndex) // While there are elements in either runs...
      {
        if (i < middleIndex && (j >= endIndex || comparer.Compare(source[i], source[j]) <= 0)) // If left run head exists and is <= existing right run head.
          target[k++] = source[i++];
        else
          target[k++] = source[j++];
      }
    }

    /// <summary>Copy <paramref name="source"/> to <paramref name="target"/> starting at beginIndex (inclusive) up to endIndex (exclusive).</summary>
    private static void MergeSortToCopy_TopDownCopyArrayByIndexing<T>(System.Span<T> source, System.Span<T> target, int beginIndex, int endIndex)
    {
      for (var index = beginIndex; index < endIndex; index++)
        target[index] = source[index];
    }

    #endregion

    #region QuickSort helpers

    private static void QuickSort_Impl<T>(System.Span<T> source, int lowIndex, int highIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      if (lowIndex < highIndex)
      {
        var pivotIndex = QuickSort_Partition(source, lowIndex, highIndex, comparer);

        QuickSort_Impl(source, lowIndex, pivotIndex - 1, comparer);
        QuickSort_Impl(source, pivotIndex + 1, highIndex, comparer);
      }
    }

    private static int QuickSort_Partition<T>(System.Span<T> source, int lowIndex, int highIndex, System.Collections.Generic.IComparer<T> comparer)
    {
      var pivotValue = source[highIndex];
      var i = lowIndex;

      for (var j = lowIndex; j < highIndex; j++)
      {
        if (comparer.Compare(source[j], pivotValue) < 0)
        {
          source.Swap(i, j);
          i++;
        }
      }

      source.Swap(i, highIndex);

      return i;
    }

    #endregion

    #region QuickSelect helpers

    /// <summary>Group a list (from left to right index) into two parts, those less than a certain element, and those greater than or equal to the element.</summary>
    private static int QuickSelectPartition<T>(System.Span<T> source, int pivotIndex, int leftIndex, int rightIndex, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      var pivotValue = source[pivotIndex];

      source.Swap(pivotIndex, rightIndex);

      var storeIndex = leftIndex;

      for (var i = leftIndex; i < rightIndex; i++)
        if (comparer.Compare(source[i], pivotValue) <= 0)
          source.Swap(storeIndex++, i);

      source.Swap(rightIndex, storeIndex);

      return storeIndex;
    }

    #endregion

    #region ShellSort helpers

    private static readonly int[] ShellSort_MarcinCiuraGapSequence = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 }; // Marcin Ciura's gap sequence.

    #endregion
  }
}
