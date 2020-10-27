namespace Flux.User32
{
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct Point
  {
    public int X;
    public int Y;

    public static implicit operator Point(Point point)
    {
      return new Point(point.X, point.Y);
    }
  }

  internal class NativeMethods
  {
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Point lpPoint);

    public static Point GetCursorPosition()
    {
      Point lpPoint;
      GetCursorPos(out lpPoint);
      // NOTE: If you need error handling
      // bool success = GetCursorPos(out lpPoint);
      // if (!success)

      return lpPoint;
    }
  }
}
