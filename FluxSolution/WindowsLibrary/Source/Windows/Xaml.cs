namespace Flux.Wpf
{
  public static partial class Windows
  {
    /// <summary>Parse xaml markup path data to a Path.</summary>
    /// <example>
    /// Sample: var path = MarkupToPath("<GeometryGroup><EllipseGeometry Center='50,50' RadiusX='50' RadiusY='50'/><RectangleGeometry Rect='0,0,100,100'/></GeometryGroup>");
    /// </example>
    /// https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.shapes.path.aspx
    public static System.Windows.Shapes.Path MarkupToPath(string pathData)
    {
      var xaml = "<Path xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><Path.Data>" + pathData + "</Path.Data></Path>";

      using var xr = System.Xml.XmlReader.Create(xaml);

      return (System.Windows.Shapes.Path)System.Windows.Markup.XamlReader.Load(xr);
    }
    /// <summary>Parse xaml markup path data to a Geometry.</summary>
    /// <example>
    /// Sample: var geometry = MarkupToPathGeometry("<GeometryGroup><EllipseGeometry Center='50,50' RadiusX='50' RadiusY='50'/><RectangleGeometry Rect='0,0,100,100'/></GeometryGroup>");
    /// </example>
    /// https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.shapes.path.data.aspx
    public static System.Windows.Media.Geometry MarkupToPathGeometry(string pathData)
    {
      var path = MarkupToPath(pathData);

      var geometry = path.Data;
      path.Data = null;
      return geometry;
    }
  }
}
