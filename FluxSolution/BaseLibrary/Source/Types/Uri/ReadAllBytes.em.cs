namespace Flux
{
	public static partial class UriEm
	{
		public static byte[] ReadAllBytes(this System.Uri uri)
		{
			using var reader = new System.IO.BinaryReader(uri.GetStream());

			return reader.ReadBytes(int.MaxValue);
		}
	}
}
