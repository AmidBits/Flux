namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Converts the DataTable into XML in the form of an XDocument.</summary>
    public static System.Xml.Linq.XDocument ToXDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.Linq.XDocument();

      using (var xw = xd.CreateWriter())
      {
        source.WriteXml(xw);
      }

      return xd;
    }
  }
}
