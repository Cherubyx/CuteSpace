using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

[XmlRoot("Inventory")]
public class InventoryCollection {

  [XmlArray("Items")]
  [XmlArrayItem("Item")]
  public List<Item> items;

  public static InventoryCollection DeserializeFromXmlString(string xml) {
    XmlSerializer serializer = new XmlSerializer(typeof(InventoryCollection));
    return serializer.Deserialize(new StringReader(xml)) as InventoryCollection;
  }

}
