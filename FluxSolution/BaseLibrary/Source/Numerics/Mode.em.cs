using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>
    /// <para>Create a new sequence ordered by descending mode.</para>
    /// <see href="http://en.wikipedia.org/wiki/Mode"/>
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TValue, int>> Mode<TValue>(this System.Collections.Generic.IEnumerable<TValue> source)
      where TValue : System.Numerics.INumberBase<TValue>
      => source.GroupBy(v => v).OrderByDescending(g => g.Count()).Select(g => new System.Collections.Generic.KeyValuePair<TValue, int>(g.Key, g.Count()));
  }
}
