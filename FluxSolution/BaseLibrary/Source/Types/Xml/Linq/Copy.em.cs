namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Xml.Linq.XElement? Copy(this System.Xml.Linq.XElement source)
    {
      var xd = new System.Xml.Linq.XDocument();

      using (var xw = xd.CreateWriter())
      {
        (source ?? throw new System.ArgumentNullException(nameof(source))).WriteTo(xw);
      }

      return xd.Root;
    }
  }
}
