using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    /// <summary>
    /// <para>Create a new sequence ordered by descending mode.</para>
    /// <see href="http://en.wikipedia.org/wiki/Mode"/>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TSelf, int>> Mode<TSelf>(this System.Collections.Generic.IEnumerable<TSelf> source)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => source.GroupBy(v => v).OrderByDescending(g => g.Count()).Select(g => new System.Collections.Generic.KeyValuePair<TSelf, int>(g.Key, g.Count()));
  }
}
