using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluxMst.IO
{
  [System.Serializable]
  public class Person(string name)
  {
    public Person() : this(string.Empty) { }

    public string Name { get; set; } = name;
  }

  [TestClass]
  public class Json
  {
    readonly Person person = new("X");

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
    readonly Person person = new("X");

    readonly string expect = "<Person><Name>X</Name></Person>";

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
