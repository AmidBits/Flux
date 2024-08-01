namespace Flux
{
  public static partial class Fx
  {
    public static System.Xml.Linq.XElement? Copy(this System.Xml.Linq.XElement source)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      
      var xd = new System.Xml.Linq.XDocument();

      using (var xw = xd.CreateWriter())
      {
        source.WriteTo(xw);
      }

      return xd.Root;
    }
  }
}
