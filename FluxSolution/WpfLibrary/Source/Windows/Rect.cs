namespace Flux.Wpf
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a new point by adding the point to the original.</summary>
    public static System.Windows.Point Center(this System.Windows.Rect self)
      => new System.Windows.Point(self.CenterX(), self.CenterY());
    /// <summary>Returns the horizontal center value of the Rect.</summary>
    public static double CenterX(this System.Windows.Rect self)
      => self.Left + self.Width / 2.0;
    /// <summary>Returns the vertical center value of the Rect.</summary>
    public static double CenterY(this System.Windows.Rect self)
      => self.Top + self.Height / 2.0;
  }
}
