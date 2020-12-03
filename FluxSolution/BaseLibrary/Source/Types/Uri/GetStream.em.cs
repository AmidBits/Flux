namespace Flux
{
	public static partial class UriEm
	{
		/// <summary>Creates and returns a System.IO.Stream from the URI source.</summary>
		/// <param name="uri"></param>
		/// <returns>A stream from a file, http or other URI resource.</returns>
		/// <example>new System.IO.StreamReader(new System.Uri(@"file://\Flux\Resources\Data\Ucd_UnicodeText.txt\").GetStream(), System.Text.Encoding.UTF8)</example>
		public static System.IO.Stream GetStream(this System.Uri uri)
			=> (uri ?? throw new System.ArgumentNullException(nameof(uri))).IsFile
			? (new System.IO.FileStream(uri.LocalPath.StartsWith(@"/", System.StringComparison.Ordinal) ? uri.LocalPath.Substring(1) : uri.LocalPath, System.IO.FileMode.Open))
			: System.Net.WebRequest.Create(uri).GetResponse().GetResponseStream();
	}
}
