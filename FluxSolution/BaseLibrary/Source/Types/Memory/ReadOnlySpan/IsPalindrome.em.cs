namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Determines whether the sequence is a palindrome. Uses the specified comparer.</summary>
		public static bool IsPalindrome<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T> comparer)
		{
			if (comparer is null) throw new System.ArgumentNullException(nameof(comparer));

			for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
				if (!comparer.Equals(source[indexL], source[indexR]))
					return false;

			return true;
		}
		/// <summary>Determines whether the sequence is a palindrome. Uses the default comparer.</summary>
		public static bool IsPalindrome<T>(this System.ReadOnlySpan<T> source)
			=> IsPalindrome(source, System.Collections.Generic.EqualityComparer<T>.Default);
	}
}
