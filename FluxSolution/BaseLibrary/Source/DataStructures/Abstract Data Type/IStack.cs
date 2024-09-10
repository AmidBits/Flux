namespace Flux.DataStructures
{
  /// <summary>
  /// <para></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue"></typeparam>
  public interface IStack<TValue>
    : System.Collections.Generic.IEnumerable<TValue>
  {
    bool IsEmpty { get; }
    TValue Peek();
    IStack<TValue> Pop();
    IStack<TValue> Push(TValue value);
  }
}
