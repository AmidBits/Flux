namespace Flux
{
	public static partial class ILists
	{
		/// <summary></summary>
		/// <see cref="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/>
		public static System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<T>> GetHeapsPermutations<T>(this System.Collections.Generic.IList<T> source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			return PermutateHeaps2(source);

			static System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<T>> PermutateHeaps2(System.Collections.Generic.IList<T> list)
			{
				var stackState = new int[list.Count];

				System.Array.Fill(stackState, default);

				yield return (System.Collections.Generic.IReadOnlyList<T>)list;

				for (var stackIndex = 0; stackIndex < stackState.Length;)
				{
					if (stackState[stackIndex] < stackIndex)
					{
						if ((stackIndex & 1) == 0)
							SpanEm.Swap(list, 0, stackIndex);
						else
							SpanEm.Swap(list, stackState[stackIndex], stackIndex);

						yield return (System.Collections.Generic.IReadOnlyList<T>)list;

						stackState[stackIndex]++;
						stackIndex = 0;
					}
					else
					{
						stackState[stackIndex] = 0;
						stackIndex++;
					}
				}
			}
		}
	}
}