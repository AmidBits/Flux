namespace Flux
{
	public static partial class Xtensions
	{
		public static System.Xml.Linq.XDocument ToXDocument(this System.Exception source, string? additionalText = null)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var xd = new System.Xml.Linq.XDocument();
			xd.Add(new System.Xml.Linq.XElement(@"Error"));
			if (!(xd.Root is null))
			{
				if (additionalText != null)
					xd.Root.Add(new System.Xml.Linq.XAttribute(@"AdditionalText", additionalText));
				xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(System.Guid), System.Guid.NewGuid().ToString()));
				xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.HResult), source.HResult.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture)));
				xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.Message), source.Message));
				if (source.Source is var sourceSource && sourceSource is not null)
					xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.Source), sourceSource));
				if (source.StackTrace is var sourceStackTrace && sourceStackTrace is not null)
					xd.Root.Add(new System.Xml.Linq.XAttribute(nameof(source.StackTrace), sourceStackTrace));
			}
			return xd;
		}

		public static System.Xml.XmlDocument ToXmlDocument(this System.Exception source, string? additionalText = null)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var xd = new System.Xml.XmlDocument();
			xd.AppendChild(xd.CreateElement(@"Error"));
			if (!(xd.DocumentElement is null))
			{
				if (additionalText != null)
					xd.DocumentElement.SetAttribute(@"AdditionalText", additionalText);
				xd.DocumentElement.SetAttribute(nameof(System.Guid), System.Guid.NewGuid().ToString());
				xd.DocumentElement.SetAttribute(nameof(source.HResult), source.HResult.ToString(@"X2", System.Globalization.CultureInfo.CurrentCulture));
				xd.DocumentElement.SetAttribute(nameof(source.Message), source.Message);
				if (source.Source is var sourceSource && sourceSource is not null)
					xd.DocumentElement.SetAttribute(nameof(source.Source), sourceSource);
				if (source.StackTrace is var sourceStackTrace && sourceStackTrace is not null)
					xd.DocumentElement.SetAttribute(nameof(source.StackTrace), sourceStackTrace);
			}
			return xd;
		}
	}
}
