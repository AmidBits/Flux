namespace Flux
{
  #region ExtensionMethods

  public static partial class Em
  {
    public static Flux.DataStructures.IStack<T> Reverse<T>(this Flux.DataStructures.IStack<T> source)
    {
      Flux.DataStructures.IStack<T> reverse = Flux.DataStructures.Immutable.ImmutableStack<T>.Empty;
      for (Flux.DataStructures.IStack<T> f = source; !f.IsEmpty; f = f.Pop())
        reverse = reverse.Push(f.Peek());
      return reverse;
    }
  }

  #endregion // ExtensionMethods
}

