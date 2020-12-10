using System.Linq;

namespace Flux
{
	public static partial class IEnumerableEm
	{
		/// <summary>Indicates whether the sequence contains the same elements (in any order) as the specified sequence, by ordering and using the built-in SequenceEqual extension method.</summary>
		public static bool SequenceContentEqualOrderBy<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
			where T : System.IEquatable<T>
			=> !source.OrderBy(e => e).Zip(target.OrderBy(e => e), (a, b) => !a.Equals(b)).Any();

		/// <summary>Indicates whether the sequence contains the same elements (in any order) as the specified sequence, by Xor'ing the hash codes of all elements.</summary>
		public static bool SequenceContentEqualByXor<T>(this System.Collections.Generic.IEnumerable<T> source, System.Collections.Generic.IEnumerable<T> target)
			=> source.Aggregate(0, (a, e) => a ^ (e?.GetHashCode() ?? 0)) == target.Aggregate(0, (a, e) => a ^ (e?.GetHashCode() ?? 0));
	}
}
