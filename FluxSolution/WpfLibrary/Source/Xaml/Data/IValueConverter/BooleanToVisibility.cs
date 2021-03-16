namespace Flux.Wpf.IValueConverter
{
	/// <summary>Conversion between boolean false/true and Collapsed/Visible.</summary>
	public class BooleanToVisibility : ValueConverter
	{
		public bool BooleanInverse { get; set; }

		public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return UniversalConversion(value, targetType, parameter, culture, nameof(Convert));
		}
		public override object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return UniversalConversion(value, targetType, parameter, culture, nameof(ConvertBack));
		}

		private object UniversalConversion(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture, string caller)
		{
			if (value is System.Boolean boolean && targetType == typeof(System.Windows.Visibility))
				switch (BooleanInverse ^ boolean)
				{
					case true:
						return System.Windows.Visibility.Visible;
					case false:
						return System.Windows.Visibility.Collapsed;
				}

			if (value is System.Windows.Visibility visibility && targetType == typeof(System.Boolean))
				switch (visibility)
				{
					case System.Windows.Visibility.Collapsed:
						return BooleanInverse ^ false;
					case System.Windows.Visibility.Visible:
						return BooleanInverse ^ true;
				}

			throw new System.NotSupportedException(string.Format("{0} from type '{1}' to type '{2}'.'.", caller, value.GetType().FullName, targetType.FullName));
		}
	}
}
