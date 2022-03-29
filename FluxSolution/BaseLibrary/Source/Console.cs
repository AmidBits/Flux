namespace Flux
{
  public static class Console
  {
    //public void Write(string value, System.ConsoleColor color )
    //{
    //  System.Console.ForegroundColor = color;
    //  System.Console.WriteLine(value);
    //  System.Console.ResetColor();
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
