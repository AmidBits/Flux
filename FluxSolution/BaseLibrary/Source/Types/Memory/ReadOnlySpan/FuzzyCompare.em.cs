using System.Linq;
using System.Reflection;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IDictionary<string, double> GetSimpleMatchingCoefficients<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      var scores = new System.Collections.Generic.Dictionary<string, double>();

      var s = source.ToArray();
      var t = target.ToArray();

      typeof(Flux.Metrical.ISimpleMatchingCoefficient<>).GetDerivedTypes().Select(type => (type, s, t)).AsParallel().ForAll(vt =>
      {
        try
        {
          var instance = (Flux.Metrical.ISimpleMatchingCoefficient<T>?)System.Activator.CreateInstance(vt.type.MakeGenericType(typeof(T)));

          var smc = instance?.GetSimpleMatchingCoefficient(vt.s, vt.t) ?? 0;

          scores.Add(vt.type.Name.Replace("`1", string.Empty), smc);
        }
        catch { }
      });

      return scores;
    }

    /// <summary>Indicates whether any element in the sequence satisfies the predicate.</summary>
    public static bool FuzzyEquals<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, double minimumScore)
      => GetSimpleMatchingCoefficients(source, target).Any(kvp => kvp.Value >= minimumScore);
  }
}
