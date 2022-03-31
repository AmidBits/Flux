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
      WriteColor(value, foreground, background);

      System.Console.WriteLine();
    }

    public static void WriteError(string value)
      => WriteColor(value, System.ConsoleColor.Red, System.ConsoleColor.Black);
    public static void WriteErrorLine(string value)
      => WriteColorLine(value, System.ConsoleColor.Red, System.ConsoleColor.Black);
    public static void WriteInformation(string value)
      => WriteColor(value, System.ConsoleColor.Blue, System.ConsoleColor.Black);
    public static void WriteInformationLine(string value)
      => WriteColorLine(value, System.ConsoleColor.Blue, System.ConsoleColor.Black);
    public static void WriteSuccess(string value)
      => WriteColor(value, System.ConsoleColor.Green, System.ConsoleColor.Black);
    public static void WriteSuccessLine(string value)
      => WriteColorLine(value, System.ConsoleColor.Green, System.ConsoleColor.Black);
    public static void WriteWarning(string value)
      => WriteColor(value, System.ConsoleColor.Yellow, System.ConsoleColor.Black);
    public static void WriteWarningLine(string value)
      => WriteColorLine(value, System.ConsoleColor.Yellow, System.ConsoleColor.Black);

    //public static void Write(string value, int? x, int? y = null, System.ConsoleColor? color = null)
    //{
    //  if (color.HasValue)
    //    System.Console.ForegroundColor = color.Value;

    //  if (x.HasValue || y.HasValue)
    //  {
    //    var (left, top) = System.Console.GetCursorPosition();

    //    if (x.HasValue)
    //      left = x.Value;
    //    if (y.HasValue)
    //      top = y.Value;

    //    System.Console.SetCursorPosition(left, top);
    //  }

    //  System.Console.Write(value);

    //  if (color.HasValue)
    //    System.Console.ResetColor();
    //}
    //public static void WriteLine(string value, int? x = null, int? y = null, System.ConsoleColor? color = null)
    //{
    //  Write(value, x, y, color);

    //  System.Console.WriteLine();
    //}

    public static (int minLeft, int minTop, int maxLeft, int maxTop) WriteToConsole(this System.Collections.Generic.IEnumerable<string> source, int left, int top)
    {
      var index = 0;

      var maxLeft = 0;
      var maxTop = 0;

      foreach (var item in source)
      {
        maxLeft = System.Math.Max(maxLeft, left + item.Length);
        maxTop = System.Math.Max(maxTop, top + index);

        System.Console.SetCursorPosition(left, top + index++);
        System.Console.Write(item);
      }

      System.Console.WriteLine();

      return (left, top, maxLeft - 1, maxTop); // Compensate for maxLeft because it represents the length, not index, like maxTop is.
    }
    public static (int minLeft, int minTop, int maxLeft, int maxTop) WriteToConsole(this System.Collections.Generic.IEnumerable<string> source)
    {
      var (left, top) = System.Console.GetCursorPosition();

      return WriteToConsole(source, left, top);
    }
  }
}
