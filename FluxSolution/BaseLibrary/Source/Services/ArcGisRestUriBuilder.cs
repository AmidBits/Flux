namespace Flux.Services.ArcGis
{
	/// <summary></summary>
	// https://developers.arcgis.com/rest/services-reference/get-started-with-the-services-directory.htm
	// https://developers.arcgis.com/rest/services-reference/project.htm
	public class RestUriBuilder
	{
		private readonly System.UriBuilder m_uriBuilder;

		/// <summary>This is the URI scheme, and would be either https or http. Https is the default.</summary>
		public string Scheme { get => m_uriBuilder.Scheme; set => m_uriBuilder.Scheme = value; }
		/// <summary>This is the ArcGIS Server host name.</summary>
		public string Host { get => m_uriBuilder.Host; set => m_uriBuilder.Host = value; }
		/// <summary>This is the ArcGIS Server port number.</summary>
		public int Port { get => m_uriBuilder.Port; set => m_uriBuilder.Port = value; }

		private string m_site = @"arcgis";
		/// <summary>This is the site name. The default value is "arcgis". ArcGIS Server accepts a site name specified in a URL as lowercase (arcgis) or camel case (ArcGIS). Using an all lowercase site name is recommended.</summary>
		public string Site { get => m_site; set => SetPath(value, m_folder, m_serviceName, m_serviceType); }

		private string? m_folder;
		/// <summary>A folder contains services. Folder names are case sensitive and should be specified in the case in which it was created.</summary>
		public string? Folder { get => m_folder; set => SetPath(m_site, value, m_serviceName, m_serviceType); }

		private string m_serviceName = string.Empty;
		/// <summary>This represents the name of the service (e.g. PopulationDensity). The service name is case sensitive and should be specified in the case in which it was created. </summary>
		public string ServiceName { get => m_serviceName; set => SetPath(m_site, m_folder, value, m_serviceType); }

		private string m_serviceType = string.Empty;
		/// <summary>This represents the name of the service (e.g. MapServer). The service type should always be specified in a mixed case format as defined for each service in the REST API reference (e.g. MapServer, GeocodeServer, and GPServer).</summary>
		public string ServiceType { get => m_serviceType; set => SetPath(m_site, m_folder, ServiceName, value); }

		/// <summary>This is a composite assignment method, where the four parts of the URI "path" property.</summary>
		/// <param name="site"></param>
		/// <param name="folder"></param>
		/// <param name="serviceName"></param>
		/// <param name="serviceType"></param>
		private void SetPath(string site, string? folder, string serviceName, string serviceType)
		{
			if (m_site != site)
				m_site = site;
			if (m_folder != folder)
				m_folder = folder;
			if (m_serviceName != serviceName)
				m_serviceName = serviceName;
			if (m_serviceType != serviceType)
				m_serviceType = serviceType;

			m_uriBuilder.Path = $"{site}/rest/services{(folder is not null ? $"/{folder}" : string.Empty)}/{serviceName}/{serviceType}";
		}

		public RestUriBuilder()
		{
			m_uriBuilder = new System.UriBuilder(System.Uri.UriSchemeHttps, null);
		}
		public RestUriBuilder(string host, string? folder, string serviceName, string serviceType)
			: this()
		{
			SetPath(host, folder, serviceName, serviceType);
		}

		public System.Uri Uri
			=> new System.Uri(ToString());

		public override string ToString()
			=> m_uriBuilder.ToString();
	}
}
