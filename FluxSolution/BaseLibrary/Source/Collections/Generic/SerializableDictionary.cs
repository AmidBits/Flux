//using System.Runtime.Serialization;
// https://stackoverflow.com/questions/1207731/how-can-i-deserialize-json-to-a-simple-dictionarystring-string-in-asp-net/31864584#31864584

namespace Flux
{
  [System.Serializable]
  public class SerializableDictionary<TKey, TValue>
    : System.Collections.Generic.Dictionary<TKey, TValue>
    , System.Xml.Serialization.IXmlSerializable
    where TKey : notnull
  {
    public System.Xml.Schema.XmlSchema? GetSchema() => null;

    //protected SerializableDictionary(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) => throw new System.NotImplementedException();

    public void ReadXml(System.Xml.XmlReader reader)
    {
      var keySerializer = new System.Xml.Serialization.XmlSerializer(typeof(TKey));
      var valueSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TValue));

      if (!(reader is null) && reader.IsEmptyElement && reader.Read())
      {
        while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
        {
          Add((TKey)keySerializer.Deserialize(reader), (TValue)valueSerializer.Deserialize(reader));
        }

        reader.ReadEndElement();
      }
    }

    public void WriteXml(System.Xml.XmlWriter writer)
    {
      var keySerializer = new System.Xml.Serialization.XmlSerializer(typeof(TKey));
      var valueSerializer = new System.Xml.Serialization.XmlSerializer(typeof(TValue));

      var namespaces = new System.Xml.Serialization.XmlSerializerNamespaces();
      namespaces.Add(string.Empty, string.Empty);

      foreach (var kvp in this)
      {
        keySerializer.Serialize(writer, kvp.Key, namespaces);
        valueSerializer.Serialize(writer, kvp.Value, namespaces);
      }
    }
  }
}
