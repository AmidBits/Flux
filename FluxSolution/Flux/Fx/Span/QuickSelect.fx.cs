namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Find the Kth smallest element in an unordered list, between left and right index.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Quickselect"/>
    public static T QuickSelect<T>(this System.Span<T> source, int leftIndex, int rightIndex, int kth, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

      if (leftIndex == rightIndex)
        return source[leftIndex];

      while (true)
      {
        var pivotIndex = Partition(source, kth);

        if (kth < pivotIndex) rightIndex = pivotIndex - 1;
        else if (kth > pivotIndex) leftIndex = pivotIndex + 1;
        else return source[kth];
      }

      /// <summary>Group a list (from left to right index) into two parts, those less than a certain element, and those greater than or equal to the element.</summary>
      int Partition(System.Span<T> source, int pivotIndex)
      {
        var pivotValue = source[pivotIndex];

        source.Swap(pivotIndex, rightIndex);

        var storeIndex = leftIndex;

        for (var i = leftIndex; i < rightIndex; i++)
          if (comparer.Compare(source[i], pivotValue) <= 0)
            source.Swap(storeIndex++, i);

        source.Swap(rightIndex, storeIndex);

        return storeIndex;
      }
    }
  }
}
