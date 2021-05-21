using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
  public class MyVisualHost
    : System.Windows.FrameworkElement
  {
    // Create a collection of child visual objects.
    private readonly System.Windows.Media.VisualCollection _children;

    public MyVisualHost()
    {
      _children = new System.Windows.Media.VisualCollection(this)
            {
                CreateDrawingVisualRectangle(),
                CreateDrawingVisualText(),
                CreateDrawingVisualEllipses()
            };

      // Add the event handler for MouseLeftButtonUp.
      MouseLeftButtonUp += MyVisualHost_MouseLeftButtonUp;
    }

    // Capture the mouse event and hit test the coordinate point value against
    // the child visual objects.
    private void MyVisualHost_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      var point = e.GetPosition((System.Windows.UIElement)sender);

      System.Windows.Media.VisualTreeHelper.HitTest(
        this,
        null,
        MyCallback,
        new System.Windows.Media.PointHitTestParameters(point)
      );
    }

    // If a child visual object is hit, toggle its opacity to visually indicate a hit.
    public System.Windows.Media.HitTestResultBehavior MyCallback(System.Windows.Media.HitTestResult result)
    {
      if (result.VisualHit.GetType() == typeof(System.Windows.Media.DrawingVisual))
      {
        ((System.Windows.Media.DrawingVisual)result.VisualHit).Opacity =
            ((System.Windows.Media.DrawingVisual)result.VisualHit).Opacity == 1.0 ? 0.4 : 1.0;
      }

      // Stop the hit test enumeration of objects in the visual tree.
      return System.Windows.Media.HitTestResultBehavior.Stop;
    }

    // Create a DrawingVisual that contains a rectangle.
    private System.Windows.Media.DrawingVisual CreateDrawingVisualRectangle()//(System.Windows.Rect rect, System.Windows.Media.Brush brush)
    {
      var drawingVisual = new System.Windows.Media.DrawingVisual();
      var drawingContext = drawingVisual.RenderOpen();

      var left = 160;
      var top = 100;
      var width = 320;
      var height = 80;
      var brush = System.Windows.Media.Brushes.LightBlue;

      drawingContext.DrawRectangle(brush, null, new System.Windows.Rect(left, top, width, height));

      drawingContext.Close();

      return drawingVisual;
    }

    // Create a DrawingVisual that contains text.
    private System.Windows.Media.DrawingVisual CreateDrawingVisualText()
    {
      // Create an instance of a DrawingVisual.
      var drawingVisual = new System.Windows.Media.DrawingVisual();

      // Retrieve the DrawingContext from the DrawingVisual.
      var drawingContext = drawingVisual.RenderOpen();

      var text = "Click Me!";
      var typeFace = new System.Windows.Media.Typeface("Verdana");
      var emSize = 36;
      var left = 200;
      var top = 0;
      var brush = System.Windows.Media.Brushes.Black;

#pragma warning disable CS0618 // 'FormattedText.FormattedText(string, CultureInfo, FlowDirection, Typeface, double, Brush)' is obsolete: 'Use the PixelsPerDip override'
      // Draw a formatted text string into the DrawingContext.
      drawingContext.DrawText(
        new System.Windows.Media.FormattedText(
          text,
          System.Globalization.CultureInfo.CurrentCulture,
          System.Windows.FlowDirection.LeftToRight,
          typeFace, emSize, brush
        ),
        new System.Windows.Point(left, top)
      );
#pragma warning restore CS0618 // 'FormattedText.FormattedText(string, CultureInfo, FlowDirection, Typeface, double, Brush)' is obsolete: 'Use the PixelsPerDip override'

      drawingContext.Close();

      return drawingVisual;
    }

    // Create a DrawingVisual that contains an ellipse.
    private System.Windows.Media.DrawingVisual CreateDrawingVisualEllipses()
    {
      var drawingVisual = new System.Windows.Media.DrawingVisual();
      var drawingContext = drawingVisual.RenderOpen();

      var centerX = 130;
      var centerY = 136;
      var radiusX = 30;
      var radiusY = 30;
      var brush = System.Windows.Media.Brushes.Maroon;

      drawingContext.DrawEllipse(brush, null, new System.Windows.Point(centerX, centerY), radiusX, radiusY);

      drawingContext.Close();

      return drawingVisual;
    }

    // Provide a required override for the VisualChildrenCount property.
    protected override int VisualChildrenCount => _children.Count;

    // Provide a required override for the GetVisualChild method.
    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      if (index < 0 || index >= _children.Count)
        throw new ArgumentOutOfRangeException();

      return _children[index];
    }
  }
}
