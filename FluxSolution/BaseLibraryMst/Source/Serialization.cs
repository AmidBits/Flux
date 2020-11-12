using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Flux;

namespace Serialization
{
  [System.Serializable]
  public class Person
  {
    public string Name { get; set; }
  }

  [TestClass]
  public class Json
  {
    Person person = new Person() { Name = "X" };

    readonly string expect = "{\"Name\":\"X\"}";

    [TestMethod]
    public void CloneJson()
    {
      Assert.AreEqual(person.Name, Flux.Serialize.CloneJson<Person>(person).Name);
    }

    [TestMethod]
    public void FromJson()
    {
      Assert.AreEqual(person.Name, Flux.Serialize.FromJson<Person>(expect).Name);
    }

    [TestMethod]
    public void ToJson()
    {
      Assert.AreEqual(expect, Flux.Serialize.ToJson(person));
    }
  }

  [TestClass]
  public class Xml
  {
    Person person = new Person() { Name = "X" };

    string expect = "<Person><Name>X</Name></Person>";

    [TestMethod]
    public void CloneXml()
    {
      Assert.AreEqual(person.Name, Flux.Serialize.CloneXml<Person>(person).Name);
    }

    [TestMethod]
    public void FromXml()
    {
      Assert.AreEqual(person.Name, Flux.Serialize.FromXml<Person>(expect).Name);
    }

    [TestMethod]
    public void ToXml()
    {
      Assert.AreEqual(expect, Flux.Serialize.ToXml(person));
    }
  }
}
