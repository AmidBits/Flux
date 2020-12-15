namespace Flux
{
	public static class DiagnosticsEm
	{
		public static T Dump<T>(this T value)
		{
			System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(value));

			return value;
		}
		public static T DumpDebug<T>(this T value)
		{
			System.Diagnostics.Debug.WriteLine(System.Text.Json.JsonSerializer.Serialize(value));

			return value;
		}
		public static T DumpTrace<T>(this T value)
		{
			System.Diagnostics.Trace.WriteLine(System.Text.Json.JsonSerializer.Serialize(value));

			return value;
		}
	}
}
