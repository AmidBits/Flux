namespace Flux.Sorting
{
  /// <summary>Sorts the content of the sequence using a heap sort, which is more or less an improved selection sort.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Heap_sort"/>
  public sealed class HeapSort<T>
    : ISortableInPlace<T>
  {
    private readonly HeapSortType m_type;

    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public HeapSort(HeapSortType type, System.Collections.Generic.IComparer<T> comparer)
    {
      m_type = type;

      Comparer = comparer;
    }
    public HeapSort()
      : this(HeapSortType.FloydDown, System.Collections.Generic.Comparer<T>.Default)
    { }

    public void SortInPlace(System.Span<T> source)
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
      BasicHeapifyDown(source, count); // Build the heap in array a so that largest value is at the root.

      // The loop maintains the invariants that source[0:end] is a heap and every element beyond end is greater than everything before it (so source[end:count] is in sorted order).

      var end = count - 1;

      while (end > 0)
      {
        source.Swap(end, 0); // source[0] is the root and largest value.The swap moves it in front of the sorted elements.

        end--; // The heap size is reduced by one.

        BasicSiftDown(source, 0, end); // The swap ruined the heap property, so restore it.
      }
    }

    // (Put elements of 'a' in heap order, in-place)
    
    private void BasicHeapifyDown(System.Span<T> source, int count)
    {
      var start = GetParent(count - 1); // Start is assigned the index in 'source' of the last parent node. The last element in a 0-based array is at [count - 1]; find the parent of that element.

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

      while (GetLeftChild(root) <= end) // While the root has at least one child.
      {
        var child = GetLeftChild(root); // Left child of root.

        var swap = root; // Keeps track of child to swap with.

        if (Comparer.Compare(source[swap], source[child]) < 0)
          swap = child;

        if (child + 1 <= end && Comparer.Compare(source[swap], source[child + 1]) < 0) // If there is a right child and that child is greater.
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
    #endregion Heap sort basic down helpers

    #region Heap sort Floyd's construction (down) helpers
    
    private void FloydHeapSort(System.Span<T> source, int count)
    {
      FloydHeapifyDown(source, count); // Build the heap in array a so that largest value is at the root.

      // The loop maintains the invariants that source[0:end] is a heap and every element beyond end is greater than everything before it(so source[end:count] is in sorted order).

      var end = count - 1;

      while (end > 0)
      {
        source.Swap(end, 0); // source[0] is the root and largest value.The swap moves it in front of the sorted elements.

        end--; // The heap size is reduced by one.

        FloydSiftDown(source, 0, end); // The swap ruined the heap property, so restore it.
      }
    }

    // (Put elements of 'a' in heap order, in-place)
    
    private void FloydHeapifyDown(System.Span<T> source, int count)
    {
      // Start is assigned the index in source of the last parent node. The last element in a 0-based array is [count-1]; find the parent of that element.
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
        var rightIndex = GetRightChild(j);
        var leftIndex = GetLeftChild(j);

        j = (Comparer.Compare(source[rightIndex], source[leftIndex]) > 0) ? rightIndex : leftIndex; // Determine which of j's two children is the greater.
      }

      if (GetLeftChild(j) is var leftChildIndex && leftChildIndex <= end) // At the last level, there might be only one child.
        j = leftChildIndex;

      return j;
    }

    
    private void FloydSiftDown(System.Span<T> source, int i, int end)
    {
      var j = FloydLeafSearch(source, i, end);

      while (Comparer.Compare(source[i], source[j]) > 0)
        j = GetParent(j);

      var tmp = source[j];

      source[j] = source[i];

      while (j > i)
      {
        var p = GetParent(j);
        (source[p], tmp) = (tmp, source[p]);
        j = p;
      }
    }
    #endregion Heap sort Floyd's construction (down) helpers
  }
}
