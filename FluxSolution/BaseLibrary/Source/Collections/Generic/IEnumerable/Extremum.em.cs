namespace Flux
{
	public static partial class SystemCollectionsGenericEm
	{
		public static (T minimum, T maximum) Extremum<T>(this System.Collections.Generic.IEnumerable<T> source)
			where T : System.IComparable<T>
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			using var e = source.GetEnumerator();

			if (e.MoveNext())
			{
				var minimum = e.Current;
				var maximum = e.Current;

				while (e.MoveNext())
				{
					if (e.Current.CompareTo(minimum) < 0)
						minimum = e.Current;
					if (e.Current.CompareTo(maximum) > 0)
						maximum = e.Current;
				}

				return (minimum, maximum);
			}
			else throw new System.ArgumentException(@"The sequence is empty.", nameof(source));
		}
	}
}
