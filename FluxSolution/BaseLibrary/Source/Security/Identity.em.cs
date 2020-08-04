using System.Linq;

namespace Flux
{
  public static partial class Security
  {
    public static System.Xml.Linq.XElement IdentityXml(this System.Security.Principal.IIdentity source)
    {
      var xe = new System.Xml.Linq.XElement(nameof(System.Security.Principal.IIdentity));

      if (source is null)
      {
        xe.Add(new System.Xml.Linq.XAttribute(@"IsNull", true.ToString()));
      }
      else
      {
        xe.SetAttributeValue(nameof(System.Type), source.GetType().Name);

        xe.SetAttributeValue(nameof(source.AuthenticationType), source.AuthenticationType);
        xe.SetAttributeValue(nameof(source.IsAuthenticated), source.IsAuthenticated.ToString());
        xe.SetAttributeValue(nameof(source.Name), source.Name);

        if (source is System.Security.Claims.ClaimsIdentity claimsIdentity) xe.Add(IdentityXml(claimsIdentity));
      }

      return xe;
    }
    public static System.Xml.Linq.XElement IdentityXml(this System.Security.Claims.ClaimsIdentity source)
    {
      var xe = new System.Xml.Linq.XElement(nameof(System.Security.Claims.ClaimsIdentity));

      if (source is null)
      {
        xe.Add(new System.Xml.Linq.XAttribute(@"IsNull", true.ToString()));
      }
      else
      {
        xe.SetAttributeValue(nameof(System.Type), source.GetType().Name);

        if (source.Actor != null) xe.Add(new System.Xml.Linq.XElement(nameof(source.Actor), IdentityXml(source.Actor)));
        xe.SetAttributeValue(nameof(source.AuthenticationType), source.AuthenticationType);
        xe.SetAttributeValue(nameof(source.IsAuthenticated), source.IsAuthenticated.ToString());
        xe.SetAttributeValue(nameof(source.Label), source.Label);
        xe.SetAttributeValue(nameof(source.Name), source.Name);
        xe.SetAttributeValue(nameof(source.NameClaimType), source.NameClaimType);
        xe.SetAttributeValue(nameof(source.RoleClaimType), source.RoleClaimType);

        xe.Add(new System.Xml.Linq.XElement(nameof(source.Claims), source.Claims.Select(c => ClaimXml(c))));
      }

      return xe;
    }
    public static System.Xml.Linq.XElement IdentitiesXml(this System.Collections.Generic.IEnumerable<System.Security.Claims.ClaimsIdentity> source)
      => new System.Xml.Linq.XElement(@"Identities", (source ?? throw new System.ArgumentNullException(nameof(source))).Select(i => IdentityXml(i)));
  }
}
