namespace Flux
{
  public static partial class Fx
  {
    public static System.Xml.Linq.XElement PrincipalXml(this System.Security.Principal.IPrincipal source)
    {
      var xe = new System.Xml.Linq.XElement(nameof(System.Security.Principal.IPrincipal));

      if (source is null)
      {
        xe.Add(new System.Xml.Linq.XAttribute(@"IsNull", true.ToString()));
      }
      else
      {
        xe.SetAttributeValue(nameof(System.Type), source.GetType().Name);

        if (source.Identity is not null)
        {
          xe.Add(IdentityXml(source.Identity));
        }

        if (source is System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
          xe.Add(PrincipalXml(claimsPrincipal));
        }
      }

      return xe;
    }
    public static System.Xml.Linq.XElement PrincipalXml(this System.Security.Claims.ClaimsPrincipal source)
    {
      var xe = new System.Xml.Linq.XElement(nameof(System.Security.Claims.ClaimsPrincipal));

      if (source is null)
      {
        xe.Add(new System.Xml.Linq.XAttribute(@"IsNull", true.ToString()));
      }
      else
      {
        xe.SetAttributeValue(nameof(System.Type), source.GetType().Name);

        //xe.Add(ClaimsXml(source.Claims));
        xe.Add(IdentitiesXml(source.Identities));

        //try { xe.Add(new System.Xml.Linq.XElement(@"PrimaryIdentity", IdentityXml(source.Identity))); } catch { }
      }

      return xe;
    }
  }
}
