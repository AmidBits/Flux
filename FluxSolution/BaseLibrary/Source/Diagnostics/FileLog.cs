using System.Linq;

namespace Flux.Diagnostics
{
	public class FileLog
	{
		private readonly string _logFileName;

		public FileLog(string folder, string fileName)
		{
			_logFileName = System.IO.Path.Combine(folder, $"{fileName}.log");

			System.IO.File.Delete(_logFileName);
		}

		public void Write(string message)
		{
			System.IO.File.AppendAllLines(_logFileName, new string[] { $"{System.DateTime.Now.ToStringISO8601Full()} {message}" });
		}

		public void Write(params string[] messages)
		{
			System.IO.File.AppendAllLines(_logFileName, messages.Select((m, i) => i == 0 ? $"{System.DateTime.Now.ToStringISO8601Full()} {m}" : $"{m}"));
		}

		public void Write(System.Exception exception)
		{
			System.IO.File.AppendAllLines(_logFileName, new string[] { $"{System.DateTime.Now.ToStringISO8601Full()} EXCEPTION {exception.Message}", exception.StackTrace ?? @"No StackTrace Available." });
		}
	}
}
