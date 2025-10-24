namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>The Sørensen–Dice coefficient is a statistic used to gauge the similarity of two samples. The algorithm will potentially iterate multiple times over the sequences, so if that is an issue then opt to buffer.</para>
      /// <see href="https://en.wikipedia.org/wiki/Dice-S%C3%B8rensen_coefficient"/>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public double DiceSørensenCoefficient(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
        => 2d * (double)source.Intersect(target, equalityComparer).Count / (double)(source.Length + target.Length);
    }
  }
}
