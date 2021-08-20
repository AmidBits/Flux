namespace Flux.Wpf
{
  public static partial class Media
  {
    /// <summary></summary>
    /// <seealso cref="https://stackoverflow.com/questions/13079610/equivalent-of-glortho-in-wpf/13093518#13093518"/>
    public static System.Windows.Media.TransformGroup GetCoordinateSystemTransform(double minimumX, double minimumY, double maximumX, double maximumY, double actualWidthOfControl, double actualHeightOfControl)
    {
      var width = maximumX - minimumX;
      var height = maximumY - minimumY;

      var translateTransform = new System.Windows.Media.TranslateTransform();
      translateTransform.X = -minimumX;
      translateTransform.Y = -(height + minimumY);

      var scaleTransform = new System.Windows.Media.ScaleTransform();
      scaleTransform.ScaleX = actualWidthOfControl / width;
      scaleTransform.ScaleY = actualHeightOfControl / -height;

      var transformGroup = new System.Windows.Media.TransformGroup();

      transformGroup.Children.Add(translateTransform);
      transformGroup.Children.Add(scaleTransform);

      return transformGroup;
    }
  }
}
