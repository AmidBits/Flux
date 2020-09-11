namespace Flux.Collections.Generic
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public interface IHeap<T>
    : System.Collections.Generic.IEnumerable<T>
  {
    int Count { get; }
    T Extract();
    void Insert(T item);
    bool IsEmpty { get; }
    T Peek();
  }

  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public interface IHeapMax<T>
    //: IHeap<T>
  {
    int Count { get; }
    T ExtractMax();
    void Insert(T item);
    bool IsEmpty { get; }
    T Peek();
  }
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Heap_(data_structure)"/>
  public interface IHeapMin<T>
    //: IHeap<T>
  {
    int Count { get; }
    T ExtractMin();
    void Insert(T item);
    bool IsEmpty { get; }
    T Peek();
  }
}
