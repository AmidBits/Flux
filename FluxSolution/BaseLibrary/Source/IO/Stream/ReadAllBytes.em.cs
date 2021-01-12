namespace Flux
{
	public static partial class SystemUriEm
	{
		public static byte[] ReadAllBytes(this System.IO.Stream source)
		{
			using var reader = new System.IO.BinaryReader(source);

			return reader.ReadBytes(int.MaxValue);
		}
	}
}
