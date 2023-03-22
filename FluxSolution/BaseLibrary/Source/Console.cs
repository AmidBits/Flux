namespace Flux
{
  public static class Console
  {
    public static void WriteColor(string value, System.ConsoleColor foreground, System.ConsoleColor background)
    {
      var backgroundColor = System.Console.BackgroundColor;
      var foregroundColor = System.Console.ForegroundColor;
      System.Console.BackgroundColor = background;
      System.Console.ForegroundColor = foreground;
      System.Console.Write(value);
      System.Console.BackgroundColor = backgroundColor;
      System.Console.ForegroundColor = foregroundColor;
    }
    public static void WriteColorLine(string value, System.ConsoleColor foreground, System.ConsoleColor background)
    {
      var backgroundColor = System.Console.BackgroundColor;
      var foregroundColor = System.Console.ForegroundColor;
      System.Console.BackgroundColor = background;
      System.Console.ForegroundColor = foreground;
      System.Console.WriteLine(value);
      System.Console.BackgroundColor = backgroundColor;
      System.Console.ForegroundColor = foregroundColor;
    }

    public static void WriteError(string value) => WriteColor(value, System.ConsoleColor.Red, System.ConsoleColor.Black);
    public static void WriteErrorLine(string value) => WriteColorLine(value, System.ConsoleColor.Red, System.ConsoleColor.Black);
    public static void WriteInformation(string value) => WriteColor(value, System.ConsoleColor.Blue, System.ConsoleColor.Black);
    public static void WriteInformationLine(string value) => WriteColorLine(value, System.ConsoleColor.Blue, System.ConsoleColor.Black);
    public static void WriteSuccess(string value) => WriteColor(value, System.ConsoleColor.Green, System.ConsoleColor.Black);
    public static void WriteSuccessLine(string value) => WriteColorLine(value, System.ConsoleColor.Green, System.ConsoleColor.Black);
    public static void WriteWarning(string value) => WriteColor(value, System.ConsoleColor.Yellow, System.ConsoleColor.Black);
    public static void WriteWarningLine(string value) => WriteColorLine(value, System.ConsoleColor.Yellow, System.ConsoleColor.Black);
  }
}
