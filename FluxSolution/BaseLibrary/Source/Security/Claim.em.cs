using System.Linq;

namespace Flux
{
  public static partial class Safety
  {
    public static System.Xml.Linq.XElement ClaimXml(this System.Security.Claims.Claim source)
    {
      var xe = new System.Xml.Linq.XElement(nameof(System.Security.Claims.Claim));

      if (source is null)
      {
        xe.Add(new System.Xml.Linq.XAttribute(@"IsNull", true.ToString()));
      }
      else
      {
        xe.SetAttributeValue(nameof(System.Type), source.GetType().Name);
        try { xe.SetAttributeValue(nameof(source.Issuer), source.Issuer); } catch { }
        try { xe.SetAttributeValue(nameof(source.OriginalIssuer), source.Issuer); } catch { }
//        try { xe.Add(source.Properties.ToPropertiesXml()); } catch { }
        // if (source.Subject != null) xe.Add(new System.Xml.Linq.XElement(nameof(source.Subject), IdentityXml(source.Subject))); // This call cause a stackoverflow.
        try { xe.SetAttributeValue(nameof(source.Type), source.Type); } catch { }
        try { xe.SetAttributeValue(nameof(source.Value), source.Value); } catch { }
        //try { xe.SetAttributeValue(@"NTAccount", ((System.Security.Principal.NTAccount)new System.Security.Principal.SecurityIdentifier(source.Value).Translate(typeof(System.Security.Principal.NTAccount))).Value); } catch { }
        try { xe.SetAttributeValue(nameof(source.ValueType), source.ValueType); } catch { }
      }

      return xe;
    }
    public static System.Xml.Linq.XElement ClaimsXml(this System.Collections.Generic.IEnumerable<System.Security.Claims.Claim> source)
      => new(@"Claims", (source ?? throw new System.ArgumentNullException(nameof(source))).Select(c => ClaimXml(c)));
  }
}
