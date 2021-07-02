using System.Linq;

namespace Flux.Services
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
			System.IO.File.AppendAllLines(_logFileName, new string[] { $"{System.DateTime.Now.ToStringSqlYyyyMmDdHhMmSsFffffff()} {message}" });
		}

		public void Write(params string[] messages)
		{
			System.IO.File.AppendAllLines(_logFileName, messages.Select((m, i) => i == 0 ? $"{System.DateTime.Now.ToStringSqlYyyyMmDdHhMmSsFffffff()} {m}" : $"{m}"));
		}

		public void Write(System.Exception exception)
		{
			if (exception is null) throw new System.ArgumentNullException(nameof(exception));

			System.IO.File.AppendAllLines(_logFileName, new string[] { $"{System.DateTime.Now.ToStringSqlYyyyMmDdHhMmSsFffffff()} EXCEPTION {exception.Message}", exception.StackTrace ?? @"No StackTrace Available." });
		}
	}
}
