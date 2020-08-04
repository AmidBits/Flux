#if Microsoft_Extensions_Logging
namespace Flux
{
  // https://stackoverflow.com/questions/5646820/logger-wrapper-best-practice/5646876#5646876
  // https://asp.net-hacker.rocks/2017/05/05/add-custom-logging-in-aspnetcore.html
  // https://blog.stephencleary.com/2018/05/microsoft-extensions-logging-part-1-introduction.html

  public static class ExtensionMethodsILogger
  {
    public static void Log(this ILogger logger, LogType type, string text, System.Exception? exception = null)
      => logger.Log(new LogEntry(type, text ?? exception?.Message ?? throw new System.ArgumentNullException(nameof(text)), exception));

    public static LogEntry ToLogEntry(this System.Exception exception, LogType type = LogType.Error, string? text = null)
      => new LogEntry(type, text ?? exception.Message, exception);
  }

  /// <summary></summary>
  /// <see cref="https://stackoverflow.com/questions/39610056/implementation-and-usage-of-logger-wrapper-for-microsoft-extensions-logging"/>
  public interface ILogger
  {
    void Log(LogEntry entry);
  }

  public enum LogType
  {
    Trace = 0,
    Debug = 1,
    Information = 2,
    Warning = 3,
    Error = 4,
    Critical = 5
  };

  public sealed class LogEntry
  {
    public readonly LogType Type;
    public readonly string Text;
    public readonly System.Exception? Exception;

    public LogEntry(LogType type, string text, System.Exception? exception = null)
    {
      Type = type;
      Text = text ?? exception?.Message ?? throw new System.ArgumentNullException(nameof(text));
      Exception = exception;
    }

    public override string ToString()
      => $"{Type}: {Text}{(Exception is null || Exception.Message.Equals(Text) ? string.Empty : $" [{Exception.Message}]")}";
  }

  public sealed class MicrosoftExtensionsLoggingAdapter
    : ILogger
  {
    private readonly Microsoft.Extensions.Logging.ILogger m_adaptee;

    [System.CLSCompliant(false)]
    public MicrosoftExtensionsLoggingAdapter(Microsoft.Extensions.Logging.ILogger adaptee)
      => this.m_adaptee = adaptee;

    public void Log(LogEntry e)
      => m_adaptee.Log(ToLevel(e.Type), new Microsoft.Extensions.Logging.EventId(0, @"Noname"), e.Text, e.Exception, (s, _) => s);

    private static Microsoft.Extensions.Logging.LogLevel ToLevel(LogType type)
      => type switch
      {
        LogType.Trace => Microsoft.Extensions.Logging.LogLevel.Trace,
        LogType.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
        LogType.Information => Microsoft.Extensions.Logging.LogLevel.Information,
        LogType.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
        LogType.Error => Microsoft.Extensions.Logging.LogLevel.Error,
        LogType.Critical => Microsoft.Extensions.Logging.LogLevel.Critical,
        _ => throw new System.ArgumentOutOfRangeException(nameof(type))
      };
  }
}
#endif
