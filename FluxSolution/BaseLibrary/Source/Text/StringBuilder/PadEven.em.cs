namespace Flux
{
	public static partial class ExtensionMethods
	{
		/// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding characters for left and right respectively.</summary>
		public static System.Text.StringBuilder PadEven(this System.Text.StringBuilder source, int totalWidth, char paddingLeft, char paddingRight)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			if (totalWidth > source.Length)
			{
				PadRight(source, source.Length + (totalWidth - source.Length) / 2, paddingLeft);
				PadLeft(source, totalWidth, paddingRight);
			}

			return source;
		}
		/// <summary>Pads the StringBuilder evenly on both sides to the specified width by the specified padding strings for left and right respectively.</summary>
		public static System.Text.StringBuilder PadEven(this System.Text.StringBuilder source, int totalWidth, string paddingLeft, string paddingRight)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			if (totalWidth > source.Length)
			{
				PadRight(source, source.Length + (totalWidth - source.Length) / 2, paddingLeft);
				PadLeft(source, totalWidth, paddingRight);
			}

			return source;
		}
	}
}
