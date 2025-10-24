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

  public static partial class XtensionSpan
  {
    extension(System.Span<char> source)
    {
      /// <summary>
      /// <para>Capitalize any lower-case char at the beginning or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<char> Capitalize(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          var c = source[index]; // Avoid multiple indexers.

          if (!char.IsLower(c)) continue; // If, c is not lower-case, advance.

          if (index > 0 && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure left char exists) left is not white-space, advance.

          if (index < maxIndex && !char.IsLower(source[index + 1])) continue; // If, (ensure right char exists) right is not lower-case, advance.

          source[index] = char.ToUpper(c, culture);
        }

        return source;
      }

      /// <summary>
      /// <para>Convert all chars in the span to lower-case.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<char> ToLower(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var index = source.Length - 1; index >= 0; index--)
          if (source[index] is var sourceChar && char.ToLower(sourceChar, culture) is var targetChar && sourceChar != targetChar)
            source[index] = targetChar;

        return source;
      }

      /// <summary>
      /// <para>Convert all chars in the span to upper-case.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<char> ToUpper(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var index = source.Length - 1; index >= 0; index--)
          if (source[index] is var sourceChar && char.ToUpper(sourceChar, culture) is var targetChar && sourceChar != targetChar)
            source[index] = targetChar;

        return source;
      }

      /// <summary>
      /// <para>Uncapitalize any upper-case char at the beginning or with a whitespace on the left, and with a lower-case char on the right.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<char> Uncapitalize(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          var c = source[index]; // Avoid multiple indexers.

          if (!char.IsUpper(c)) continue; // If, c is not upper-case, advance.

          if (index > 0 && !char.IsWhiteSpace(source[index - 1])) continue; // If, (ensure left char exists) left is not white-space, advance.

          if (index < maxIndex && !char.IsLower(source[index + 1])) continue; // If, (ensure right char exists) right is not lower-case, advance.

          source[index] = char.ToLower(c, culture);
        }

        return source;
      }
    }

    extension(System.Span<System.Text.Rune> source)
    {
      /// <summary>
      /// <para>Capitalize any lower-case rune at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> Capitalize(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          var r = source[index]; // Avoid multiple indexers.

          if (!System.Text.Rune.IsLower(r)) continue; // If, r is not lower-case, advance.

          if (index > 0 && !System.Text.Rune.IsWhiteSpace(source[index - 1])) continue; // If, (ensure left char exists) left is not white-space, advance.

          if (index < maxIndex && !System.Text.Rune.IsLower(source[index + 1])) continue; // If, (ensure right char exists) right is not lower-case, advance.

          source[index] = System.Text.Rune.ToUpper(r, culture);
        }

        return source;
      }

      /// <summary>
      /// <para>Convert all runes in the span to lower-case.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToLower(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var index = source.Length - 1; index >= 0; index--)
          if (source[index] is var sourceRune && System.Text.Rune.ToLower(sourceRune, culture) is var targetRune && sourceRune != targetRune)
            source[index] = targetRune;

        return source;
      }

      /// <summary>
      /// <para>Convert all runes in the span to upper-case.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> ToUpper(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        for (var index = source.Length - 1; index >= 0; index--)
          if (source[index] is var sourceRune && System.Text.Rune.ToUpper(sourceRune, culture) is var targetRune && sourceRune != targetRune)
            source[index] = targetRune;

        return source;
      }

      /// <summary>
      /// <para>Uncapitalize any upper-case rune at the beginning or with a whitespace on the left, and with a lower-case rune on the right.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="culture">Set to <see cref="System.Globalization.CultureInfo.CurrentCulture"/> if null.</param>
      /// <returns></returns>
      public System.Span<System.Text.Rune> Uncapitalize(System.Globalization.CultureInfo? culture = null)
      {
        culture ??= System.Globalization.CultureInfo.CurrentCulture;

        var maxIndex = source.Length - 1;

        for (var index = maxIndex; index >= 0; index--)
        {
          var r = source[index]; // Avoid multiple indexers.

          if (!System.Text.Rune.IsUpper(r)) continue; // If, r is not upper-case, advance.

          if (index > 0 && !System.Text.Rune.IsWhiteSpace(source[index - 1])) continue; // If, (ensure left char exists) left is not white-space, advance.

          if (index < maxIndex && !System.Text.Rune.IsLower(source[index + 1])) continue; // If, (ensure right char exists) right is not lower-case, advance.

          source[index] = System.Text.Rune.ToLower(r, culture);
        }

        return source;
      }
    }

    extension<T>(System.Span<T> source)
    {
      /// <summary>Non-allocating conversion (casting) from <see cref="System.Span{T}"/> to <see cref="System.ReadOnlySpan{T}"/>.</summary>
      public System.ReadOnlySpan<T> AsReadOnlySpan() => source;

      #region Replace

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

      public System.Span<T> Replace(System.Func<T, T> replacementSelector)
        => Replace(source, (e, i) => true, (e, i) => replacementSelector(e));

      #endregion

      #region Rotate

      public void RotateLeft1()
      {
        var tmp = source[0];
        source[1..].CopyTo(source[..^1]);
        source[^1] = tmp;
      }

      public void RotateRight1()
      {
        var tmp = source[^1];
        source[0..^1].CopyTo(source[1..]);
        source[0] = tmp;
      }

      public void RotateLeft(int count)
      {
        count %= source.Length;

        source.Reverse();
        source[^count..].Reverse();
        source[..^count].Reverse();
      }

      public void RotateRight(int count)
      {
        count %= source.Length;

        source.Reverse();
        source[..count].Reverse();
        source[count..].Reverse();
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

      /// <summary>In-place swap of two elements by the specified indices.</summary>
      public bool Swap(int indexA, int indexB)
      {
        if ((indexA != indexB) is var isUnequal && isUnequal) // No need to actually swap if the indices are the same.
          (source[indexB], source[indexA]) = (source[indexA], source[indexB]);

        return isUnequal;
      }

      /// <summary>In-place swap of two elements by the specified index and the first element.</summary>
      public bool SwapFirstWith(int index)
        => Swap(source, 0, index);

      /// <summary>In-place swap of two elements by the specified index and the last element.</summary>
      public bool SwapLastWith(int index)
        => Swap(source, index, source.Length - 1);

      #endregion
    }

    #region Helpers HeapSort

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

    #region Helpers MergeSort

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

    #region Helpers MergeSortToCopy

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

    #region Helpers QuickSort

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

    #region QuickSelect

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

    extension<T>(System.Span<T> source)
    {
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
    }

    #endregion

    #region Helpers ShellSort

    private static readonly int[] ShellSort_MarcinCiuraGapSequence = new int[] { 701, 301, 132, 57, 23, 10, 4, 1 }; // Marcin Ciura's gap sequence.

    #endregion
  }
}
