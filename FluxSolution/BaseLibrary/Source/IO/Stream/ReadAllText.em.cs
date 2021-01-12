namespace Flux
{
	public static partial class SystemUriEm
	{
		public static string ReadAllText(this System.IO.Stream source, System.Text.Encoding encoding)
		{
			using var reader = new System.IO.StreamReader(source, encoding);

			return reader.ReadToEnd();
		}
	}
}
