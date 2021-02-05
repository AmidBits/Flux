namespace Flux
{
	public static partial class SystemTextEm
	{
		public static void CopyTo(this System.Text.StringBuilder source, int sourceStartIndex, System.Collections.Generic.IList<char> target, int targetStartIndex, int count)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (sourceStartIndex < 0 || sourceStartIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
			if (target is null) throw new System.ArgumentNullException(nameof(target));
			if (targetStartIndex < 0 || targetStartIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));
			if (count <= 0 || (sourceStartIndex + count) > source.Length || (targetStartIndex + count) > target.Count) throw new System.ArgumentOutOfRangeException(nameof(count));

			while (count-- > 0)
				target[targetStartIndex++] = source[sourceStartIndex++];
		}
		public static void CopyTo(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> target, int count)
			=> CopyTo(source, 0, target, 0, count);
		public static int CopyTo(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> target)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));
			if (target is null) throw new System.ArgumentNullException(nameof(target));

			var count = System.Math.Min(source.Length, target.Count);
			CopyTo(source, 0, target, 0, count);
			return count;
		}
	}
}
