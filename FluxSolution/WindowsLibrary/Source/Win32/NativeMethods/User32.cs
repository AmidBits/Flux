namespace Flux.Win32.User32
{
  public enum GmmpResolution
  {
    GmmpUseDisplayPoints = 1,
    GmmpUseHighResolutionPoints = 2
  }

  [System.Flags]
  public enum MouseEventFlags
    : int
  {
    /// <summary>Movement occurred.</summary>
    Move = 0x0001,
    /// <summary>The left button is down.</summary>
    LeftDown = 0x0002,
    /// <summary>The left button is up.</summary>
    LeftUp = 0x0004,
    /// <summary>The right button is down.</summary>
    RightDown = 0x0008,
    /// <summary>The right button is up.</summary>
    RightUp = 0x0010,
    /// <summary>The middle button is down.</summary>
    MiddleDown = 0x0020,
    /// <summary>The middle button is up.</summary>
    MiddleUp = 0x0040,
    /// <summary>An X button was pressed.</summary>
    XDown = 0x0080,
    /// <summary>An X button was released.</summary>
    XUp = 0x0100,
    /// <summary>The wheel button is rotated.</summary>
    Wheel = 0x0800,
    /// <summary>The wheel button is tilted.</summary>
    HWheel = 0x1000,
    /// <summary>The dx and dy parameters contain normalized absolute coordinates. If not set, those parameters contain relative data: the change in position since the last reported position. This flag can be set, or not set, regardless of what kind of mouse or mouse-like device, if any, is connected to the system.</summary>
    Absolute = 0x8000
  }

  // https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mousemovepoint
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public struct MouseMovePoint
    : System.IEquatable<MouseMovePoint>
  {
    private int m_x;
    private int m_y;
    private uint m_time;
    private System.UIntPtr m_dwExtraInfo;

    public int X { get => m_x; set => m_x = value; }
    public int Y { get => m_y; set => m_y = value; }
    public int Time { get => unchecked((int)m_time); set => m_time = unchecked((uint)value); }
    [System.CLSCompliant(false)] public System.UIntPtr ExtraInfo { get => m_dwExtraInfo; set => m_dwExtraInfo = value; }

    // Operators
    public static bool operator ==(MouseMovePoint a, MouseMovePoint b)
      => a.Equals(b);
    public static bool operator !=(MouseMovePoint a, MouseMovePoint b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] MouseMovePoint other)
      => X == other.X && Y == other.Y && Time == other.Time && ExtraInfo == other.ExtraInfo;

    // Object overrides
    public override bool Equals(object? obj)
      => obj is MouseMovePoint o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(X, Y, Time, ExtraInfo);
    public override string ToString()
      => $"<{X}, {Y}>";
  }

  public static class NativeMethods
  {
    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool GetCursorPos(out CartesianCoordinateI2 lpPoint);
    /// <summary>Retrieves the position of the mouse cursor, in screen coordinates.</summary>
    public static bool TryGetCursorPos(out CartesianCoordinateI2 point)
      => GetCursorPos(out point);

    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setcursorpos
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);
    /// <summary>Moves the cursor to the specified screen coordinates. If the new coordinates are not within the screen rectangle set by the most recent ClipCursor function call, the system automatically adjusts the coordinates so that the cursor stays within the rectangle./summary>
    public static bool TrySetCursorPos(CartesianCoordinateI2 point)
      => SetCursorPos(point.X, point.Y);

    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmousemovepointsex
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int GetMouseMovePointsEx(uint cbSize, [System.Runtime.InteropServices.In] ref MouseMovePoint lppt, [System.Runtime.InteropServices.Out] MouseMovePoint[] lpptBuf, int nBufPoints, uint resolution);
    /// <summary>Retrieves a history of up to 64 previous coordinates of the mouse or pen.</summary>
    public static MouseMovePoint[] GetMouseMovePoints(GmmpResolution resolution, int numberOfPointsToRetrieve = 16)
    {
      var mp_in = new MouseMovePoint();
      var mp_out = new MouseMovePoint[numberOfPointsToRetrieve];

      var count = GetMouseMovePointsEx((uint)System.Runtime.InteropServices.Marshal.SizeOf(mp_in), ref mp_in, mp_out, numberOfPointsToRetrieve, (uint)resolution);

      var mp = new MouseMovePoint[count];

      System.Array.Copy(mp_out, mp, mp.Length);

      return mp;
    }

    public static int MouseEventState { get; set; }

    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern void mouse_event(int flags, int dx, int dy, uint data, System.UIntPtr extraInfo);
    /// <summary>Synthesize mouse motion and button clicks.</summary>
    public static void InvokeMouseEvent(MouseEventFlags flags)
      => InvokeMouseEvent(flags, 0, 0, 0, System.UIntPtr.Zero);
    /// <summary>Synthesize mouse motion and button clicks.</summary>
    public static void InvokeMouseEvent(MouseEventFlags flags, int x = 0, int y = 0)
      => InvokeMouseEvent(flags, x, y, 0, System.UIntPtr.Zero);
    /// <summary>Synthesize mouse motion and button clicks.</summary>
    public static void InvokeMouseEvent(MouseEventFlags flags, int x = 0, int y = 0, int data = 0)
      => InvokeMouseEvent(flags, x, y, data, System.UIntPtr.Zero);
    /// <summary>Synthesize mouse motion and button clicks.</summary>
    [System.CLSCompliant(false)]
    public static void InvokeMouseEvent(MouseEventFlags flags, int x, int y, int data, System.UIntPtr extraInfo)
    {
      MouseEventState ^= (int)flags;

      mouse_event((int)flags, x, y, unchecked((uint)data), extraInfo);
    }
  }
}
