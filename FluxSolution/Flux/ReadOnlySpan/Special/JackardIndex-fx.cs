namespace Flux
{
  public static class GilbertJackardTanimoto
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>The Jaccard coefficient measures similarity between finite sample sets, and is defined as the size of the intersection divided by the size of the union of the sample sets. The Jaccard distance measures dissimilarity between sample sets, is complementary to the Jaccard coefficient and is obtained by subtracting the Jaccard coefficient from 1 (i.e.: JackardDistance = 1 - JackardIndex). The Jackard index needs to iterate each sequence multiple times, if that is an issue opt to buffer.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Jaccard_index"/></para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public double JackardIndex(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => (double)source.Intersect(target, equalityComparer).Count / (double)source.Union(target, equalityComparer).Count;
    }
  }
}
