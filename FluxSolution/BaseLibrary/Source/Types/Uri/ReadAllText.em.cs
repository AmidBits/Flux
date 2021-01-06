namespace Flux
{
	public static partial class SystemUriEm
	{
		public static string ReadAllText(this System.Uri uri, System.Text.Encoding encoding)
		{
			using var reader = new System.IO.StreamReader(uri.GetStream(), encoding);

			return reader.ReadToEnd();
		}
	}
}
