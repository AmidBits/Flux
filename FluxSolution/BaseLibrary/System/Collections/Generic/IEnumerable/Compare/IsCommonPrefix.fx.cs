namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Determines whether the source sequence begins with the target sequence. Uses the specified equality comparer.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <param name="equalityComparer"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool IsCommonPrefix<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Zip(target).All(z => equalityComparer.Equals(z.First, z.Second));
    }
  }
}