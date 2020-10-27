namespace Flux.User32
{
  internal class NativeMethods
  {
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern bool GetCursorPos(out Geometry.Point2 lpPoint);
  }
}
