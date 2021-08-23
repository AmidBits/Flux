using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Throw an exception if the sequence is null.</summary>
    public static System.Collections.Generic.IEnumerable<T> ThrowOnNull<T>(this System.Collections.Generic.IEnumerable<T>? source, string? name = null)
      => source ?? throw new System.ArgumentNullException(name ?? nameof(source));
  }
}
