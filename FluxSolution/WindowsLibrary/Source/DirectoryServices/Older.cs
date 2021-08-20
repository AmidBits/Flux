#if DS

public static class Test
{
  public static System.Collections.Generic.List<object> GetPropertyValues(this System.DirectoryServices.ResultPropertyCollection rpc, string propertyName)
  {
    var list = new System.Collections.Generic.List<object>();
    var propertyValues = rpc[propertyName];
    for (var index = 0; index < propertyValues.Count; index++)
      list.Add(propertyValues[index]);
    return list;
  }

    public static System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<object>> GetProperties(this System.DirectoryServices.SearchResult sr)
    {
      var dictionary = new System.Collections.Generic.SortedDictionary<string, System.Collections.Generic.IList<object>>();

      foreach (var propertyName in sr.Properties.PropertyNames.Cast<string>())
      {
        var propertyValues = sr.Properties.GetPropertyValues(propertyName);

        dictionary.Add(propertyName, propertyValues);

        if (propertyValues.Count == 1)
        {
          var propertyValue0 = dictionary[propertyName][0];

          if (propertyValue0 is byte[] byteArray)
          {
            dictionary[propertyName].Add($"0x{string.Join(string.Empty, (byteArray).Select(b => b.ToString(@"X2")))}");

            if (propertyName.EndsWith("Guid", System.StringComparison.InvariantCultureIgnoreCase)) // Includes "objectGuid".
              dictionary[propertyName].Add(new System.Guid((byte[])propertyValue0));
            else if (new string[] { @"objectSid" }.Contains(propertyName, System.StringComparer.InvariantCultureIgnoreCase) || propertyName.EndsWith(@"SecurityDescriptor", System.StringComparison.InvariantCultureIgnoreCase))
              dictionary[propertyName].Add(new System.Security.Principal.SecurityIdentifier((byte[])propertyValue0, 0));
          }

          if (new string[] { @"accountExpires", @"badPasswordTime", @"lastLogoff", @"lastLogon", @"lastLogonTimestamp", @"lockoutTime", @"pwdLastSet", @"usnChanged", @"usnCreated" }.Contains(propertyName, System.StringComparer.InvariantCultureIgnoreCase))
            if (propertyValue0 is long longValue && longValue != 0 && longValue != System.Int64.MaxValue)
              dictionary[propertyName].Add(System.DateTime.FromFileTimeUtc(longValue));

          if (propertyName.Equals(@"groupType", System.StringComparison.OrdinalIgnoreCase))
            dictionary[propertyName].Add(string.Join('|', DirectoryServices.GroupTypeList((int)propertyValue0)));

          if (propertyName.Equals(@"sAMAccountType", System.StringComparison.OrdinalIgnoreCase))
            dictionary[propertyName].Add(string.Join('|', DirectoryServices.SamAccountTypeList((int)propertyValue0)));

          if (propertyName.Equals(@"userAccountControl", System.StringComparison.OrdinalIgnoreCase))
            dictionary[propertyName].Add(string.Join('|', DirectoryServices.UserAccountControlList((int)propertyValue0)));
        }
      }

      return dictionary;
    }
}

#region DS
public static class DirectoryServices
{
  public const string LDAP_MATCHING_RULE_BIT_AND = @":1.2.840.113556.1.4.803:=";
  public const string LDAP_MATCHING_RULE_BIT_OR = @":1.2.840.113556.1.4.804:=";
  public const string LDAP_MATCHING_RULE_IN_CHAIN = @":1.2.840.113556.1.4.1941:=";

  [System.Flags]
  public enum GroupTypeEnum
  {
    GLOBAL_GROUP = 0x00000002,
    DOMAIN_LOCAL_GROUP = 0x00000004,
    UNIVERSAL_GROUP = 0x00000008,
    SECURITY_ENABLED = unchecked((int)0x80000000),
    /// <summary>This is the INVERTED bit of a SECURITY_ENABLED group, but added to "complete" the set. This "flag" is implied when the SECURITY_ENABLED (above) bit is NOT set, and cannot be used in the typical bitwise sense.</summary>
    DISTRIBUTION_GROUP = unchecked((int)0x7FFFFFFF)
  }
  /// <summary>Returns a concatenated string based on groupType bits.</summary>
  /// <see cref="https://msdn.microsoft.com/en-us/library/ms675935(v=vs.85).aspx"/>
  public static System.Collections.Generic.List<string> GroupTypeList(int groupType)
  {
    var list = new System.Collections.Generic.List<string>() { @"0x" + groupType.ToString(@"X8") };

    foreach (int value in System.Enum.GetValues(typeof(GroupTypeEnum)))
    {
      if ((groupType & value) == value)
      {
        list.Add(((GroupTypeEnum)value).ToString());
      }
    }

    if (!list.Contains(nameof(GroupTypeEnum.SECURITY_ENABLED)))
    {
      list.Add(nameof(GroupTypeEnum.DISTRIBUTION_GROUP));
    }

    list.Sort();

    return list;
  }

  public enum SamAccountTypeEnum
  {
    DOMAIN_OBJECT = 0x00000000,
    GROUP_OBJECT = 0x10000000,
    NON_SECURITY_GROUP_OBJECT = 0x10000001,
    ALIAS_OBJECT = 0x20000000,
    NON_SECURITY_ALIAS_OBJECT = 0x20000001,
    USER_OBJECT = 0x30000000,
    MACHINE_ACCOUNT = 0x30000001,
    TRUST_ACCOUNT = 0x30000002,
    APP_BASIC_GROUP = 0x40000000,
    APP_QUERY_GROUP = 0x40000001,
    ACCOUNT_TYPE_MAX = 0x7FFFFFFF
  }

  public static System.Collections.Generic.List<string> SamAccountTypeList(int sAMAccountType)
  {
    var list = new System.Collections.Generic.List<string>() { @"0x" + sAMAccountType.ToString(@"X8") };

    foreach (int value in System.Enum.GetValues(typeof(SamAccountTypeEnum)))
    {
      if ((sAMAccountType & value) == value)
      {
        list.Add(((SamAccountTypeEnum)value).ToString());
        break;
      }
    }

    list.Sort();

    return list;
  }

  /// <summary>Returns a string based on sAMAccountType.</summary>
  /// <see cref="https://msdn.microsoft.com/en-us/library/ms679637(v=vs.85).aspx"/>
  public static string SamAccountTypeString(int sAMAccountType)
  {
    return @"0x" + sAMAccountType.ToString(@"X8") + '|' + sAMAccountType switch
    {
      0x00000000 => "DOMAIN_OBJECT",
      0x10000000 => "GROUP_OBJECT",
      0x10000001 => "NON_SECURITY_GROUP_OBJECT",
      0x20000000 => "ALIAS_OBJECT",
      0x20000001 => "NON_SECURITY_ALIAS_OBJECT",
      0x30000000 => "USER_OBJECT",
      0x30000001 => "MACHINE_ACCOUNT",
      0x30000002 => "TRUST_ACCOUNT",
      0x40000000 => "APP_BASIC_GROUP",
      0x40000001 => "APP_QUERY_GROUP",
      0x7FFFFFFF => "ACCOUNT_TYPE_MAX",
      _ => @"Unrecognized sAMAccountType"
    };
  }

  [System.Flags]
  public enum UserAccountControlEnum
  {
    SCRIPT = 0x00000001,
    ACCOUNTDISABLE = 0x00000002,
    HOMEDIR_REQUIRED = 0x00000008,
    LOCKOUT = 0x00000010,
    PASSWD_NOTREQD = 0x00000020,
    PASSWD_CANT_CHANGE = 0x00000040,
    ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x00000080,
    TEMP_DUPLICATE_ACCOUNT = 0x00000100,
    NORMAL_ACCOUNT = 0x00000200,
    INTERDOMAIN_TRUST_ACCOUNT = 0x00000800,
    WORKSTATION_TRUST_ACCOUNT = 0x00001000,
    SERVER_TRUST_ACCOUNT = 0x00002000,
    DONT_EXPIRE_PASSWD = 0x00010000,
    MNS_LOGON_ACCOUNT = 0x00020000,
    SMARTCARD_REQUIRED = 0x00040000,
    TRUSTED_FOR_DELEGATION = 0x00080000,
    NOT_DELEGATED = 0x00100000,
    USE_DES_KEY_ONLY = 0x00200000,
    DONT_REQUIRE_PREAUTH = 0x00400000,
    PASSWORD_EXPIRED = 0x00800000,
    TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0x01000000,
    PARTIAL_SECRETS_ACCOUNT = 0x04000000
  }

  /// <summary>Returns a concatenated string based on userAccountControl bits.</summary>
  /// <see cref="https://msdn.microsoft.com/en-us/library/ms680832(v=vs.85).aspx"/>
  public static System.Collections.Generic.List<string> UserAccountControlList(int userAccountControl)
  {
    var list = new System.Collections.Generic.List<string>() { @"0x" + userAccountControl.ToString(@"X8") };

    foreach (int value in System.Enum.GetValues(typeof(UserAccountControlEnum)))
    {
      if ((userAccountControl & value) == value)
      {
        list.Add(((UserAccountControlEnum)value).ToString());
      }
    }

    list.Sort();

    return list;
  }

  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAll(string filter, int pageSize = 100, int sizeLimit = 1000, params string[] properties)
  {
    if (properties is null || properties.Length == 0) properties = new string[] { @"*" };

    using var directoryEntry = new System.DirectoryServices.DirectoryEntry(@"LDAP://RootDSE");

    var path = $"LDAP://{directoryEntry.Properties[@"defaultNamingContext"][0]}";

    using var directoryEntryPath = new System.DirectoryServices.DirectoryEntry(path);
    using System.DirectoryServices.DirectorySearcher directorySearcher = new System.DirectoryServices.DirectorySearcher(directoryEntryPath);

    directorySearcher.Filter = filter;
    for (var index = 0; index < properties.Length; index++) directorySearcher.PropertiesToLoad.Add(properties[index]);
    directorySearcher.PageSize = pageSize;
    directorySearcher.SizeLimit = sizeLimit;

    using System.DirectoryServices.SearchResultCollection searchResultCollection = directorySearcher.FindAll();

    foreach (var searchResult in searchResultCollection.Cast<System.DirectoryServices.SearchResult>())
    {
      yield return searchResult;
    }
  }

  public static System.DirectoryServices.SearchResult GetOne(string filter, params string[] properties)
  {
    if (properties is null || properties.Length == 0) properties = new string[] { @"*" };

    using var directoryEntry = new System.DirectoryServices.DirectoryEntry(@"LDAP://RootDSE");

    var path = $"LDAP://{directoryEntry.Properties[@"defaultNamingContext"][0]}";

    using var directoryEntryPath = new System.DirectoryServices.DirectoryEntry(path);
    using System.DirectoryServices.DirectorySearcher directorySearcher = new System.DirectoryServices.DirectorySearcher(directoryEntryPath);

    directorySearcher.Filter = filter;
    for (var index = 0; index < properties.Length; index++) directorySearcher.PropertiesToLoad.Add(properties[index]);

    return directorySearcher.FindOne();
  }

  public const string UAC_Disabled = @"(useraccountcontrol:1.2.840.113556.1.4.803:=2)";

  public static string GetQueryByFlag(System.Text.RegularExpressions.Match match)
  {
    switch (match.Groups[@"AttributeName"].Value)
    {
      case @"userAccountControl":
        return $"({(match.Groups[@"Negate"].Value)}{match.Groups[@"AttributeName"].Value}:1.2.840.113556.1.4.803:={(int)System.Enum.Parse(typeof(UserAccountControlEnum), match.Groups[@"FlagName"].Value)})";
      default:
        return string.Empty;
    }
  }
  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllByFlag(System.Text.RegularExpressions.Match match)
  {
    return GetAll($"(&{GetQueryByFlag(match)})");
  }

  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllBy(string flag, bool state) => GetAll($"(&({(state ? string.Empty : @"!")}useraccountcontrol:1.2.840.113556.1.4.803:={(int)System.Enum.Parse(typeof(UserAccountControlEnum), flag)}))");

  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllByDistinguishedNameOfComputer(string computerDN) => GetAll($"(&(objectCategory=computer)(|(distinguishedName={computerDN})))");
  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllByDistinguishedNameOfGroup(string groupDN) => GetAll($"(&(objectCategory=group)(|(distinguishedName={groupDN})(member={groupDN})(memberOf={groupDN})))");
  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllByDistinguishedNameOfUser(string userDN) => GetAll($"(|(&(objectCategory=person)(objectClass=user)(|(distinguishedName={userDN})(manager={userDN})))(&(objectCategory=group)(|(managedBy={userDN})(member={userDN}))))");

  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllEmptyGroups() => GetAll(@"(&(objectCategory=group)(!member=*))");
  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllManagedGroups() => GetAll(@"(&(objectCategory=group)(managedBy=*))");
  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllNestedGroups() => GetAll(@"(&(objectCategory=group)(memberOf=*))");

  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllLockedUsers() => GetAll(@"(&(objectCategory=person)(objectClass=user)(lockoutTime>=1))");
  public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAllDisabledUsers() => GetAll(@"(&(objectCategory=person)(objectClass=user)(useraccountcontrol:1.2.840.113556.1.4.803:=2))");

  public static System.DirectoryServices.SearchResult GetOneByDistinguishedName(string distinguishedName) => GetOne($"(&(distinguishedName={distinguishedName}))");
}
#endregion DS

     var myDN = DirectoryServices.GetOne($"(&(objectCategory=person)(objectClass=user)(sAMAccountName=u131621))").Properties[@"distinguishedName"][0].ToString();
      System.Console.WriteLine(myDN);

      var propertyNames = new string[] { "*" };

      var sr = DirectoryServices.GetOne($"(&(objectCategory=person)(objectClass=user)(sAMAccountName=u131621))", propertyNames);

      var myProperties = sr.GetProperties();

#endif
