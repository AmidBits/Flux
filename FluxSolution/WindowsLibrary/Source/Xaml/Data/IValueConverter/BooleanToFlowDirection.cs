namespace Flux.Wpf.IValueConverter
{
	/// <summary>Conversion between boolean false/true and two RightToLeft/LeftToRight.</summary>
	public class BooleanToFlowDirection : ValueConverter
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
			if (value is System.Boolean boolean && targetType == typeof(System.Windows.FlowDirection))
				switch (BooleanInverse ^ boolean)
				{
					case false:
						return System.Windows.FlowDirection.RightToLeft;
					case true:
						return System.Windows.FlowDirection.LeftToRight;
				}

			if (value is System.Windows.FlowDirection flowDirection && targetType == typeof(System.Boolean))
				switch (flowDirection)
				{
					case System.Windows.FlowDirection.LeftToRight:
						return BooleanInverse ^ true;
					case System.Windows.FlowDirection.RightToLeft:
						return BooleanInverse ^ false;
				}

			throw new System.NotSupportedException(string.Format("{0} from type '{1}' to type '{2}'.'.", caller, value.GetType().FullName, targetType.FullName));
		}
	}
}
