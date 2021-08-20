namespace Flux.Wpf.IValueConverter
{
  public class ValueConverter 
    : System.Windows.DependencyObject, System.Windows.Data.IValueConverter
  {
    public virtual object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new System.NotSupportedException(string.Format("Convert from type '{0}' to type '{1}'.", value.GetType().Name, targetType.Name));
    }

    public virtual object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new System.NotSupportedException(string.Format("ConvertBack from type '{0}' to type '{1}'.", value.GetType().Name, targetType.Name));
    }
  }
}
