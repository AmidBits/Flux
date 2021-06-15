#if DirectoryServices
using System.Linq;

namespace Flux
{
  public static partial class DirectoryEm
  {
    public static string ToCombineOperatorString(this Directory.CombineOperator combineOperator)
    {
      switch (combineOperator)
      {
        case Directory.CombineOperator.And:
          return @"&";
        case Directory.CombineOperator.Not:
          return @"!";
        case Directory.CombineOperator.Or:
          return @"|";
        default:
          throw new System.ArgumentOutOfRangeException(nameof(combineOperator));
      }
    }

    public static string ToCompareOperatorString(this Directory.CompareOperator compareOperator)
    {
      switch (compareOperator)
      {
        case Directory.CompareOperator.Equal:
          return @"=";
        case Directory.CompareOperator.GreaterThanOrEqual:
          return @">=";
        case Directory.CompareOperator.LessThanOrEqual:
          return @"<=";
        default:
          throw new System.ArgumentOutOfRangeException(nameof(compareOperator));
      }
    }

    public static string ToMatchingRuleOperatorString(this Directory.MatchingRule matchingRule)
    {
      switch (matchingRule)
      {
        case Directory.MatchingRule.BitAnd:
          return @":1.2.840.113556.1.4.803:=";
        case Directory.MatchingRule.BitOr:
          return @":1.2.840.113556.1.4.804:=";
        case Directory.MatchingRule.InChain:
          return @":1.2.840.113556.1.4.1941:=";
        default:
          throw new System.ArgumentOutOfRangeException(nameof(matchingRule));
      }
    }
  }

  namespace Directory
  {
    public enum CombineOperator
    {
      And,
      Or,
      Not,
    }

    public enum CompareOperator
    {
      Equal,
      GreaterThanOrEqual,
      LessThanOrEqual,
    }

    public enum MatchingRule
    {
      BitAnd,
      BitOr,
      InChain,
    }

    [System.Flags]
    public enum GroupTypeEnum
    {
      SYSTEM_GROUP = 0x00000001,
      GLOBAL_GROUP = 0x00000002,
      DOMAIN_LOCAL_GROUP = 0x00000004,
      UNIVERSAL_GROUP = 0x00000008,
      APP_BASIC = 0x00000010,
      APP_QUERY = 0x00000020,
      SECURITY_ENABLED = unchecked((int)0x80000000),
      /// <summary>This is the INVERTED bit of a SECURITY_ENABLED group, but added to "complete" the set. This "flag" is implied when the SECURITY_ENABLED (above) bit is NOT set, and cannot be used in the typical bitwise sense.</summary>
      DISTRIBUTION_GROUP = 0x7FFFFFFF
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
      PARTIAL_SECRETS_ACCOUNT = 0x04000000,
      USER_USE_AES_KEYS = unchecked((int)0x80000000)
    }

    public static class Query
    {
      /// <summary>Returns a concatenated string based on groupType bits.</summary>
      /// <see cref="https://msdn.microsoft.com/en-us/library/ms675935(v=vs.85).aspx"/>
      public static System.Collections.Generic.List<string> GetGroupTypeStringList(int groupType)
      {
        var list = new System.Collections.Generic.List<string>();

        foreach (int value in System.Enum.GetValues(typeof(GroupTypeEnum)))
          if ((groupType & value) == value)
            list.Add(((GroupTypeEnum)value).ToString());

        if (!list.Contains(nameof(GroupTypeEnum.SECURITY_ENABLED)))
          list.Add(nameof(GroupTypeEnum.DISTRIBUTION_GROUP));

        list.Sort();

        return list;
      }

      public static System.Collections.Generic.List<string> GetSamAccountTypeStringList(int sAMAccountType)
      {
        var list = new System.Collections.Generic.List<string>();

        foreach (int value in System.Enum.GetValues(typeof(SamAccountTypeEnum)))
          if ((sAMAccountType & value) == value)
          {
            list.Add(((SamAccountTypeEnum)value).ToString());
            break;
          }

        list.Sort();

        return list;
      }

      /// <summary>Returns a string based on sAMAccountType.</summary>
      /// <see cref="https://msdn.microsoft.com/en-us/library/ms679637(v=vs.85).aspx"/>
      public static string GetSamAccountTypeDelimitedString(int sAMAccountType)
      {
        return sAMAccountType switch
        {
          (int)SamAccountTypeEnum.DOMAIN_OBJECT => nameof(SamAccountTypeEnum.DOMAIN_OBJECT),
          (int)SamAccountTypeEnum.GROUP_OBJECT => nameof(SamAccountTypeEnum.GROUP_OBJECT),
          (int)SamAccountTypeEnum.NON_SECURITY_GROUP_OBJECT => nameof(SamAccountTypeEnum.NON_SECURITY_GROUP_OBJECT),
          (int)SamAccountTypeEnum.ALIAS_OBJECT => nameof(SamAccountTypeEnum.ALIAS_OBJECT),
          (int)SamAccountTypeEnum.NON_SECURITY_ALIAS_OBJECT => nameof(SamAccountTypeEnum.NON_SECURITY_ALIAS_OBJECT),
          (int)SamAccountTypeEnum.USER_OBJECT => nameof(SamAccountTypeEnum.USER_OBJECT),
          (int)SamAccountTypeEnum.MACHINE_ACCOUNT => nameof(SamAccountTypeEnum.MACHINE_ACCOUNT),
          (int)SamAccountTypeEnum.TRUST_ACCOUNT => nameof(SamAccountTypeEnum.TRUST_ACCOUNT),
          (int)SamAccountTypeEnum.APP_BASIC_GROUP => nameof(SamAccountTypeEnum.APP_BASIC_GROUP),
          (int)SamAccountTypeEnum.APP_QUERY_GROUP => nameof(SamAccountTypeEnum.APP_QUERY_GROUP),
          (int)SamAccountTypeEnum.ACCOUNT_TYPE_MAX => nameof(SamAccountTypeEnum.ACCOUNT_TYPE_MAX),
          _ => @"Unrecognized sAMAccountType"
        };
      }

      /// <summary>Returns a concatenated string based on userAccountControl bits.</summary>
      /// <see cref="https://msdn.microsoft.com/en-us/library/ms680832(v=vs.85).aspx"/>
      public static System.Collections.Generic.List<string> GetUserAccountControlStringList(int userAccountControl)
      {
        var list = new System.Collections.Generic.List<string>();

        foreach (int value in System.Enum.GetValues(typeof(UserAccountControlEnum)))
          if ((userAccountControl & value) == value)
            list.Add(((UserAccountControlEnum)value).ToString());

        list.Sort();

        return list;
      }

      public static System.Collections.Generic.Dictionary<string, object[]> GetCurrentComputer()
        => GetOneEx(FilterComputer(CompareOne(System.Environment.MachineName, CompareOperator.Equal, @"cn")));

      public static System.Collections.Generic.Dictionary<string, object[]> GetCurrentUser()
        => GetOneEx(FilterUser(CompareOne(System.Environment.UserName, CompareOperator.Equal, @"sAMAccountName")));

      public static System.Collections.Generic.Dictionary<string, object[]>[] GetGroupsWithCurrentUser(bool includeNestedGroups)
      {
        var distinguishedName = GetCurrentUser().TryGetValue(@"distinguishedName", out var dn) ? dn.First().ToString() : throw new System.Exception();

        var filter = includeNestedGroups ? CompareMatchingRule(distinguishedName, MatchingRule.InChain, @"member") : CompareOne(distinguishedName, CompareOperator.Equal, @"member");

        return GetAllEx(filter, 100, 1000).ToArray();
      }

      public static System.Collections.Generic.IEnumerable<System.DirectoryServices.SearchResult> GetAll(string filter, int pageSize, int sizeLimit, params string[] properties)
      {
        if (properties is null || properties.Length == 0) properties = new string[] { @"*" };

        using var directoryEntry = new System.DirectoryServices.DirectoryEntry(@"LDAP://RootDSE");

        var path = $"LDAP://{directoryEntry.Properties[@"defaultNamingContext"][0]}";

        using var directoryEntryPath = new System.DirectoryServices.DirectoryEntry(path);
        using System.DirectoryServices.DirectorySearcher directorySearcher = new System.DirectoryServices.DirectorySearcher(directoryEntryPath);

        directorySearcher.Filter = filter;
        directorySearcher.PropertiesToLoad.AddRange(properties);
        directorySearcher.PageSize = pageSize;
        directorySearcher.SizeLimit = sizeLimit;

        using System.DirectoryServices.SearchResultCollection searchResultCollection = directorySearcher.FindAll();

        foreach (var searchResult in searchResultCollection.Cast<System.DirectoryServices.SearchResult>())
          yield return searchResult;
      }
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.Dictionary<string, object[]>> GetAllEx(string filter, int pageSize, int sizeLimit, params string[] properties)
        => GetAll(filter, pageSize, sizeLimit, properties).Select(sr => GetProperties(sr));

      public static System.DirectoryServices.SearchResult GetOne(string filter, params string[] properties)
      {
        if (properties is null || properties.Length == 0) properties = new string[] { @"*" };

        using var directoryEntry = new System.DirectoryServices.DirectoryEntry(@"LDAP://RootDSE");

        var path = $"LDAP://{directoryEntry.Properties[@"defaultNamingContext"][0]}";

        using var directoryEntryPath = new System.DirectoryServices.DirectoryEntry(path);
        using System.DirectoryServices.DirectorySearcher directorySearcher = new System.DirectoryServices.DirectorySearcher(directoryEntryPath);

        directorySearcher.Filter = filter;
        directorySearcher.PropertiesToLoad.AddRange(properties);

        return directorySearcher.FindOne();
      }
      public static System.Collections.Generic.Dictionary<string, object> m_getOneExCache = new System.Collections.Generic.Dictionary<string, object>();
      public static System.Collections.Generic.Dictionary<string, object[]> GetOneEx(string filter, params string[] properties)
      {
        var key = filter + string.Concat(properties);

        if (!m_getOneExCache.ContainsKey(key))
          m_getOneExCache.Add(key, GetProperties(GetOne(filter, properties)));

        return (System.Collections.Generic.Dictionary<string, object[]>)m_getOneExCache[key];
      }

      public static System.Collections.Generic.Dictionary<string, object[]> GetProperties(System.DirectoryServices.SearchResult searchResult)
      {
        var dictionary = new System.Collections.Generic.Dictionary<string, object[]>(System.StringComparer.OrdinalIgnoreCase);
        foreach (var propertyName in searchResult.Properties.PropertyNames.Cast<string>().OrderBy(pn => pn))
          dictionary.Add(propertyName, GetPropertyValuesExpanded(searchResult, propertyName));
        return dictionary;
      }

      public static System.Collections.Generic.List<object> GetPropertyValues(System.DirectoryServices.SearchResult searchResult, string propertyName)
        => new System.Collections.Generic.List<object>(searchResult.Properties[propertyName].Cast<object>());
      public static object[] GetPropertyValuesExpanded(System.DirectoryServices.SearchResult searchResult, string propertyName)
      {
        var propertyValues = GetPropertyValues(searchResult, propertyName);

        if (propertyValues.Count == 1)
        {
          if (propertyValues[0] is byte[] byteArray)
          {
            propertyValues.Add($"0x{string.Join(string.Empty, byteArray.Select(b => b.ToString(@"X2")))}");

            if (propertyName.EndsWith(@"Guid", System.StringComparison.InvariantCultureIgnoreCase)) // Includes "objectGuid".
              propertyValues.Add(new System.Guid(byteArray));
            else if (new string[] { @"objectSid" }.Contains(propertyName, System.StringComparer.InvariantCultureIgnoreCase) || propertyName.EndsWith(@"SecurityDescriptor", System.StringComparison.InvariantCultureIgnoreCase))
              propertyValues.Add(new System.Security.Principal.SecurityIdentifier(byteArray, 0));
          }
          else if (new string[] { @"accountExpires", @"badPasswordTime", @"lastLogoff", @"lastLogon", @"lastLogonTimestamp", @"lockoutTime", @"pwdLastSet", @"usnChanged", @"usnCreated" }.Contains(propertyName, System.StringComparer.InvariantCultureIgnoreCase))
          {
            if (propertyValues[0] is long valueI64 && valueI64 != 0 && valueI64 != System.Int64.MaxValue)
              propertyValues.Add(System.DateTime.FromFileTimeUtc(valueI64));
          }
          else if (propertyValues[0] is int valueI32)
          {
            if (propertyName.Equals(@"groupType", System.StringComparison.InvariantCultureIgnoreCase))
            {
              propertyValues.Add($"0x{valueI32.ToString(@"X8")}");
              propertyValues.Add(string.Join('|', GetGroupTypeStringList(valueI32)));
            }
            else if (propertyName.Equals(@"sAMAccountType", System.StringComparison.InvariantCultureIgnoreCase))
            {
              propertyValues.Add($"0x{valueI32.ToString(@"X8")}");
              propertyValues.Add(string.Join('|', GetSamAccountTypeStringList(valueI32)));
            }
            else if (propertyName.Equals(@"userAccountControl", System.StringComparison.InvariantCultureIgnoreCase))
            {
              propertyValues.Add($"0x{valueI32.ToString(@"X8")}");
              propertyValues.Add(string.Join('|', GetUserAccountControlStringList(valueI32)));
            }
          }
        }

        return propertyValues.ToArray();
      }

#region Query helpers
      public static string CompareAll(CombineOperator combinationOperator, string value, CompareOperator comparisonOperator, params string[] propertyNames)
        => $"({combinationOperator.ToCombineOperatorString()}{string.Concat(propertyNames.Select(pn => CompareOne(value, comparisonOperator, pn)))})";
      public static string CompareMatchingRule(string value, MatchingRule matchingRule, string propertyName)
        => $"({propertyName}{matchingRule.ToMatchingRuleOperatorString()}{value})";
      public static string CompareOne(string value, CompareOperator comparisonOperator, string propertyName)
        => $"({propertyName}{comparisonOperator.ToCompareOperatorString()}{value})";

      public static string FilterComputer(string filter)
        => $"(&(objectCategory=computer){filter})";
      public static string FilterDomain(string filter)
        => $"(&(objectCategory=domain){filter})";
      public static string FilterGroup(string filter)
        => $"(&(objectCategory=group){filter})";
      public static string FilterUser(string filter)
        => $"(&(objectCategory=user){filter})";
#endregion Query helpers
    }
  }
}
#endif
