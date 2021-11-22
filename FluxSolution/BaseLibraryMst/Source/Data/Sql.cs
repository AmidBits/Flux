using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Data
{
  [TestClass]
  public class Sql
  {
    [TestMethod]
    public void ParseAndFormatColumnDefinition()
    {
      var columnName = "TheName";
      var dataTypeName = "nvarchar";
      var arguments = new System.Collections.Generic.List<string> { "MAX" };
      var nullability = Flux.Data.TsqlNullability.FromBoolean(true);

      var columnDefinition = new Flux.Data.TsqlColumnDefinition(columnName, dataTypeName, arguments, nullability);

      var parsedColumnDefinition = Flux.Data.TsqlColumnDefinition.Parse(columnDefinition.ToUnitString());

      Assert.AreEqual(columnName, parsedColumnDefinition.ColumnName);
      Assert.AreEqual(dataTypeName, parsedColumnDefinition.DataTypeName);
      Assert.AreEqual(string.Join(", ", arguments), string.Join(", ", parsedColumnDefinition.DataTypeArguments));
      Assert.AreEqual(nullability, parsedColumnDefinition.Nullability);
    }

    //[TestMethod]
    //public void ParseDataTypeNameFrom()
    //{
    //  var columnName = "TheName";
    //  var dataTypeName = "nvarchar";
    //  var arguments = "(MAX)";
    //  var nullability = "NOT NULL";

    //  var columnDefinition = Flux.Data.Sql.T.FormatColumnDefinition(columnName, dataTypeName, arguments, nullability);

    //  Assert.AreEqual(dataTypeName, Flux.Data.Sql.T.ParseDataTypeNameFrom(columnDefinition));
    //}
  }
}
