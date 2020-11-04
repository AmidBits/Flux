namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Sorts the content of the sequence using heap sort.</summary>
    public static void HeapSort<T>(this System.Span<T> source, Sorting.HeapSortType type, System.Collections.Generic.IComparer<T> comparer)
      => new Sorting.HeapSort<T>(type, comparer).Sort(source);
    /// <summary>Sorts the content of the sequence using heap sort.</summary>
    public static void HeapSort<T>(this System.Span<T> source, Sorting.HeapSortType type)
      => HeapSort(source, type, System.Collections.Generic.Comparer<T>.Default);
  }

  namespace Sorting
  {
    public enum HeapSortType
    {
      BasicDown,
      FloydDown
    }

    /// <summary>Sorts the content of the sequence using heap sort.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Heap_sort"/>
    public class HeapSort<T>
      : ASortable<T>, ISortable<T>
    {
      private HeapSortType m_type;

      public HeapSort(HeapSortType type, System.Collections.Generic.IComparer<T> comparer)
        : base(comparer)
      {
        m_type = type;
      }
      public HeapSort()
        : this(HeapSortType.FloydDown, System.Collections.Generic.Comparer<T>.Default)
      {
      }

      public void Sort(System.Collections.Generic.List<T> source)
        => Sort(new System.Span<T>((source ?? throw new System.ArgumentNullException(nameof(source))).ToArray()));
      public void Sort(System.Span<T> source)
      {
        switch (m_type)
        {
          case HeapSortType.BasicDown:
            BasicHeapSort(source, source.Length);
            break;
          case HeapSortType.FloydDown:
            FloydHeapSort(source, source.Length);
            break;
        }
      }

      #region Heap sort helpers
      public int GetParent(int index)
        => (index - 1) / 2;
      public int GetLeftChild(int index)
        => 2 * index + 1;
      public int GetRightChild(int index)
        => 2 * index + 2;
      #endregion Heap sort helpers

      #region Heap sort basic down helpers
      private void BasicHeapSort(System.Span<T> source, int count)
      {
        BasicHeapifyDown(source, count); // (Build the heap in array a so that largest value is at the root)

        // (The following loop maintains the invariants that a[0:end] is a heap and every element beyond end is greater than everything before it(so a[end: count] is in sorted order))

        var end = count - 1;

        while (end > 0)
        {
          source.Swap(end, 0); //(a[0] is the root and largest value.The swap moves it in front of the sorted elements.)

          end--; // // (the heap size is reduced by one)

          BasicSiftDown(source, 0, end); // (the swap ruined the heap property, so restore it)
        }
      }

      // (Put elements of 'a' in heap order, in-place)
      private void BasicHeapifyDown(System.Span<T> source, int count)
      {
        // (start is assigned the index in 'a' of the last parent node)
        // (the last element in a 0-based array is at index count-1; find the parent of that element)
        var start = GetParent(count - 1);

        while (start >= 0)
        {
          // (sift down the node at index 'start' to the proper place such that all nodes below the start index are in heap order)
          BasicSiftDown(source, start, count - 1);
          // (go to the next parent node)
          start--;
          // (after sifting down the root all nodes / elements are in heap order)

          // (Repair the heap whose root element is at index 'start', assuming the heaps rooted at its children are valid)
        }
      }

      private void BasicSiftDown(System.Span<T> source, int start, int end)
      {
        var root = start;

        while (GetLeftChild(root) <= end) // (While the root has at least one child)
        {
          var child = GetLeftChild(root); // (Left child of root)
          var swap = root; // (Keeps track of child to swap with)

          if (Comparer.Compare(source[swap], source[child]) < 0)
            swap = child;
          // (If there is a right child and that child is greater)
          if (child + 1 <= end && Comparer.Compare(source[swap], source[child + 1]) < 0)
            swap = child + 1;
          if (swap == root)
            return; // (The root holds the largest element.Since we assume the heaps rooted at the children are valid, this means that we are done.)
          else
          {
            source.Swap(root, swap);
            root = swap; // (repeat to continue sifting down the child now)
          }
        }
      }
      #endregion Heap sort basic down helpers

      #region Heap sort Floyd's construction (down) helpers
      private void FloydHeapSort(System.Span<T> source, int count)
      {
        FloydHeapifyDown(source, count); // (Build the heap in array a so that largest value is at the root)

        // (The following loop maintains the invariants that a[0:end] is a heap and every element beyond end is greater than everything before it(so a[end: count] is in sorted order))

        var end = count - 1;

        while (end > 0)
        {
          source.Swap(end, 0); //(a[0] is the root and largest value.The swap moves it in front of the sorted elements.)

          end--; // // (the heap size is reduced by one)

          FloydSiftDown(source, 0, end); // (the swap ruined the heap property, so restore it)
        }
      }

      // (Put elements of 'a' in heap order, in-place)
      private void FloydHeapifyDown(System.Span<T> source, int count)
      {
        // (start is assigned the index in 'a' of the last parent node)
        // (the last element in a 0-based array is at index count-1; find the parent of that element)
        var start = GetParent(count - 1);

        while (start >= 0)
        {
          // (sift down the node at index 'start' to the proper place such that all nodes below the start index are in heap order)
          FloydSiftDown(source, start, count - 1);
          // (go to the next parent node)
          start--;
          // (after sifting down the root all nodes / elements are in heap order)

          // (Repair the heap whose root element is at index 'start', assuming the heaps rooted at its children are valid)
        }
      }

      private int FloydLeafSearch(System.Span<T> source, int i, int end)
      {
        var j = i;
        while (GetRightChild(j) <= end)
        {
          j = (Comparer.Compare(source[GetRightChild(j)], source[GetLeftChild(j)]) > 0) ? GetRightChild(j) : GetLeftChild(j); // (Determine which of j's two children is the greater)
        }

        if (GetLeftChild(j) <= end) // (At the last level, there might be only one child)
          j = GetLeftChild(j);

        return j;
      }

      private void FloydSiftDown(System.Span<T> source, int i, int end)
      {
        var j = FloydLeafSearch(source, i, end);

        while (Comparer.Compare(source[i], source[j]) > 0)
          j = GetParent(j);

        var x = source[j];
        source[j] = source[i];

        while (j > i)
        {
          var jp = GetParent(j);
          var sw = x;
          x = source[jp];
          source[jp] = sw;
          j = jp;
        }
      }
      #endregion Heap sort Floyd's construction (down) helpers
    }
  }
}
