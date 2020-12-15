#pragma warning disable CA1416 // Validate platform compatibility
using System.Linq;

namespace Wormhole
{
	public static partial class ExtensionMethods
	{
		public static string ParsedStringValue(this System.DirectoryServices.PropertyValueCollection pvc)
		{
			if (pvc.Count > 0)
			{
				return pvc.Value.ToString();
			}

			return null;
		}
	}

	public class DirectoryServices
		: Core.IDataSource
	{
		public System.Data.IDataReader Read(System.Xml.Linq.XElement source, Core.Log log)
		{
			var entity = source.Attribute("Entity").Value;
			var filter = source.Attribute("Filter").Value;
			var properties = source.Attribute("Properties").Value.Split(',');

			log.Write($"Preparing {entity}", filter);

			System.Collections.Generic.IEnumerable<object[]> e = null;

			switch (filter)
			{
				case "PresetGroupsDirectMembers":
				case "PresetGroupsNested":
					e = Preset2Steps(filter, properties);
					break;
				default:
					e = Ldap.Search(Ldap.RootDse.DefaultNamingContext, filter, properties);
					break;
			}

			return new Flux.Data.EnumerableDataReader(e, properties, System.Linq.Enumerable.Range(0, properties.Length).Select(i => typeof(object)).ToArray());
		}

		//public System.Data.IDataReader Read(System.Xml.Linq.XElement export, Wormhole.Log log)
		//{
		//  var entity = export.Attribute("Entity").Value;
		//  var filter = export.Attribute("Filter").Value;
		//  var properties = export.Attribute("Properties").Value.Split(',');
		//  var tsqlDefinition = export.Attribute("TSqlDefinition").Value.Split(',');
		//  var netTypes = tsqlDefinition.Select(s => System.Text.RegularExpressions.Regex.Match(s, @"\[(?<ColumnName>[^\[\]]+)\]\s*\[(?<DataTypeName>[^\[\]]+)\]\s*(\((?<DataTypeSize>[^\(\)]+)\))?\s*(?<Nullability>(NULL|NOT NULL))") is var m && m.Success && m.Groups.Count >= 3 && m.Groups["DataTypeName"] is var g && g.Success ? Flux.Data.Sql.DataTypeName.ToClrType(g.Value) : throw new System.Exception()).ToArray();
		//  var netTypeStrings = netTypes.Select(s => s.FullName).ToArray();

		//  writer.WriteFields(properties);
		//  writer.WriteFields(netTypeStrings);
		//  writer.WriteFields(tsqlDefinition);

		//  //log.Write($"Exporting {entity}", filter);

		//  System.Collections.Generic.IEnumerable<object[]> e = null;

		//  switch (filter)
		//  {
		//    case "PresetGroupsDirectMembers":
		//    case "PresetGroupsNested":
		//      e = Preset2Steps(filter, properties);
		//      break;
		//    default:
		//      e = Ldap.Search(Ldap.RootDse.DefaultNamingContext, filter, properties);
		//      break;
		//  }

		//  foreach (var row in e)
		//  {
		//    writer.WriteRecord(row);

		//    //if (writer.TimeToNotify)
		//    //{
		//    //  log.Write($"{writer.RecordIndex} rows exported so far.");
		//    //}
		//  }

		//  log.Write($"Export process COUNT={writer.RecordIndex}", filter, string.Join(@",", properties));
		//}

		System.Collections.Generic.IEnumerable<object[]> Preset2Steps(string preset, string[] properties)
		{
			using (var directorySearcher = new System.DirectoryServices.DirectorySearcher(Ldap.RootDse.DefaultNamingContext))
			{
				directorySearcher.PageSize = 100;

				System.Collections.Generic.IEnumerable<object[]> Query(string queryFilter, params string[] queryProperties)
				{
					directorySearcher.Filter = queryFilter;
					directorySearcher.PropertiesToLoad.Clear();
					if (queryProperties.Length > 0)
					{
						directorySearcher.PropertiesToLoad.AddRange(queryProperties);
					}

					using (var searchResultCollection = directorySearcher.FindAll())
					{
						foreach (System.DirectoryServices.SearchResult searchResult in searchResultCollection)
						{
							if (queryProperties.Length > 0)
							{
								yield return queryProperties.Select(qp => searchResult.Properties[qp][0]).ToArray();
							}
							else
							{
								yield return new object[] { searchResult.Path.Substring(7) };
							}
						}
					}
				}

				var groupsDN = Query("(&(objectCategory=group)(member=*))").Select(a => a[0].ToString()).ToArray();

				foreach (var groupDN in groupsDN)
				{
					switch (preset)
					{
						case "PresetGroupsDirectMembers":
							foreach (var fields in Query($"(&(|(objectCategory=computer)(objectCategory=group)(objectCategory=user))(memberOf={groupDN}))", "distinguishedName", "objectCategory"))
							{
								yield return new object[] { groupDN, fields[0].ToString(), fields[1].ToString() };
							}
							break;
						case "PresetGroupsNested":
							foreach (var fields in Query($"(&(objectCategory=group)(memberOf:1.2.840.113556.1.4.1941:={groupDN}))"))
							{
								yield return new object[] { groupDN, fields[0].ToString() };
							}
							break;
					}
				}
			}
		}

		public static class Ldap
		{
			public const string Prefix = @"LDAP://";

			public static class RootDse
			{
				public static object GetPropertyValue(string propertyName)
				{
					using (var rootDse = new System.DirectoryServices.DirectoryEntry($"{Prefix}RootDSE"))
					{
						return rootDse.Properties[propertyName].Value;
					}
				}

				public static string DefaultNamingContext => (string)GetPropertyValue(nameof(DefaultNamingContext));
			}

			public static System.Collections.Generic.IEnumerable<object[]> Search(string searchRoot, string filter, string[] properties)
			{
				using (var directorySearcher = new System.DirectoryServices.DirectorySearcher(searchRoot))
				{
					directorySearcher.Filter = filter;
					directorySearcher.PageSize = 500;
					directorySearcher.PropertiesToLoad.Clear();
					directorySearcher.PropertiesToLoad.AddRange(properties);

					using (var searchResultCollection = directorySearcher.FindAll())
					{
						foreach (System.DirectoryServices.SearchResult searchResult in searchResultCollection)
						{
							yield return properties.Select<string, object>((name, index) =>
							{
								if (searchResult.Properties[name] is var pvc && pvc != null && pvc.Count > 0)
								{
									if (pvc.Count > 1)
									{
										return string.Join("; ", pvc.Cast<string>());
									}
									else if (name.Contains("objectGuid")) // GUIDs are binary attributes.
									{
										return new System.Guid((byte[])pvc[0]);
									}
									else if (name.Contains("objectSid")) // SID is a binary attribute.
									{
										return new System.Security.Principal.SecurityIdentifier((byte[])(pvc[0]), 0).ToString();
									}
									else if (new string[] { "badPasswordTime", "lastLogon", "lastLogonTimestamp", "lastLogoff", "lockoutTime", "pwdLastSet" }.Contains(name)) // These are datetime fields stored in NTFS format (and are also not actual date time objects) so they need to be converted.
									{
										return (System.Convert.ToInt64(pvc[0]) is var int64 && int64 > 0 ? (System.DateTime?)System.DateTime.FromFileTime(int64) : null);
									}
									else if (pvc[0].GetType() == typeof(System.DateTime)) // Adjust all attributes of type System.DateTime from GMT (UTC) to local (-7) time.
									{
										return System.TimeZoneInfo.ConvertTimeFromUtc((System.DateTime)pvc[0], System.TimeZoneInfo.Local);
									}
									else return pvc[0]; // Return the object as is.
								}

								return null;
							}).ToArray();
						}
					}
				}
			}
		}
	}
}
#pragma warning restore CA1416 // Validate platform compatibility
