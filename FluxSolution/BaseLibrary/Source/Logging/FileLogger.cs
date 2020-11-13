#if Microsoft_Extensions_Logging
using System.Linq;

namespace LoggerApp
{
  public static class FileLoggerExtensions
  {
    public static System.Collections.Generic.IEnumerable<(long lineNumber, long totalCharacters, string value)> GetLineStatistics(this System.IO.FileInfo source)
    {
      var totalCharacterCount = 0L;
      var lineCount = 0L;

      var sr = source?.OpenText() ?? throw new System.ArgumentNullException(nameof(source));

      while (!sr.EndOfStream)
      {
        var line = sr.ReadLine();
        totalCharacterCount += line is null ? 0 : line.Length + 2;
        lineCount++;

        if (line is not null)
        {
          yield return (lineCount, totalCharacterCount, line);
        }
      }

      if (lineCount == 0 && totalCharacterCount == 0)
      {
        yield return (lineCount, totalCharacterCount, string.Empty);
      }
    }

    public static void ShrinkBeginning(this System.IO.FileInfo source, double percentToRemove)
    {
      source.Refresh();

      var (lineNumber, totalCharacters, value) = source.GetLineStatistics().First(vt => vt.totalCharacters > (long)(source.Length * percentToRemove));

      source.ShrinkBeginning(totalCharacters);
    }
    public static void ShrinkBeginning(this System.IO.FileInfo source, long bytesToRemove)
    {
      using var fs = source?.Open(System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite) ?? throw new System.ArgumentNullException(nameof(source));

      var sourcePosition = bytesToRemove;
      var targetPosition = 0;

      var bytesRemaining = (int)(source.Length - sourcePosition);

      var bufferSize = System.Math.Min(bytesRemaining, 1024 * 1024);

      var buffer = new byte[bufferSize];

      while (bytesRemaining > 0)
      {
        var bytesToRead = System.Math.Min(bufferSize, bytesRemaining);

        fs.Position = sourcePosition;
        fs.Read(buffer, 0, bytesToRead);

        fs.Position = targetPosition;
        fs.Write(buffer, 0, bytesToRead);

        sourcePosition += bytesToRead;
        targetPosition += bytesToRead;

        bytesRemaining -= bytesToRead;
      }

      fs.SetLength(targetPosition);
    }

    [System.CLSCompliant(false)]
    public static Microsoft.Extensions.Logging.ILoggerFactory AddFileLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, FileLoggerConfiguration config)
    {
      loggerFactory.AddProvider(new FileLoggerProvider(config));
      return loggerFactory;
    }
    //public static Microsoft.Extensions.Logging.ILoggerFactory AddFileLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
    //{
    //  var config = new FileLoggerConfiguration();
    //  return loggerFactory.AddFileLogger(config);
    //}
    //public static Microsoft.Extensions.Logging.ILoggerFactory AddFileLogger(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory, System.Action<FileLoggerConfiguration> configure)
    //{
    //  var config = new FileLoggerConfiguration();
    //  configure(config);
    //  return loggerFactory.AddFileLogger(config);
    //}
  }

  public class FileLoggerConfiguration
  {
    public System.IO.DirectoryInfo LogDirectory { get; set; }

    [System.CLSCompliant(false)]
    public System.Collections.Generic.List<Microsoft.Extensions.Logging.LogLevel> LogLevel { get; set; } = new System.Collections.Generic.List<Microsoft.Extensions.Logging.LogLevel>();

    private double m_shrinkPercent = 0.11;
    public double ShrinkPercent { get => m_shrinkPercent; set => m_shrinkPercent = value > 0 && value < 1 ? value : throw new System.ArgumentOutOfRangeException(nameof(value), @"Percentage must be within the range [0, 1] (exclusive)."); }

    private long m_shrinkThreshold = 128 * 1024;
    public long ShrinkThreshold { get => m_shrinkThreshold; set => m_shrinkThreshold = value > 0 ? value : throw new System.ArgumentOutOfRangeException(nameof(value), @"Threshold must be greater than zero."); }

    public string LogicalName { get; set; } = "[LogicalName]";

    public FileLoggerConfiguration(string logDirectory)
    {
      LogDirectory = System.IO.Directory.CreateDirectory(logDirectory);
    }
  }

  public class FileLogger : Microsoft.Extensions.Logging.ILogger
  {
    private readonly string m_name;
    private readonly FileLoggerConfiguration m_config;

    private readonly System.IO.FileInfo m_logFile;

    public FileLogger(string name, FileLoggerConfiguration config)
    {
      m_name = name;
      m_config = config;

      m_logFile = GetFileInfo(System.IO.Path.Combine(m_config.LogDirectory.FullName, m_name + (m_config.LogicalName.Length > 0 ? '.' + m_config.LogicalName : string.Empty) + @".log"));
    }

    public System.IDisposable BeginScope<TState>(TState state) => null!;

    [System.CLSCompliant(false)]
    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => m_config is null || m_config.LogLevel is null || m_config.LogLevel.Count == 0 || m_config.LogLevel.Contains(logLevel);

    [System.CLSCompliant(false)]
    public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, System.Exception exception, System.Func<TState, System.Exception, string> formatter)
    {
      if (IsEnabled(logLevel))
      {
        m_logFile.Refresh();

        if (m_logFile.Length > m_config.ShrinkThreshold)
        {
          m_logFile.ShrinkBeginning(m_config.ShrinkPercent);
        }

        System.IO.File.AppendAllText(m_logFile.FullName, $"\"LogLevel = {logLevel}\", Event(Id = {eventId.Id}, Name = \"{eventId.Name}\"), Name = \"{m_name}\", SubName = \"{m_config.LogicalName}\", Exception = \"{formatter(state, exception)}\"{System.Environment.NewLine}");
      }
    }

    public static System.IO.FileInfo GetFileInfo(string filePath)
    {
      if (!System.IO.File.Exists(filePath)) System.IO.File.CreateText(filePath).Close();

      return new System.IO.FileInfo(filePath);
    }
  }

  public class FileLoggerProvider : Microsoft.Extensions.Logging.ILoggerProvider
  {
    private readonly FileLoggerConfiguration m_config;
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, FileLogger> m_loggers = new System.Collections.Concurrent.ConcurrentDictionary<string, FileLogger>();

    public FileLoggerProvider(FileLoggerConfiguration config) => m_config = config;

    [System.CLSCompliant(false)]
    public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName) => m_loggers.GetOrAdd(categoryName, name => new FileLogger(name, m_config));

    public void Dispose() => m_loggers.Clear();
  }
}
#endif
