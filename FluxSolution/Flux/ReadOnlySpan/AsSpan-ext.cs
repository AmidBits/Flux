namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>Creates a new (non-allocating) span over a read-only span.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Span<T> AsSpan()
        => System.Runtime.InteropServices.MemoryMarshal.CreateSpan(ref System.Runtime.InteropServices.MemoryMarshal.GetReference(source), source.Length);
    }
  }
}
