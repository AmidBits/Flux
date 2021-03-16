namespace Flux.Wpf.IValueConverter
{
	/// <summary>Conversion between boolean false/true and two DataTemplates.</summary>
	public class BooleanToDataTemplate : ValueConverter
	{
		public bool BooleanInverse { get; set; }

		public System.Windows.DataTemplate DataTemplateWhenFalse { get; set; }
		public System.Windows.DataTemplate DataTemplateWhenTrue { get; set; }

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
			if (value is System.Boolean boolean && targetType == typeof(System.Windows.DataTemplate))
				switch (BooleanInverse ^ boolean)
				{
					case false:
						return DataTemplateWhenFalse;
					case true:
						return DataTemplateWhenTrue;
				}

			if (value is System.Windows.DataTemplate dataTemplate && targetType == typeof(System.Boolean))
			{
				if (dataTemplate == DataTemplateWhenFalse)
					return BooleanInverse ^ false;
				if (dataTemplate == DataTemplateWhenTrue)
					return BooleanInverse ^ true;
			}

			throw new System.NotSupportedException(string.Format("{0} from type '{1}' to type '{2}'.'.", caller, value.GetType().FullName, targetType.FullName));
		}
	}
}
