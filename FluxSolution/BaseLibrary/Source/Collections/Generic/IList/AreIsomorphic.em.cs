namespace Flux
{
	public static partial class IListEm
	{
		/// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
		/// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
		public static bool AreIsomorphic<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
			where T : notnull
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));
			if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

			if (source.Count != target.Count) return false;

			var map1 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);
			var map2 = new System.Collections.Generic.Dictionary<T, T>(equalityComparer);

			for (var i = source.Count - 1; i >= 0; i--)
			{
				var c1 = source[i];
				var c2 = target[i];

				if (map1.ContainsKey(c1))
				{
					if (!equalityComparer.Equals(map1[c1], c2)) return false;
				}
				else
				{
					if (map2.ContainsKey(c2)) return false;

					map1[c1] = c2;
					map2[c2] = c1;
				}
			}

			return true;
		}
		/// <summary>Indicates whether the given two sequences, a and b, are isomorphic. Two sequences are isomorphic if the characters in a can be replaced to get b.</summary>
		/// <remarks>For example,"egg" and "add" are isomorphic, "foo" and "bar" are not.</remarks>
		public static bool AreIsomorphic<T>(this System.Collections.Generic.IList<T> source, System.Collections.Generic.IList<T> target)
			where T : notnull
			=> AreIsomorphic(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
