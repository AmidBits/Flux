namespace Flux
{
	public static partial class SystemUriEm
	{
		public static bool TryConnectTo(this System.Uri uri)
		{
			try
			{
				using var wc = new System.Net.WebClient();
				using var s = wc.OpenRead(uri);

				return true;
			}
#pragma warning disable CA1031 // Do not catch general exception types.
			catch
#pragma warning restore CA1031 // Do not catch general exception types.
			{ }
			return false;
		}
	}
}
