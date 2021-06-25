namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Pads this StringBuilder on the right with the specified padding character.</summary>
		public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, char padding)
			=> (source ?? throw new System.ArgumentNullException(nameof(source))).Append(padding, totalWidth - source.Length);
		/// <summary>Pads this StringBuilder on the right with the specified padding string.</summary>
		public static System.Text.StringBuilder PadRight(this System.Text.StringBuilder source, int totalWidth, System.ReadOnlySpan<char> padding)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			while (source.Length < totalWidth)
				source.Append(padding);

			source.Remove(totalWidth, source.Length - totalWidth);

			return source;
		}
	}
}
