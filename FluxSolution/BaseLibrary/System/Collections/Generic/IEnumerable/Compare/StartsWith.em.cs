using System.Data.Common;
using System.Linq;

namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Determines whether the source sequence begins with the target sequence. Uses the specified equality comparer.</summary>
    /// <exception cref="System.ArgumentNullException"/>
    public static bool StartsWith<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      return source.Zip(target).All(z => equalityComparer.Equals(z.First, z.Second));
    }
  }
}
